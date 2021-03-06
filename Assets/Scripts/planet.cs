﻿using UnityEngine;
using System.Collections;

public class planet : MonoBehaviour
{
    public int range = 450;
    public GameObject player;
    public float distance;


    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        distanceCheck();
    }

    void distanceCheck()
    {
        // Distance between Player and Planet
        distance = Vector3.Distance(this.transform.position, player.transform.position);

        if (distance <= range)
        {
            Debug.Log("You are Near " + this.gameObject.name);
            // Alex
            // player.GetComponent<ship> ().nearPlanet = true;

            // Anton
            player.GetComponent<PlayerMove>().nearPlanet = true;
            player.GetComponent<PlayerMove>().nearPlanetName = this.gameObject.name;
        }
    }
}
