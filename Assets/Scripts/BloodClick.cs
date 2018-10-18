using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodClick : MonoBehaviour
{

    public GameObject[] prefabs;

    private Vector3 mousePosition;

    private int currentParticle;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            mousePosition = Input.mousePosition;

            Vector2 spawnPosition = new Vector2((mousePosition.x-Screen.width/2) / 50, (mousePosition.y - Screen.height / 2) / 50);

            Instantiate(prefabs[currentParticle], spawnPosition, Quaternion.identity);
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

    }
}
