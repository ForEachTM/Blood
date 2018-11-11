using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : ABullet
{
    void Update()
    {
        Destroy(gameObject, LifeTime);
    }

    void FixedUpdate()
    {
        Rigidbody2D.velocity = Velocity;
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("MapCollider"))
        {
            Destroy(gameObject);
        }
    }

    public override void Flip(Vector2 scale)
    {
        Velocity = Velocity * scale;
    }
}
