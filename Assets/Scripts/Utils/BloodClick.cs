using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodClick : MonoBehaviour
{

    public GameObject[] prefabs;

    private Vector2 mousePosition;

    private int currentParticle;

    SpriteRenderer spriteRenderer;

    public Sprite[] sprites;

    // Use this for initialization
    void Start () {
        currentParticle = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(prefabs[currentParticle], mousePosition, Quaternion.identity);
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            currentParticle++;
        }
        else if(Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            currentParticle--;
        }

        if (currentParticle < 0) // forward
        {
            currentParticle = prefabs.Length - 1;
        }
        else if (currentParticle > prefabs.Length - 1)
        {
            currentParticle = 0;
        }

        transform.position = mousePosition;

        spriteRenderer.sprite = sprites[currentParticle];

    }
}
