using System.Collections;
using UnityEngine;

public class DeleteObject : MonoBehaviour {

    public bool spawnSmthOnDie;

    public float delay;

    public GameObject effectsOnDie;

    void Start()
    {
        if (spawnSmthOnDie)
        {
            StartCoroutine(Die(delay, effectsOnDie));
        }
        else
        {
            StartCoroutine(Die(delay));
        }
    }

    IEnumerator Die(float delay, GameObject effectsOnDie)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
        Instantiate(effectsOnDie, transform.position, Quaternion.identity);
    }

    IEnumerator Die(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
