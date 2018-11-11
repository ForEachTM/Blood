using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fraction : ABullet
{
    void Update()
    {
        Destroy(gameObject, LifeTime);
    }

    void FixedUpdate()
    {
        Velocity = new Vector2(Velocity.x - Velocity.x * 1f / Mathf.Abs(Velocity.x), Velocity.y);
        Rigidbody2D.velocity = Velocity;
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("MapCollider"))
        {
            Destroy(gameObject);
        }

        if (collider2D.CompareTag("Enemy"))
        {
            collider2D.gameObject.GetComponent<Enemy>().Hit(Damage);
            Destroy(gameObject);
        }
    }

    public override void Flip(Vector2 scale)
    {
        Velocity = Velocity * scale;
    }
}
