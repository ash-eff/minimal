using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBullet : MonoBehaviour
{
    float speed;
    Rigidbody2D rb2d;

    private void Awake()
    {
        speed = Random.Range(13, 16);
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (transform.position.y < -(Camera.main.orthographicSize + 1))
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + Vector2.down * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
