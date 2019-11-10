﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public ParticleSystem exhaust;

    // Start is called before the first frame update
    void Start()
    {        
        exhaust = GameObject.Find("Particle System").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Turns the thruster on/off
    public void ThrusterPlay(bool on)
    {
        if (on && !exhaust.isPlaying)
        {
            exhaust.Play();
        }
        else if (!on && exhaust.isPlaying)
        {
            exhaust.Stop();
        }
        else
        {
            Debug.Log("Invalid Toggle ThrusterPlay(...)");
        }
    }
    
}
