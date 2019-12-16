using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollHandler : MonoBehaviour
{

    public GameManagerScript GameManager;
    public Text ScrollingText;

    private bool isScrolling; // We'll use this for debugging
    private float rotation;   // Default 55deg, but read in from canvas

    // Start is called before the first frame update
    void Start()
    {
    }

    public void StartScroll()
    {
        GameManager.ShowMenuChildren(false);
        Setup();
    }


    // Update is called once per frame
    void Update()
    {

        // If we are scrolling, perform update action
        if (isScrolling)
        {
            Vector3 _currentUIPosition = gameObject.transform.position;

            // Get the current transform position of the panel
            //Debug.Log("Current Positon: " + _currentUIPosition);

            // Increment the Y value of the panel 
            Vector3 _incrementYPosition =
              new Vector3(_currentUIPosition.x,
                          _currentUIPosition.y,
                          _currentUIPosition.z + 0.15f);

            // Change the transform position to the new one
            Debug.Log("New Position: " + _incrementYPosition);
            gameObject.transform.position = _incrementYPosition;

            if (_currentUIPosition.z > 450)
            {
                Color x = ScrollingText.color;
                x.a -= 0.01f;
                ScrollingText.color = x;

                if(x.a <= 0f)
                {
                    this.gameObject.SetActive(false);
                    GameManager.StartGame();
                }
                    
            }
        }

       
            //startFade();
    }

    // Set up the initial variables
    void Setup()
    {
        this.gameObject.SetActive(true);

        isScrolling = true;
        rotation = gameObject.GetComponentInParent<Transform>().eulerAngles.x;
        Debug.Log("Parent rotation: " + rotation);
    }


}
