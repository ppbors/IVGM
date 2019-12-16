using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;


public class GameManagerScript : MonoBehaviour
{
    public Canvas MenuCanvas;
    public Canvas GameCanvas;

    public PlayerController Player;
    public AsteroidSpawn AsteroidSpawn;
    public Countdown countdown;
    public WifiAnimation wifi;

    public BossEncounter be;

    public GameObject EnemyPrefab; /* dummy */

    private bool menuShown; // If menu is shown
    private bool gameRunning; //If game has started
    private bool gamePaused; // If game is paused

    private const int x_Res = 1024;
    private const int y_Res = 576;

    int gamestate = 0; // 1 = in 1st boss battle, 2 = after 1st boss battle, 3 = in 2nd boss battle, 4 = after 2nd boss battle, 5 = in last boss battle

    

    //Init game here, runs when start button is clicked
    public void StartGame()
    {
        ShowMenu(false);

        // Initialize game variables here
        ShowHUD();

        // Only toggle ship appearance at start
        if (!gameRunning) // Specific actions taken at new game
        {
            Player.Hide(false);
            gameRunning = true;
            

            be = Instantiate(be, new Vector3(0, 0, 3000), Quaternion.identity).GetComponent<BossEncounter>();
            be.SpawnBoss(0); // The first boss

            wifi.reset(Player.GetCoordinates());


            //GameObject go = Instantiate(EnemyPrefab, (Player.transform.position + new Vector3(0, 0, 50)), Quaternion.identity); /* dummy */
            // countdown.startCountdown();
        }
        else 
        {
            // Actions taken only when proceeding from paused game
        }
        

        // Start game time
        Time.timeScale = 1;
        gamePaused = false;

        // Enable spawning of new Asteroids
        AsteroidSpawn.PauseSpawn(gamePaused);
    }

    public void HandleBossDefeat() // Should be called by boss object after destruction
    {
        // Based on the current game state, determine what to do
    }

    // Stops game and returns to game menu
    public void StopGame()
    {
        gameRunning = false;

        // Stopping of the game, a.k.a reset
        // Reset game params
        // Show start menu
        Time.timeScale = 0; // Pause game time
    }

    // Paused game state, opens menu by default
    public void PauseGame()
    {
        // Pause game time
        Time.timeScale = 0;

        gamePaused = true;
        
        ShowHUD(false);
        ShowMenu();

        // Stop spawning new asteroids while game is paused
        AsteroidSpawn.PauseSpawn(gamePaused);
    }

    public bool IsPaused() => gamePaused;
    public bool IsRunning() => gameRunning;

    public void ShowMenuChildren(bool enabled = true)
    {
        foreach(Transform child in MenuCanvas.transform)
        {
            child.gameObject.SetActive(enabled);
        }
    }

    // Enable/disable menu rendering
    public void ShowMenu(bool enabled = true)
    {
        MenuCanvas.GetComponent<MenuControl>().Button1Text.text = gamePaused ? "Continue" : "Start";
        MenuCanvas.gameObject.SetActive(enabled);
        menuShown = enabled;
        Cursor.visible = enabled;
    }

    // Enable/disable HUD rendering
    private void ShowHUD(bool enabled = true)
    {
        GameCanvas.gameObject.SetActive(enabled);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set to specified resolution
        Screen.SetResolution(x_Res, y_Res, true);
        gameRunning = false;
        gamePaused = false;

        //Asteroids = new List<GameObject>();
        Player.Hide();
        ShowMenu();
    }

    // Update is called once per frame
    void Update()
    {
        // Open/close menu with 'm' key
        if(Input.GetKeyDown("m"))
        {
            if (menuShown)
                StartGame();
            else
                PauseGame();
        }
    }

    public Vector3 GetCurrentSignalSource()
    {
        GameObject go = GameObject.Find("BossEnvironment");

        if (!go)
            return new Vector3(0, 0, 0); // Very short boss to boss transition

        BossEncounter be = go.GetComponent<BossEncounter>();
        return be.GetCoordinates();
    }

    public Vector3 GetPlayerCoordinates()
    {
        return Player.GetCoordinates();
    }

}
