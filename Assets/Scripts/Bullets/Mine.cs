using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{

    public GameObject explosion;

    CameraController сameraController;

    private void Start()
    {
        сameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.name.Equals("EnemyCollider"))
        {
            Delete();
        }
    }

    public void Delete()
    {
        сameraController.ShakeCamera(8f, 0.5f);
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(transform.parent.gameObject);
    }
}
