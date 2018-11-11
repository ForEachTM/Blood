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
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, transform.position.z), 0.01f);
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
}
