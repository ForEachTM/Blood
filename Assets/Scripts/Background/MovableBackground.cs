﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableBackground : MonoBehaviour
{
    public float slowMultiplier;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
            transform.position = new Vector2(-player.transform.position.x / slowMultiplier, -player.transform.position.y / slowMultiplier / 4);
    }
}
