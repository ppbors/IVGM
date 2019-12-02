using UnityEngine;
using System.Collections;

public class AsteroidBehavior : MonoBehaviour
{
    public float tumble;

    private Rigidbody rb;
    private Vector3 angularVelocity;
    private Vector3 normalVelocity;
    private readonly float scale = 10.0f;


    private void Start()
    {
        Vector3 force;
        rb = GetComponent<Rigidbody>();
        rb.angularVelocity = Random.insideUnitSphere * tumble;

        // Experiment on right velocity. Rigidbody drag on 0
        if(gameObject.name.Contains("1"))
            force = Random.Range(10,20) * transform.forward;
        else if (gameObject.name.Contains("2"))
            force = Random.Range(5, 15) * transform.forward + transform.right * Random.Range(0, 5);
        else
            force = 10* transform.forward + transform.right *-1 * Random.Range(0, 5);

        rb.velocity = force;
        rb.sleepThreshold = 1.0f;
        transform.localScale = new Vector3(Random.Range(5,15), Random.Range(5, 15), Random.Range(5, 15));
    }

    /* Added: for paused game */
    public void Freeze(bool on = true)
    {
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

    void OnColisionEnter(Collision collision)
    {
        /* TODO: ... */
    }
}