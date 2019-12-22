using UnityEngine;
using System.Collections;

public class ShotBehavior : MonoBehaviour {

    public ParticleSystem explosion;
    private readonly float explosionSize = 3000.0f;

	// Use this for initialization
	void Start ()
    {

        float dist = Mathf.Log(Vector3.Distance(GameObject.Find("Player").transform.position, transform.position));
        if (dist <= 6f) {
            this.GetComponent<AudioSource>().volume =
                Mathf.Min(100, Mathf.Max(0, 6-dist)*40)
            / 100.0f;
            this.GetComponent<AudioSource>().Play();
        }
        var main = explosion.main;
        main.startSize = explosionSize;
    }
	
	// Update is called once per frame
	void Update ()
    {
        // change to change speed of laser
		transform.position += transform.forward * Time.deltaTime * 150;
	}

    private void OnTriggerEnter(Collider other)
    {
        
        // shot destroys itself once it hits something with a trigger.
        //if (!other.gameObject.name.Contains("Spawn"))
            //GameObject.Destroy(this.gameObject);
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Player")
        {
            float health = -1 * gameObject.transform.localScale.x * 10;
            other.gameObject.GetComponent<PlayerController>().AddHealth(health);
        }

        if (other.gameObject.name.Contains("Asteroid"))
            other.gameObject.GetComponent<AsteroidBehavior>().ChangeSize();
        if (other.gameObject.name.Contains("Enemy") || other.gameObject.name.Contains("Boss"))
            if (other.gameObject.GetComponent<EnemyControl>()!=null)
                other.gameObject.GetComponent<EnemyControl>().DecreaseHealth();
        explosion = GameObject.Instantiate(explosion, transform.position, transform.rotation) as ParticleSystem;
        GameObject.Destroy(this.gameObject, 0.2f);
    }

}
