﻿using UnityEngine;
using System.Collections;

public class ShotBehavior : MonoBehaviour {

    public ParticleSystem explosion;
    private readonly float explosionSize = 3000.0f;

	// Use this for initialization
	void Start ()
    {
        var main = explosion.main;
        main.startSize = explosionSize;
    }
	
	// Update is called once per frame
	void Update ()
    {
        // change to change speed of laser
		transform.position += transform.forward * Time.deltaTime * 100f;
	}

    private void OnTriggerEnter(Collider other)
    {
        // shot destroys itself once it hits something with a trigger.
        GameObject.Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        explosion = GameObject.Instantiate(explosion, transform.position, transform.rotation) as ParticleSystem;
        GameObject.Destroy(this.gameObject);
    }

}
