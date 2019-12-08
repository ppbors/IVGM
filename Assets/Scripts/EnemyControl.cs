using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    private Rigidbody rb;
    public ParticleSystem exhaust;
    public GameObject m_laserPrefab;

    private GameManagerScript gm;
    public PlayerController player;

    private Transform target;
    private Transform myTransform;

    /* Enemy movement constants */
    private readonly float thrust = 1.0f;
    private readonly float rotationSpeed = 1.0f;
    private readonly float moveSpeed = 5.0f;
    /* --- */

    /* Enemy distance trigger constants */
    private readonly float maxDist = 200.0f;
    private readonly float fireDist = 100.0f;
    /* --- */

    private bool cannonOnCooldown = true;
    private float fireRate = 3.0f; 

    /* Enemy rigidbody constants */
    private readonly float mass = 10.0f;
    private readonly float drag = 0.3f;
    /* --- */

    /* Exhaust particle effect constants (specific for enemy) */
    private readonly float idleParticleSpeed = 1.0f;
    private readonly float moveParticleSpeed = 10.0f;
    private readonly float idleParticleScale = 1.0f;
    private readonly float moveParticleScale = 2.0f;
    /* --- */

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        gm = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        rb = gameObject.GetComponent<Rigidbody>();
        rb.mass = mass;
        rb.drag = drag;

        StartCoroutine(CoolDown());
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.IsPaused() || !gm.IsRunning())
            return;

        /* Update locations */
        target = player.transform;
        myTransform = this.transform;

        if (IsPlayerWithinAttackRange())
        {
            RotateTowardsPlayer();

            if (!OnCooldown())
                Fire();
        }
        else if (IsPlayerWithinApproachRange())
        {
            RotateTowardsPlayer();
            MoveForward();
        }
        else
        {
            // Idle state: do nothing?
        }
    }

    private void RotateTowardsPlayer()
    {
        myTransform.rotation = Quaternion.Slerp(myTransform.rotation,
                               Quaternion.LookRotation(target.position - myTransform.position), 
                               rotationSpeed * Time.deltaTime);
    }

    private bool OnCooldown()
    {
        return cannonOnCooldown;
    }

    private void MoveForward() // need thrust code here instead of simple addition
    {
        myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
    }

    private bool IsPlayerWithinApproachRange()
    {
        var distance = (target.position - myTransform.position).magnitude;
        return distance < maxDist;
    }

    private bool IsPlayerWithinAttackRange()
    {
        var distance = (target.position - myTransform.position).magnitude;
        return distance < fireDist;
    }

    private IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(fireRate);
        cannonOnCooldown = false;
    }

    private void Fire()
    {
        // Instantiate laser, spawn slightly in front
        cannonOnCooldown = true;
        GameObject go = GameObject.Instantiate(m_laserPrefab, transform.position + transform.forward * 20, transform.rotation) as GameObject;
        StartCoroutine(CoolDown());
        
        // Destroys itself after 3seconds
        GameObject.Destroy(go, 3f);
    }
}
