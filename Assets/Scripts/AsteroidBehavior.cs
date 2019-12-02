using UnityEngine;
using System.Collections;

public class AsteroidBehavior : MonoBehaviour
{
    public float tumble;

    private Rigidbody rb;
    private Vector3 angularVelocity = Vector3.zero;
    private Vector3 normalVelocity = Vector3.zero;
    private readonly float scale = 10.0f;
    private float x_Scale, y_Scale, z_Scale;

    private void Start()
    {
        Vector3 force;
        rb = gameObject.GetComponent<Rigidbody>();
        rb.angularVelocity = Random.insideUnitSphere * tumble;

        // Experiment on right velocity. Rigidbody drag on 0.001
        if(gameObject.name.Contains("1"))
            force = Random.Range(10,20) * transform.forward;
        else if (gameObject.name.Contains("2"))
            force = Random.Range(5, 15) * transform.forward + transform.right * Random.Range(0, 5);
        else
            force = 10* transform.forward + transform.right *-1 * Random.Range(0, 5);

        rb.velocity = force;
        rb.sleepThreshold = 1.0f;

        // set random range for size Asteroid
        x_Scale = Random.Range(5, 15);
        y_Scale = Random.Range(5, 15);
        z_Scale = Random.Range(5, 15);
        transform.localScale = new Vector3(x_Scale, y_Scale, z_Scale);
    }

    private void Update()
    {
        
    }

    /* Added: for paused game */
    public void Freeze(bool on = true)
    {
        //rb = GetComponent<Rigidbody>();
        if (on)
        {
            angularVelocity = rb.angularVelocity;
            normalVelocity = rb.velocity;
            rb.angularVelocity = Vector3.zero;
            rb.velocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.angularVelocity = angularVelocity;
            rb.velocity = normalVelocity;
        }
    }

    private void ChangeSize()
    {
        x_Scale -= 1.0f;
        y_Scale -= 1.0f;
        z_Scale -= 1.0f;

        if (x_Scale <= 1 || y_Scale <= 1 || z_Scale <= 1)
            Destroy(gameObject);

        transform.localScale = new Vector3(x_Scale, y_Scale, z_Scale);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("laser"))
        {
            ChangeSize();

        }
    }
  
}