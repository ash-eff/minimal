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

    private float blockSize;

    GameController gc;

    private void Awake()
    {
        gc = FindObjectOfType<GameController>();
        blockSize = Random.Range(minSize, maxSize);
        transform.localScale = new Vector2(blockSize, blockSize);
    }

    private void Update()
    {
        if(transform.position.y < -(Camera.main.orthographicSize + maxSize))
        {
            Destroy(gameObject);
        }
    }

    void Explode()
    {
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
            gc.PlayerScore += 5 - Mathf.RoundToInt(transform.localScale.x);
            transform.GetComponent<Collider2D>().enabled = false;
            transform.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(collision.gameObject);
            Explode();
        }
    }
}
