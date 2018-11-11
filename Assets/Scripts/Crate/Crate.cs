using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour {

    public GameObject smokePrefab;

    public Vector2 min, max;

    Vector2 position;

    void Start ()
    {
        SetRandomPosition();
	}

    public void Respawn()
    {
        Instantiate(smokePrefab, transform.position, Quaternion.identity);

        SetRandomPosition();

        Instantiate(smokePrefab, transform.position, Quaternion.identity);
    }

    private void SetRandomPosition()
    {
        do
        {
            position = GetRandomPosition(min, max);
        } while (DeathZone(position) || Physics2D.OverlapCircle(position, 5f, LayerMask.GetMask("PhysicsColliders")));

        transform.position = position;
    }

    private bool DeathZone(Vector2 position)
    {
        return position.x < 1.5 && position.x > -1.5 && position.y < -4;
    }

    private Vector2 GetRandomPosition(Vector2 min, Vector2 max)
    {
        return new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
    }

}
