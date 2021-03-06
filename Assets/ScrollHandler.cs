﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollHandler : MonoBehaviour
{

    public GameManagerScript GameManager;
    public Text ScrollingText;

    private bool isScrolling; // extra check to not scroll while Start is not called
    private float rotation;   // Default 55deg, but read in from canvas

    bool hasplayed = false;

    public void StartScroll()
    {
        if (hasplayed)
        {
            GameManager.StartGame();
            return;
        }

        GameManager.ShowMenuChildren(false);
        hasplayed = true;
        Setup();
        
    }


    // Update is called once per frame
    void Update()
    {
        // Stop scrolling text if game is paused
        if(GameManager.IsPaused())
        {
            return;
        }

        // Skip text scrolling when 'z' button is pressed
        if (Input.GetKeyDown("z")){
            this.gameObject.SetActive(false);
            GameManager.ShowMenuChildren(true);
            GameManager.StartGame();
        }

        // If we are scrolling, perform update action
        if (isScrolling)
        {
            Vector3 _currentUIPosition = ScrollingText.transform.position;

            // Increment the Y value of the panel 
            Vector3 _incrementYPosition =
              new Vector3(_currentUIPosition.x,
                          _currentUIPosition.y,
                          _currentUIPosition.z + 0.2f);

            // Change the transform position to the new one
            ScrollingText.transform.position = _incrementYPosition;

            // start fade when z > 450 (hardcoded)
            if (_currentUIPosition.z > 430)
            {
                Color x = ScrollingText.color;
                x.a -= 0.01f;
                ScrollingText.color = x;

                
                // Start game when text is transparent
                if(x.a <= 0f)
                {
                    this.gameObject.SetActive(false);
                    GameManager.ShowMenuChildren(true);
                    GameManager.StartGame();
                }
                    
            }
        }
    }

    // Set up the initial variables
    void Setup()
    {
        this.gameObject.SetActive(true);

        isScrolling = true;
        rotation = gameObject.GetComponentInParent<Transform>().eulerAngles.x;
    }


}
