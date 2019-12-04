using UnityEngine;
using System.Collections;

public class CannonBehavior : MonoBehaviour {

	public GameObject m_laserPrefab;
    public GameManagerScript gm;
	
	// Update is called once per frame
	void Update() 
	{
        // Do not shoot laser when game is paused
        if(gm.IsPaused())
        {
            return;
        }
        else if(Input.GetKeyDown(KeyCode.Space))
		{
            // Instantiate laser
			GameObject go = GameObject.Instantiate(m_laserPrefab, transform.position, transform.rotation) as GameObject;

            // Destroys itself after 3seconds
            GameObject.Destroy(go, 3f);
		}
	}
}
