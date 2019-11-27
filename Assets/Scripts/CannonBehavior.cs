using UnityEngine;
using System.Collections;

public class CannonBehavior : MonoBehaviour {

	public GameObject m_laserPrefab;
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
            // Instantiate laser
			GameObject go = GameObject.Instantiate(m_laserPrefab, this.transform.position, this.transform.rotation) as GameObject;
            
            // Destroys itself after 3seconds
            GameObject.Destroy(go, 3f);
		}
	}

   

}
