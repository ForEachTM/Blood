using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    GameObject enemySpawner;

    GunManager gunManager;

    Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        gunManager = FindObjectOfType<GunManager>();
        enemySpawner = FindObjectOfType<EnemySpawner>().gameObject;
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.CompareTag("DeathZone") || collider2D.name.Equals("EnemyCollider"))
        {
            Manager.Instance.SetScore(player.Score);
            player.gameOverPanel.SetActive(true);
        }

        if (collider2D.CompareTag("DeathZone"))
        {
            enemySpawner.SetActive(false);
            Destroy(transform.parent.gameObject);
        }

        if (collider2D.name.Equals("EnemyCollider"))
        {
            player.SpawnDummy(player.spriteRenderer.sprite, collider2D.transform.position, 15f, manyDummies: true);

            player.SpawnDummy(gunManager.spriteRenderer.sprite, collider2D.transform.position, 20f, manyDummies: true);

            enemySpawner.SetActive(false);
            Destroy(transform.parent.gameObject);
        }

        if (collider2D.name.Equals("CrateCollider"))
        {
            player.SpawnDummy(gunManager.spriteRenderer.sprite, collider2D.transform.position, 15f, manyDummies: true);

            player.scoreText.text = "" + ++player.Score;
            gunManager.SetRandomGun();
            collider2D.gameObject.GetComponentInParent<Crate>().Respawn();
        }
    }
}
