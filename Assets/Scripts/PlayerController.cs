using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Boundary
{
    /* Skybox size */
    public const float 
        xMin = -300.0f, 
        xMax = 300.0f, 
        yMin = -300.0f, 
        yMax = 300.0f, 
        zMin = -300.0f, 
        zMax = 300.0f;
}

public class PlayerController : MonoBehaviour
{
    private GameManagerScript gm;
    public ParticleSystem exhaust;
    private Rigidbody rb;

    /* Player rigidbody constants */
    private readonly float mass = 10.0f;
    private readonly float drag = 0.3f;
    /* --- */

    /* For saving directional variables in paused game state */
    private Vector3 velocity = Vector3.zero;
    private Vector3 angularVelocity = Vector3.zero;
    /* --- */

    /* Player movement variables / constants */
    private readonly float thrust = 1.0f; /* default thrust */
    private readonly float controlSpeed = 1.8f;

    private float yaw = 0.0f;    /* left/right yaw */
    private float pitch = 0.0f;  /* up/down pitch */
    private float tilt = 3.0f;   /* left/right tilt */

    private readonly float speedH = 1.0f; /* speed for yaw */
    private readonly float speedV = 1.0f; /* speed for pitch */
    private readonly float speedZ = 1.0f; /* speed for tilt */
    /* --- */

    /* Temp */
    public bool FULL360 = false; 
    /* --- */

    void FixedUpdate()
    {
        if (gm.IsPaused() || !gm.IsRunning())
            return;

        /* Update user input */
        float moveHorizontal = Input.GetAxis("Horizontal"); /* a/d -> roll left / right */
        float moveVertical = Input.GetAxis("Vertical");     /* w/s -> forward / backward */

        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");
        tilt += speedZ * moveHorizontal;

        /* It should add thrust to where the ship is facing */
        Vector3 movement = new Vector3(yaw,             /* Left / right */
                                       pitch,           /* Up / Down */
                                       moveVertical);   /* Forward / backward */

        /* Relative force (with respect to player rotation) */
        rb.AddRelativeForce(Vector3.forward * moveVertical * thrust, ForceMode.Impulse);

        /* Force to stay inside skybox */
        rb.position = new Vector3
        (
            Mathf.Clamp(rb.position.x, Boundary.xMin, Boundary.xMax),
            Mathf.Clamp(rb.position.y, Boundary.yMin, Boundary.yMax),
            Mathf.Clamp(rb.position.z, Boundary.zMin, Boundary.zMax)
        );

        transform.Rotate(new Vector3(pitch, yaw, -tilt) * controlSpeed * Time.deltaTime);
    }
    

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        exhaust = GameObject.Find("Particle System").GetComponent<ParticleSystem>();
        rb = gameObject.GetComponent<Rigidbody>();
        rb.mass = mass;
        rb.drag = drag;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnColisionEnter(Collision collision)
    {
        /* TODO: lose hp? */
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
