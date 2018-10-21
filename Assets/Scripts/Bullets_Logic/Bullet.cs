using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : IBullet
{
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Destroy(gameObject, LifeTime);
    }

    void FixedUpdate()
    {
        Rigidbody2D.velocity = Velocity;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        tag = collision.gameObject.tag;
        if (tag == "Enemy" || tag == "MapCollider")
        {
            Destroy(gameObject);
        }
    }

    public override void Flip(Vector2 Scale)
    {
        SetVelocity(Velocity * Scale);
    }
}
