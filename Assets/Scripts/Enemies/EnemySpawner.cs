using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemies;

    GameObject enemy;

    public Vector2[] spawnPositions;

    public float timeBtwSpawns, timeBtwWaveSpawn;

    float timer;

    float randomEnemy;

    int randomPosition;

    int enemyToSpawn;

    int waveEnemyCounter;

    bool spawnWave;

    void Start()
    {
        randomPosition = Random.Range(0, spawnPositions.Length);
        randomEnemy = Random.Range(0, enemies.Length);

        if (randomEnemy == 0)
        {
            spawnWave = Random.value > 0.5f ? true : false;
        }

        timer = timeBtwSpawns;
    }

    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            if (spawnWave)
            {
                if (waveEnemyCounter < 3)
                {
                    waveEnemyCounter++;
                    if(waveEnemyCounter == 3) timer = timeBtwSpawns; else timer = timeBtwWaveSpawn;
                }
                else
                {
                    spawnWave = false;
                    waveEnemyCounter = 0;
                    timer = timeBtwSpawns;
                }  
            }
            else
            {
                timer = timeBtwSpawns + Random.Range(-1f, 1f);
            }
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        if (!spawnWave)
        {
            randomPosition = Random.Range(0, spawnPositions.Length);
            randomEnemy = Random.value;

            if (randomEnemy > 0.9)
            {
                enemyToSpawn = 1;
            }
            else if (randomEnemy > 0.7)
            {
                enemyToSpawn = 2;
            }
            else if (randomEnemy > 0)
            {
                enemyToSpawn = 0;
            }

            if (enemyToSpawn == 0)
            {
                spawnWave = Random.value > 0.7f ? true : false;
            }
        }
        enemy = Instantiate(enemies[enemyToSpawn], spawnPositions[randomPosition], Quaternion.identity);

        switch (enemyToSpawn)
        {
            case 0:
                enemy.GetComponent<Enemy>().Flip(-Mathf.Sign(spawnPositions[randomPosition].x));
                break;
            case 2:
                enemy.GetComponent<Sceleton>().Flip(-Mathf.Sign(spawnPositions[randomPosition].x));
                break;
            case 1:
                enemy.GetComponent<Enemy>().Flip(-Mathf.Sign(spawnPositions[randomPosition].x));
                timer = timeBtwSpawns - Random.Range(0.5f, 1f);
                break;
        }
    }

    IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
    }
}
