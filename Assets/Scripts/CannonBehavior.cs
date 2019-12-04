﻿using UnityEngine;
using System.Collections;

public class CannonBehavior : MonoBehaviour {

    public GameObject m_laserPrefab;
    public GameManagerScript gm;

    // Update is called once per frame
    void Update()
    {
        // Do not shoot laser when game is paused
        if(gm.IsPaused() || !gm.IsRunning())
        {
            return;
        }
        // Shoot laser when SPACE or left-mouse button is pressed
        else if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) 
		{
            // Instantiate laser
			GameObject go = GameObject.Instantiate(m_laserPrefab, transform.position, transform.rotation) as GameObject;

            // Destroys itself after 3seconds
            GameObject.Destroy(go, 3f);
		}
	}
}
