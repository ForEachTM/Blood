using UnityEngine;

public class CrateGenerator : MonoBehaviour {

    public GameObject crate, gameObject;

    public Vector2 min, max;

    public LayerMask player;

    const float radious = 1;

    bool isPlayerInside;

    Vector2 position;

    public void Start()
    {
        GenerateCrate();
    }

    public void GenerateCrate () {

        do
        {
            position = GetRandomPosition(min, max);

            isPlayerInside = Physics2D.OverlapCircle(position, radious, player);

        } while (isPlayerInside);

        Instantiate(crate, position, Quaternion.identity);
    }

    private Vector2 GetRandomPosition(Vector2 min, Vector2 max)
    {
        return new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
    }

}
