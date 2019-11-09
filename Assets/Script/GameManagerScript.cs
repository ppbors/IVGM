using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public Canvas UICanvas;

    private bool menuShown;//If menu is shown
    private bool gameRunning;//If game has started

    //Init game here, runs when start button is clicked
    public void StartGame()
    {
        showMenu(false);


        //Init game var values here ...

        gameRunning = true;
        //Debug.Log("GAME STARTED");
    }

    //Stops game and returns to game menu
    public void StopGame()
    {

        gameRunning = false;
        //Debug.Log("GAME STOPPED");
    }

    //Enable/disable menu rendering
    private void showMenu(bool enabled)
    {
        UICanvas.GetComponent<MenuControl>().Button1Text.text = gameRunning ? "Continue" : "Start";
        UICanvas.gameObject.SetActive(enabled);
        menuShown = enabled;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Set to specified resolution
        Screen.SetResolution(1024, 576, true);
        showMenu(true);
    }

    // Update is called once per frame
    void Update()
    {



        if (!menuShown && Input.GetKey(KeyCode.Escape))
            showMenu(true);//Re-enable menu
    }
}
