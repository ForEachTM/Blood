using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    float ShakeTimer, ShakeAmount;

	void Update () {

        if (ShakeTimer > 0)
        {
            Vector2 shakePosition = Random.insideUnitCircle * ShakeAmount * Time.deltaTime;
            transform.position = new Vector3(transform.position.x + shakePosition.x, transform.position.y + shakePosition.y / 2, transform.position.z);

            ShakeTimer -= Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, transform.position.z), 0.1f);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, transform.position.z), 0.1f);
        }

	}

    public void ShakeCamera(float ShakePower, float ShakeDuration)
    {
        ShakeTimer = ShakeDuration;
        ShakeAmount = ShakePower;
    }

    public void Shake(float ShakePower)
    {
        Vector2 ShakePosition = Random.insideUnitCircle * ShakePower * Time.deltaTime;
        transform.position = new Vector3(transform.position.x + ShakePosition.x, transform.position.y + ShakePosition.y / 2, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, transform.position.z), 0.1f);
    }
}
