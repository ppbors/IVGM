using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public Canvas MenuCanvas;
    public Canvas GameCanvas;

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

    public void ShowMenu() { showMenu(true); }

    //Enable/disable menu rendering
    private void showMenu(bool Enabled)
    {
        MenuCanvas.GetComponent<MenuControl>().Button1Text.text = gameRunning ? "Continue" : "Start";
        MenuCanvas.gameObject.SetActive(Enabled);
        menuShown = Enabled;

        showHUD(!Enabled);
    }

    //Enable/disable HUD rendering
    private void showHUD(bool Enabled)
    {
        GameCanvas.gameObject.SetActive(Enabled);
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
        if (Input.GetKey(KeyCode.Escape))
        {
            if (menuShown)
                Application.Quit();//Ignored in editor
            else
                showMenu(true);//Re-enable menu
        }
    }
}
