using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Boundary
{
    /* Skybox size */
    public const float 
        xMin = -5000.0f, 
        xMax = 5000.0f, 
        yMin = -5000.0f, 
        yMax = 5000.0f, 
        zMin = -5000.0f, 
        zMax = 5000.0f;
}

public class PlayerController : MonoBehaviour
{
    public AsteroidSpawn AS;
    public List<CannonBehavior> cannons;
    public List<ParticleSystem> ExhaustList;

    private GameManagerScript gm;
    private Rigidbody rb;

    /* Exhaust particle effect constants */
    private readonly float idleParticleSpeed = 1.0f;
    private readonly float moveParticleSpeed = 10.0f;
    private readonly float idleParticleScale = 1.0f;
    private readonly float moveParticleScale = 2.0f;
    /* --- */

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
    private float tilt = 0.0f;   /* left/right tilt */

    private float health;

    private readonly float speedH = 1.0f; /* speed for yaw */
    private readonly float speedV = 1.0f; /* speed for pitch */
    private readonly float speedZ = 0.2f; /* speed for tilt */
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

        tilt = Mathf.Clamp(tilt, -100f, 100f);

        /* It should add thrust to where the ship is facing */
        Vector3 movement = new Vector3(yaw,             /* Left / right */
                                       pitch,           /* Up / Down */
                                       moveVertical);   /* Forward / backward */

        ThrusterIntensify(moveVertical > 0);

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
        rb = gameObject.GetComponent<Rigidbody>();
        rb.mass = mass;
        rb.drag = drag;
        health = 200;
        InvokeRepeating("HealthDecreaseAutomatically", 1, 1);

    }

    // Update is called once per frame
    void Update()
    {
        // Shoot laser when SPACE or left-mouse button is pressed
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            // Instantiate laser
            cannons[Random.Range(0, 2)].Fire(3);
        }
    }

    public Vector3 GetCoordinates()
    {
        return transform.position;
    }

    private void HealthDecreaseAutomatically()
    {
        AddHealth(-0.5f);
    }

    public float getHealth()
    {
        return health;
    }

    public void AddHealth(float amount)
    {
        health += amount;
        // clamp health between 0 and 100
        health = (health < 0) ? 0 : (health > 100) ? 100 : health;

        if(health == 0)
        {
            gm.LostGame();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        AddHealth(-3);
    }


    public void Hide(bool on = true)
    {
        gameObject.SetActive(!on);
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

    private void ThrusterIntensify(bool move = true)
    {
        foreach (ParticleSystem exhaust in ExhaustList)
        {
            var main = exhaust.main;
            main.startSpeed = move ? moveParticleSpeed : idleParticleSpeed;
            main.startSize = move ? moveParticleScale : idleParticleScale;
        }
    }

    // Turns the thruster on/off
    public void ThrusterPlay(bool on = true)
    {
        foreach (ParticleSystem exhaust in ExhaustList)
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
    }

    // Pauses the thruster
    public void ThrusterPause()
    {
        foreach (ParticleSystem exhaust in ExhaustList)
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
}
