using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SecondBlock : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem tinyExplosion;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private TextMeshProUGUI lifeText;

    private Rigidbody2D rb2d;
    private Collider2D col;

    GameController gc;

    float timer = 1f;
    float xAxis;
    float yAxis;
    bool shooting;

    private void Awake()
    {
        gc = FindObjectOfType<GameController>();
        col = GetComponent<BoxCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        xAxis = Random.Range(-5f, 5f);
        yAxis = Random.Range(0f, 2f);
        lifeText.text = "...";
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (transform.position.y < -(Camera.main.orthographicSize + 1))
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if(timer > 0)
        {
            rb2d.velocity = new Vector2(xAxis, yAxis);
        }
        else
        {
            rb2d.velocity = new Vector2(0, 0);
            if (!shooting)
            {
                shooting = true;
                col.enabled = true;
                StartCoroutine(Shoot());
                StartCoroutine(LifeCount());
            }
        }
    }

    IEnumerator Shoot()
    {
        int bullets = 3;
        while(bullets > 0)
        {
            Instantiate(bulletPrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            bullets--;
            yield return new WaitForSeconds(.5f);
        }

        Instantiate(tinyExplosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    IEnumerator LifeCount()
    {
        while (true)
        {
            lifeText.text = "..";
            yield return new WaitForSeconds(1);
            lifeText.text = ".";
            yield return new WaitForSeconds(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bullet")
        {
            gc.PlayerScore += 5;
            Destroy(collision.gameObject);
            Instantiate(tinyExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
