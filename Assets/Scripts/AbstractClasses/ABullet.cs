using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ABullet : MonoBehaviour
{
    public Rigidbody2D Rigidbody2D { get; set; }

    public SpriteRenderer SpriteRenderer { get; set; }

    public Vector2 Velocity { get; set; }

    public float LifeTime { get; set; }

    public int Damage { get; set; }

    void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public abstract void Flip(Vector2 scale);

}
