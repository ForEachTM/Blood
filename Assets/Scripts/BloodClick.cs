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
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(prefabs[currentParticle], mousePosition, Quaternion.identity);
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (currentParticle < prefabs.Length - 1) {
                currentParticle += 1;
            } else
            {
                currentParticle = 0;
            }
        }

        transform.position = mousePosition;

        spriteRenderer.sprite = sprites[currentParticle];

    }
}
