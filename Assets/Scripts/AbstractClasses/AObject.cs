using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AObject : MonoBehaviour
{

    [HideInInspector]
    public SpriteRenderer spriteRenderer;

    [HideInInspector]
    public new Rigidbody2D rigidbody2D;

    [HideInInspector]
    public Animator animator;

    public GameObject dummyPrefab;

    Dummy dummy;

    Transform circle;

    [HideInInspector]
    public LayerMask ground;

    [HideInInspector]
    public bool isCircleCollided;

    [HideInInspector]
    public bool facingRight;

    public const float circleRadiuos = 0.01f;

    public float speed;

    bool dummyExists;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        ground = LayerMask.GetMask("Ground");

        circle = transform.Find("Circle");
    }



    void LateUpdate()
    {
        if(circle != null)
            isCircleCollided = Physics2D.OverlapCircle(circle.position, circleRadiuos, ground);

        Flip();
    }

    public void SpawnDummy(Sprite sprite, Vector3 forcePosition, float force, bool smoke = false, bool manyDummies = false)
    {
        if (!dummyExists || manyDummies)
        {
            dummyExists = !manyDummies;

            GameObject dummyObject = Instantiate(dummyPrefab, transform.position, Quaternion.identity);

            dummy = dummyObject.GetComponent<Dummy>();
            dummy.ExplosionKnockback(force, forcePosition);
            dummy.Flip(transform.localScale.x);
            dummy.SetSprite(sprite);

            if (smoke) dummy.AddSmoke();
        }
    }

    public abstract void Flip();

}
