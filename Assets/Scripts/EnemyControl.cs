using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public float sizeLaser;

    /* Enemy distance triggers */

    public float maxDist = 1000.0f;
    public float fireDist = 1000.0f;
    public bool isBoss;

    /* --- */

    private Rigidbody rb;
    public ParticleSystem exhaust;

    public List<CannonBehavior> cannon;
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



    private bool cannonOnCooldown = true;
    private float fireRate = 5.0f; 

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

    int health = 100;
    bool fireLeft = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        gm = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        rb = gameObject.GetComponent<Rigidbody>();
        rb.mass = mass;
        rb.drag = drag;

        transform.LookAt(player.transform);
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
        
        if (health <= 0)
        {
            Debug.Log("DEAD");
            if (isBoss)
                gm.HandleBossDefeat();
            else
            {
                gm.HandleEnemyDefeat();
                Destroy(this.gameObject);
            }
                
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


    public void DecreaseHealth()
    {
        
        if (isBoss)
            health -= 100;
        else
            health -= 20;
    }

    private void Fire()
    {
        int fireFrom = 0;
        // Instantiate laser, spawn slightly in front
        cannonOnCooldown = true;
        if (cannon.Count > 1)
        {
            fireFrom = (fireLeft) ? 0 : 1;
            cannon[fireFrom].gameObject.transform.LookAt(player.transform);
        }
        cannon[fireFrom].Fire(sizeLaser);
        fireLeft = !fireLeft;
        StartCoroutine(CoolDown());
        
    }
}
