using UnityEngine;
using System.Collections;

public class AsteroidBehavior : MonoBehaviour
{
   
    public float tumble;

    private GameManagerScript gameManager;
    private Rigidbody rb;
    private Vector3 angularVelocity;
    private Vector3 normalVelocity;
    private readonly float scale = 10.0f;
    private float x_Scale, y_Scale, z_Scale;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        rb = gameObject.GetComponent<Rigidbody>();

        angularVelocity = Vector3.zero;
        normalVelocity = Vector3.zero;
        RandomVelocity();
        RandomSize();
        
    }

    // Experiment on right velocity. Rigidbody drag on 0.001
    // Different diretions for the 3 different prefabs
    private void RandomVelocity()
    {
        //rb.angularVelocity = Random.insideUnitSphere * tumble;

        if (gameObject.name.Contains("1"))
            normalVelocity = Random.Range(10, 80) * transform.forward;
        else if (gameObject.name.Contains("2"))
            normalVelocity = Random.Range(5, 40) * transform.forward + transform.right * Random.Range(0, 10);
        else
            normalVelocity = 30 * transform.forward + transform.right * -1 * Random.Range(0, 20);

        rb.velocity = normalVelocity;
        rb.sleepThreshold = 1.0f;
    }

    // set random range for size Asteroid
    private void RandomSize()
    {
        x_Scale = Random.Range(5, 15);
        y_Scale = Random.Range(5, 15);
        z_Scale = Random.Range(5, 15);
        transform.localScale = new Vector3(x_Scale, y_Scale, z_Scale);
    }

    /* Added: for paused game */
    public void Freeze(bool on = true)
    {
        // extra in case Freeze is called when Start hasn't finished
        // (it happens)
        rb = gameObject.GetComponent<Rigidbody>();

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

    // Change size of Asteroid
    // Destroy when one scale < 1
    public void ChangeSize()
    {
        x_Scale -= 1.0f;
        y_Scale -= 1.0f;
        z_Scale -= 1.0f;

        if (x_Scale <= 1 || y_Scale <= 1 || z_Scale <= 1)
        {
            gameManager.AsteroidDestroyed();
            Destroy(gameObject);
        }

        transform.localScale = new Vector3(x_Scale, y_Scale, z_Scale);

    }
  
}