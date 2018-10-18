using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour {

    public GameObject smokePrefab;

	// Use this for initialization
	void Awake () {
        Instantiate(smokePrefab, transform.position, Quaternion.identity);
	}
	
	// Update is called once per frame
	public void Destroy () {
        Instantiate(smokePrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
