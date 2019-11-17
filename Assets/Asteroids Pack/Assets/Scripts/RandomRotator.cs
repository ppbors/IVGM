using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    public float tumble;
    private Vector3 angularVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.angularVelocity = Random.insideUnitSphere * tumble;
    }

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
}