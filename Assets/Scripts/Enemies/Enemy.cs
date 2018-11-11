using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : AObject {

    CameraController cameraController;

    EnemySpawner enemySpawner;

    public GameObject fire;

    public float angrySpeed;

    public int Health;

    public bool cameraShake;

    bool angry;

    // Use this for initialization
    void Start () {
        cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    void FixedUpdate()
    {
        rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
    }

    public override void Flip()
    {
        if (isCircleCollided)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
            speed *= -1;
        }
    }

    public void Flip(float sign)
    {
        Vector3 scale = transform.localScale;
        scale.x *= sign;
        transform.localScale = scale;
        speed *= sign;
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("MapCollider"))
        {
            if (cameraShake) cameraController.ShakeCamera(8, 0.1f);
        }

        if (collider2D.CompareTag("Explosion"))
        {
            SpawnDummy(spriteRenderer.sprite, collider2D.transform.position, 20f, smoke: true);

            Destroy(gameObject);
        }

        if (collider2D.CompareTag("Bullet"))
        {
            if (Health <= 0)
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
            if(!angry)
                SetAngry();
            transform.position = enemySpawner.spawnPositions[transform.localScale.x == 1 ? 0 : 1];
        }  
    }

    void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Bullet"))
        {
            Destroy(collider2D.gameObject);
        }
    }

    public void Hit(int Damage)
    {
        if (Health > 0)
        {
            Health -= Damage;
        }else
        {
            Health = 0;
        }
        StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        spriteRenderer.material.color = Color.red;
        yield return new WaitForSeconds(.1f);
        spriteRenderer.material.color = Color.white;
    }

    public void SetAngry()
    {
        angry = true;
        animator.SetBool("Angry", true);
        speed = angrySpeed * transform.localScale.x;
        GameObject gameObject = Instantiate(fire, transform.position, Quaternion.identity);
        gameObject.transform.parent = transform;
        gameObject.transform.localScale =  new Vector3(1,1,1);
    }
}
