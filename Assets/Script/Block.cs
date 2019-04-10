using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField]
    private float minSize;
    [SerializeField]
    private float maxSize;
    [SerializeField]
    private GameObject secondPrefab;
    [SerializeField]
    private ParticleSystem explode;
    [SerializeField]
    private int numOfExtraBlocks;
    [SerializeField]
    private AudioClip explodeClip;

    private float blockSize;

    Collider2D col;
    GameController gc;

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        gc = FindObjectOfType<GameController>();
        blockSize = Random.Range(minSize, maxSize);
        transform.localScale = new Vector2(blockSize, blockSize);
    }

    private void Update()
    {
        if(transform.position.y < Camera.main.orthographicSize)
        {
            col.enabled = true;
        }
        if(transform.position.y < -(Camera.main.orthographicSize + maxSize))
        {
            Destroy(gameObject);
        }
    }

    void Explode()
    {
        gc.ScorePopUp(transform.position, 5 - Mathf.RoundToInt(transform.localScale.x));;
        col.enabled = false;
        transform.GetComponent<SpriteRenderer>().enabled = false;
        gc.PlayerScore += 5 - Mathf.RoundToInt(transform.localScale.x);
        gc.PlaySFX(explodeClip);
        Instantiate(explode, transform.position, Quaternion.identity);

        for (int i = 0; i < numOfExtraBlocks; i++)
        {
            Instantiate(secondPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            Explode();
        }
    }
}
