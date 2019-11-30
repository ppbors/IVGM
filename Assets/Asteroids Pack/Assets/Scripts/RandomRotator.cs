using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    public float tumble;
    private Vector3 angularVelocity;

    private readonly float scale = 10.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.angularVelocity = Random.insideUnitSphere * tumble;
        transform.localScale = new Vector3(scale, scale, scale);
    }

    /* Added: for paused game */
    public void Freeze(bool on = true)
    {
        if (on)
        {
            angularVelocity = rb.angularVelocity;
            rb.angularVelocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.angularVelocity = angularVelocity;
        }
    }

    void OnColisionEnter(Collision collision)
    {
        /* TODO: ... */
    }
}