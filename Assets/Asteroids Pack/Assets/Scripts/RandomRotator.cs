using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour
{
    public float tumble;
    public GameObject m_ExplosionPrefab;


    private Rigidbody rb;
    private Vector3 angularVelocity;

    private int hit = 0;

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.name.Contains("laser")){
            Debug.Log("LASER");
            GameObject go = GameObject.Instantiate(m_ExplosionPrefab, collision.gameObject.transform.position, this.transform.rotation);
            go.transform.SetParent(this.gameObject.transform);
            GameObject.Destroy(go, 1);


            //count how manny times hit
            hit++;
            //if (hit > 4)
                //explodeAsteroid();
        }

    }

    private void Start()
    {
        Vector3 force;
        rb = GetComponent<Rigidbody>();
        rb.angularVelocity = Random.insideUnitSphere * tumble;

        // Experiment on right velocity. Rigidbody drag on 0
        if(this.name.Contains("1"))
            force = Random.Range(10,20) * transform.forward;
        else if (this.name.Contains("2"))
            force = Random.Range(5, 15) * transform.forward + transform.right * Random.Range(0, 5);
        else
            force = 10* transform.forward + transform.right *-1 * Random.Range(0, 5);

        rb.velocity = force;
        rb.sleepThreshold = 1.0f;
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