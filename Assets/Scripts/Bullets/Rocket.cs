using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : ABullet
{

    public GameObject explosion;
    GameObject smoke;

    CameraController сameraController;

    void Start () {
        сameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        smoke = gameObject.transform.GetChild(0).gameObject;
    }
	
	void Update () {
        Destroy(gameObject, LifeTime);
    }

    void FixedUpdate()
    {
        Velocity = new Vector2(Velocity.x + Velocity.x * 0.6f / Mathf.Abs(Velocity.x), Velocity.y);

        Rigidbody2D.velocity = Velocity;
    }

    void OnTriggerStay2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("MapCollider") || collider2D.name.Equals("EnemyCollider"))
        {
            Delete();
        }
    }

    public override void Flip(Vector2 scale)
    {
        transform.localScale = scale;
        Velocity = Velocity * scale; 
    }

    public void Delete()
    {
        сameraController.ShakeCamera(8f, 0.5f);
        smoke.transform.parent = null;
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
