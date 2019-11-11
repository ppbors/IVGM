using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public ParticleSystem exhaust;
    private Rigidbody rb;

    public float thrust = 100000.0f;
    private Vector3 velocity = Vector3.zero;
    private Vector3 angularVelocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {        
        exhaust = GameObject.Find("Particle System").GetComponent<ParticleSystem>();
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            // TODO: add extra thruster intensity
            rb.AddForce(0, 0, thrust, ForceMode.Impulse);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            // About half the forward thrust possible ?
            rb.AddForce(0, 0, -thrust / 2, ForceMode.Impulse);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            // Bank left
        }
        else if (Input.GetKey(KeyCode.D))
        {
            // Bank right
        }
        
    }

    public void Freeze(bool on = true)
    {
        if (on)
        {
            velocity = rb.velocity; // Save the original velocity
            angularVelocity = rb.angularVelocity;

            // Make the object stop moving
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
            // First remove the constraint, then re-introduce its velocity
            rb.constraints = RigidbodyConstraints.None;
            rb.velocity = velocity;
            rb.angularVelocity = angularVelocity;
        }
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
