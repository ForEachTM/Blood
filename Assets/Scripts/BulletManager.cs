using UnityEngine;
using System.Collections;

public class BulletManager : MonoBehaviour {

    public GameObject explosionPrefab, smokePrefab, smoke;

    public SpriteRenderer spriteRenderer;
    public Sprite[] bullets;

    Rigidbody2D rigidbody2D;

    BoxCollider2D boxCollider2D;

    Vector3 velocity;

    float speed;
    float dispersion;
    float lifetime;

    public enum Bullets { Small, Larger, Rocket, Mine };

    [HideInInspector]
    public Bullets bullet;

    public void Init()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update () {
        if (bullet != Bullets.Mine) Destroy(gameObject, lifetime);
    }

    void FixedUpdate()
    {
        if (smoke != null)
        {
            velocity.x += velocity.x * 1f/Mathf.Abs(velocity.x);
        }

        transform.position += velocity * Time.fixedDeltaTime;
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        tag = collider2D.gameObject.tag;

        if (tag.Equals("Enemy") || tag.Equals("MapCollider"))
        {
            if(smoke != null)
            {
                smoke.transform.parent = null;
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }

            if(bullet == Bullets.Mine && tag.Equals("Enemy"))
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }

            if(bullet != Bullets.Mine) Destroy(gameObject);
        }

    }

    /*void OnCollsionEnter2D(Collision2D collision2D)
    {
        tag = collision2D.gameObject.tag;

        if (!tag.Equals("MapCollider"))
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision2D.gameObject.GetComponent<Collider2D>());
        }

    }*/

    public void AddSmoke()
    {
        smoke = Instantiate(smokePrefab, transform.position, Quaternion.identity);
        smoke.gameObject.transform.SetParent(gameObject.transform);
    }

    public void SetDispersion(float dispersion)
    {
        this.dispersion = dispersion;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetLifeTime(float lifetime)
    {
        this.lifetime = lifetime;
    }

    public void SetBullet(int bulletType)
    {
        Init();

        bullet = (Bullets)bulletType;

        spriteRenderer.sprite = bullets[(int)bullet];
        switch (bullet)
        {
            case Bullets.Small:
                velocity = new Vector3(speed, dispersion, 0f);
                break;

            case Bullets.Larger:
                velocity = new Vector3(speed, 0f, 0f);
                break;

            case Bullets.Rocket:
                velocity = new Vector3(speed, 0f, 0f);
                AddSmoke();
                break;

            case Bullets.Mine:
                velocity = new Vector3(0f, 0f, 0f);
                rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
                rigidbody2D.gravityScale = 15f;
                boxCollider2D.isTrigger = false;
                break;
        }
    }

    public void Flip(bool facingRight)
    {
        if (facingRight)
        {
            spriteRenderer.flipX = facingRight;
            velocity.x *= -1;
        }
    }
}
