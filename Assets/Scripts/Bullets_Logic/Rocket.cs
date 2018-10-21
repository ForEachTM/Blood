using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : IBullet {

    public GameObject Explosion;
    GameObject Smoke;

    CameraController CameraController;

    void Start () {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        CameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        Smoke = gameObject.transform.GetChild(0).gameObject;
    }
	
	void Update () {
        Destroy(gameObject, LifeTime);
    }

    void FixedUpdate()
    {
        Velocity.x += Velocity.x * 1f / Mathf.Abs(Velocity.x);

        Rigidbody2D.velocity = Velocity;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        tag = collision.gameObject.tag;
        if (tag == "Enemy" || tag == "MapCollider")
        {
            Delete();
        }
    }

    public override void Flip(Vector2 Scale)
    {
        transform.localScale = Scale;
        SetVelocity(Velocity * Scale); 
    }

    public void Delete()
    {
        CameraController.ShakeCamera(10, 0.3f);
        Smoke.transform.parent = null;
        Instantiate(Explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
