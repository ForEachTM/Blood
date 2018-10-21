using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IBullet : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D Rigidbody2D;

    [HideInInspector]
    public SpriteRenderer SpriteRenderer;

    [HideInInspector]
    public Vector2 Velocity;

    [HideInInspector]
    public float LifeTime;

    public abstract void Flip(Vector2 Scale);

    public void SetVelocity(Vector2 Velocity) { this.Velocity = Velocity; }

    public void SetLifeTime(float LifeTime){ this.LifeTime = LifeTime; }
}
