using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float shootSpeed;
    [SerializeField]
    private ParticleSystem explosion;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private int health = 100;
    [SerializeField]
    private AudioClip shoot;
    [SerializeField]
    private AudioClip damage;

    private float nextFire = 0.0f;

    private Collider2D col;
    private SpriteRenderer spr;
    private Camera cam;
    private CameraShake cs;
    private GameController gc;
    private Rigidbody2D rb2d;

    private Vector2 velocity;

    private float camWidth;
    private float camHeight;
    private float alteredCamWidth;
    private float moveSpeed;


    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        cs = cam.GetComponent<CameraShake>();
        gc = FindObjectOfType<GameController>();
        spr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        gc.PlayerHealth = health;
        //StartCoroutine(Shoot());
    }

    private void Update()
    {
        if (gc.PlayerDead)
        {
            velocity = Vector2.zero;
            return;
        }

        Shoot();

        ClampPosition();
        velocity = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);

        if (health <= 0)
        {
            gc.PlayerDead = true;
            explosion.Play();
            col.enabled = false;
            spr.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + velocity * speed * Time.fixedDeltaTime);
    }

    void ClampPosition()
    {
        camHeight = cam.orthographicSize;
        camWidth = camHeight * cam.aspect;
        alteredCamWidth = camWidth / 3f;

        Vector2 clampedPos = new Vector2(Mathf.Clamp(transform.position.x, (-alteredCamWidth + (transform.localScale.x / 2)), (alteredCamWidth - (transform.localScale.x / 2))),
                                         Mathf.Clamp(transform.position.y, (-camHeight + (transform.localScale.y / 2)), camHeight - (transform.localScale.y / 2)));
        
        transform.position = clampedPos;
    }

    void Shoot()
    {
        if (Input.GetMouseButton(0) && Time.time > nextFire)
        {
            gc.PlaySFX(shoot);
            nextFire = Time.time + shootSpeed;
            Instantiate(bulletPrefab, new Vector2(transform.position.x, transform.position.y + transform.localScale.y), Quaternion.identity);

        }
    }

    //IEnumerator Shoot()
    //{
    //    while (!gc.PlayerDead)
    //    {
    //        gc.PlaySFX(shoot);
    //        Instantiate(bulletPrefab, new Vector2(transform.position.x, transform.position.y + transform.localScale.y), Quaternion.identity);
    //
    //        yield return new WaitForSeconds(shootSpeed);
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9 || collision.gameObject.layer == 10)
        {
            gc.PlaySFX(damage);
            StartCoroutine(cs.Shake(.5f, .2f));
            StartCoroutine(cs.ColorLerp());
            StartCoroutine(gc.LifeColorLerp());
            StartCoroutine(gc.ScoreColorLerp());
            health--;   
            gc.PlayerHealth = health;
        }
    }
}
