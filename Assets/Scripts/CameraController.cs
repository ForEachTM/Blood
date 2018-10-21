using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    float shakeTimer, shakeAmount;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (shakeTimer >= 0)
        {
            Vector2 shakePosition = Random.insideUnitCircle * shakeAmount * Time.deltaTime;
            transform.position = new Vector3(transform.position.x + shakePosition.x, transform.position.y + shakePosition.y/2, transform.position.z);

            shakeTimer -= Time.deltaTime;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, transform.position.z), 0.1f);
        }

	}

    public void ShakeCamera(float shakePower, float shakeDuration)
    {
        shakeTimer = shakeDuration;
        shakeAmount = shakePower;
    } 
}
