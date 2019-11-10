using System.Collections;
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
    public void ThrusterPlay(bool on = true)
    {
        if (on && !exhaust.isPlaying)
        {
            exhaust.Play();
        }
        else if (!on && (exhaust.isPlaying || exhaust.isPaused))
        {
            exhaust.Stop();
        }
        else // If play->play or stop->stop
        {
            Debug.Log("Invalid Toggle ThrusterPlay(...)");
        }
    }

    // Pauses the thruster
    public void ThrusterPause()
    {
        if (exhaust.isPlaying)
        {
            exhaust.Pause();
        }
        else // If not initialized / active
        {
            Debug.Log("Invalid Pausing ThrusterPause()");
        }
    }
}
