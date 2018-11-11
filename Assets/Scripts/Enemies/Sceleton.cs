using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sceleton : AObject
{
    EnemySpawner enemySpawner;

    Player player;

    Transform up, down, left;

    Vector2 velocity;

    public Sprite angry;

    float health;

    float velX = 2, velY = 2;
    const float angryVelX = 4, angryVelY = 3;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();

        player = FindObjectOfType<Player>();

        up = transform.Find("Up");
        down = transform.Find("Down");
        left = transform.Find("Left");

        velocity.y = -velY;
        velocity.x = Mathf.Sign(transform.localScale.x);

        health = 2f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isCircleCollided || Physics2D.OverlapCircle(left.position, circleRadiuos, ground))
        {
            velocity.y = Mathf.Sign(velocity.y) * Random.Range(velY, velY*2);
            velocity.x *= -1;
            Flip(-1);
        }

        if (Physics2D.OverlapCircle(up.position, circleRadiuos, ground) || Physics2D.OverlapCircle(down.position, circleRadiuos, ground))
        {
            velocity.y = Mathf.Sign(velocity.y) * Random.Range(velY, velY * 2);
            velocity.x = Mathf.Sign(transform.localScale.x) * Random.Range(velX, velX*2);
            velocity.y *= -1;
        }

        rigidbody2D.velocity = new Vector2(velocity.x, rigidbody2D.velocity.y);
        transform.position = new Vector2(transform.position.x, transform.position.y + velocity.y * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    { 
        if (collider2D.CompareTag("Explosion"))
        {
            SpawnDummy(spriteRenderer.sprite, collider2D.transform.position, 20f, smoke: true);

            Destroy(gameObject);
        }

        if (collider2D.CompareTag("Bullet"))
        {
            if (health <= 0)
            {
                SpawnDummy(spriteRenderer.sprite, collider2D.transform.position, 15f);
                Destroy(gameObject);
            }
            else
            {
                Hit(collider2D.gameObject.GetComponent<ABullet>().Damage);
            }

            Destroy(collider2D.gameObject);
        }

        if (collider2D.CompareTag("DeathZone"))
        {
            SetAngry();
            transform.position = enemySpawner.spawnPositions[transform.localScale.x == 1 ? 0 : 1];
        }
    }

    public override void Flip()
    {
        Vector2 scale = transform.localScale;

        if (player != null)
        {
            if (player.transform.position.y - 3f < transform.position.y && player.transform.position.y + 3f > transform.position.y)
            {
                if (transform.position.x > player.transform.position.x) { scale.x = -1; } else { scale.x = 1; }
            }
        }

        transform.localScale = scale;
    }

    public void Hit(int Damage)
    {
        if (health > 0)
        {
            health -= Damage;
        }
        else
        {
            health = 0;
        }
        StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        spriteRenderer.material.color = Color.red;
        yield return new WaitForSeconds(.1f);
        spriteRenderer.material.color = Color.white;
    }

    public void Flip(float sign)
    {
        Vector3 scale = transform.localScale;
        scale.x *= sign;
        transform.localScale = scale;
        speed *= sign;
    }

    public void SetAngry()
    {
        spriteRenderer.sprite = angry;
        velocity.y = -velY;
        velX = angryVelX * transform.localScale.x;
        velY = angryVelY;
    }
}
