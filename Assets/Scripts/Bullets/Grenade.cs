using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : ABullet
{

    public GameObject explosion;

    CameraController сameraController;

    void Start()
    {
        сameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
    }

    void Update()
    {
        transform.parent.position = new Vector2(transform.position.x + Velocity.x * Time.fixedTime, transform.position.y + Velocity.y * Time.fixedTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Equals("EnemyCollider"))
        {
            Delete();
        }
    }

    public override void Flip(Vector2 scale)
    {
        Velocity = Velocity * scale;
    }

    public void Delete()
    {
        сameraController.ShakeCamera(8f, 0.5f);
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(transform.parent.gameObject);
    }
}
