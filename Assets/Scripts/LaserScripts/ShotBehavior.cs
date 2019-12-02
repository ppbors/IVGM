using UnityEngine;
using System.Collections;

public class ShotBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        // change to change speed of laser
		transform.position += transform.forward * Time.deltaTime * 100f;
	}

    private void OnTriggerEnter(Collider other)
    {
        // shot destroys itself once it hits something with a trigger.
        if(!other.gameObject.name.Contains("Spawn"))
            GameObject.Destroy(this.gameObject);
    }

}
