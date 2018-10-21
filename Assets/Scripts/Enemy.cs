using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public GameObject smokePrefab, smoke;

    public Animator animator;

    public int lives;

    const float radious = 0.01f;

    public float speed;

    public Transform feetPose;
    public LayerMask ground;

    Rigidbody2D rigidbody2D;

    BoxCollider2D boxCollider2D;

    bool dead, flip;

    // Use this for initialization
    void Start () {
        lives = 3;
        rigidbody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {
        flip = Physics2D.OverlapCircle(feetPose.position, radious, ground);

        transform.position = new Vector2(Mathf.Round(transform.position.x * 10) * 0.1f, Mathf.Round(transform.position.y * 10) * 0.1f);

        if (transform.position.y <= -10 && !dead)
        {
            SetAngry();
            transform.position = new Vector2(-10,10);
        }
        else if(dead && transform.position.y <= -10)
        {
            smoke.transform.parent = null;
            Destroy(gameObject);
        }

        rigidbody2D.velocity = new Vector2(Mathf.Clamp(rigidbody2D.velocity.x,-20,20), Mathf.Clamp(rigidbody2D.velocity.y, -30, 20));

        Flip();
    }

    private void Flip()
    {
        if (flip)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
            speed *= -1;
        }
    }

    public void Knockback(float knockback, Vector3 position)
    {
        dead = true;

        rigidbody2D.freezeRotation = false;
        boxCollider2D.isTrigger = true;

        Vector2 moveDirection = transform.position - position;

        rigidbody2D.AddForce(new Vector2(moveDirection.normalized.x * knockback, knockback), ForceMode2D.Impulse);
        rigidbody2D.AddTorque(knockback);
    }

    private void FixedUpdate()
    {
        if (!dead)
        {
            rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
        }
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.tag.Equals("Explosion"))
        {
            rigidbody2D.freezeRotation = false;
            boxCollider2D.isTrigger = true;
            Knockback(5f, collision2D.gameObject.transform.position);
        }
    }

    public void AddSmoke()
    {
        smoke = Instantiate(smokePrefab, transform.position, Quaternion.identity);
        smoke.gameObject.transform.SetParent(gameObject.transform);
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag.Equals("Explosion"))
        {
            AddSmoke();
            Knockback(20f, collider2D.gameObject.transform.position);
        }

        Debug.Log(collider2D.gameObject.tag);

        if (collider2D.gameObject.tag.Equals("Bullet"))
        {

            if (lives <= 0)
            {
                Knockback(20f, collider2D.gameObject.transform.position);
            }
            else
            {
                lives--;
            }

        }
        /*if (collider2D.gameObject.tag.Equals("MapCollider"))
        {
            boxCollider2D.isTrigger = false;
        }

        if (collider2D.gameObject.tag.Equals("Explosion"))
        {
            rigidbody2D.freezeRotation = false;
            boxCollider2D.isTrigger = true;
            Knockback(20f, collider2D.gameObject.transform.position);
        }*/
    }

    public void SetAngry()
    {
        animator.SetBool("Angry", true);
        speed = 10f * transform.localScale.x;
    }
}
