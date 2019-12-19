﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;


public class GameManagerScript : MonoBehaviour
{
    public Canvas MenuCanvas;
    public Canvas GameCanvas;
    public Canvas EndScreenCanvas;

    public PlayerController Player;
    public AsteroidSpawn AsteroidSpawn;
    public Countdown countdown;
    public WifiAnimation wifi;

    public BossEncounter be;

    public GameObject EnemyPrefab; /* dummy */

    private bool isMenuShown; // If menu is shown
    private bool isGameRunning; //If game has started
    private bool isGamePaused; // If game is paused
    private bool isGameLost;
    private bool isGameWon;

    private const int x_Res = 1024;
    private const int y_Res = 576;
    private BossEncounter be2;

    // 1 = in 1st boss battle
    // 2 = after 1st boss battle
    // 3 = in 2nd boss battle
    // 4 = after 2nd boss battle
    // 5 = in last boss battle
    int gameState = 0; 

    bool bossDead = false;


    // Initialize game here, runs when Start button is clicked
    public void StartGame()
    {
        ShowMenu(false);

        // Initialize game variables here
        ShowHUD();

        // Only toggle ship appearance at start
        if (!isGameRunning) // Specific actions taken at new game
        {
            Player.Hide(false);
            isGameRunning = true;
            isGameLost = false;
            isGameWon = false;

            be2 = Instantiate(be, new Vector3(0, 0, 2500), Quaternion.identity).GetComponent<BossEncounter>();
            be2.SpawnBoss(0); // The first boss

            wifi.reset(Player.GetCoordinates());


            //GameObject go = Instantiate(EnemyPrefab, (Player.transform.position + new Vector3(0, 0, 50)), Quaternion.identity); /* dummy */
            countdown.startCountdown();
        }
        else 
        {
            // Actions taken only when proceeding from paused game
        }
        

        // Start game time
        Time.timeScale = 1;
        isGamePaused = false;

        // Enable spawning of new Asteroids
        AsteroidSpawn.PauseSpawn(isGamePaused);
    }


    public void AsteroidDestroyed()
    {
        Player.AddHealth(10.0f);
    }


    public void HandleBossDefeat() // Should be called by boss object after destruction
    {
        if (!bossDead)
        {
            Destroy(be2.gameObject);

            // Based on the current game state, determine what to do
            be2 = Instantiate(be, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<BossEncounter>();
            be2.SpawnBoss(1); // The first boss

            wifi.reset(Player.GetCoordinates());
            bossDead = true;
        }
    }


    // Called when game is won
    private void WonGame()
    {
        isGameWon = true;
        StopGame();

        // Show "Won" screen
        EndScreenCanvas.gameObject.SetActive(enabled);

        // Show game menu
        MenuCanvas.GetComponent<MenuControl>().Button1Text.text = "Restart";
        ShowMenuChildren(enabled);
        MenuCanvas.gameObject.SetActive(enabled);
        isMenuShown = enabled;
        Cursor.visible = enabled;
    }


    // Called when game is lost
    private void LostGame()
    {
        isGameLost = true;
        StopGame();

        // Show "Lost" screen
        EndScreenCanvas.gameObject.SetActive(enabled);

        // Show game menu
        MenuCanvas.GetComponent<MenuControl>().Button1Text.text = "Restart";
        ShowMenuChildren(enabled);
        MenuCanvas.gameObject.SetActive(enabled);
        isMenuShown = enabled;
        Cursor.visible = enabled;
    }


    // Stops game and resets game params
    public void StopGame()
    {
        isGameRunning = false;

        AsteroidSpawn.PauseSpawn(true);
        Time.timeScale = 0; // Pause game time
        
        Player.Hide(true);
        isGameRunning = false;
        isGamePaused = true;

        be2 = Instantiate(be, new Vector3(0, 0, 2500), Quaternion.identity).GetComponent<BossEncounter>();
        be2.SpawnBoss(0); // The first boss

        wifi.reset(Player.GetCoordinates());
    }


    // Paused game state, opens menu by default
    public void PauseGame()
    {
        // Pause game time
        Time.timeScale = 0;

        isGamePaused = true;
        
        ShowHUD(false);
        ShowMenu();

        // Stop spawning new asteroids while game is paused
        AsteroidSpawn.PauseSpawn(isGamePaused);
    }


    public bool IsPaused() => isGamePaused;
    public bool IsRunning() => isGameRunning;

    public void ShowMenuChildren(bool enabled = true)
    {
        foreach(Transform child in MenuCanvas.transform)
        {
            if(!child.gameObject.name.Contains("Story"))
                child.gameObject.SetActive(enabled);
        }
    }


    // Enable/disable menu rendering
    public void ShowMenu(bool enabled = true)
    {
        MenuCanvas.GetComponent<MenuControl>().Button1Text.text = isGamePaused ? "Continue" : "Start";
        ShowMenuChildren(enabled);
        MenuCanvas.gameObject.SetActive(enabled);
        isMenuShown = enabled;
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
        isGameRunning = false;
        isGamePaused = false;

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
            if (isMenuShown)
                StartGame();
            else
                PauseGame();
        }
    }

    public Vector3 GetCurrentSignalSource(GameObject go)
    {
        //GameObject go = GameObject.Find("BossEnvironment");

        if (!go)
            return new Vector3(0, 0, 0); // Very short boss to boss transition

        //BossEncounter be2 = go.GetComponent<BossEncounter>();
        return be2.GetCoordinates();
    }

    public Vector3 GetPlayerCoordinates()
    {
        return Player.GetCoordinates();
    }

}
