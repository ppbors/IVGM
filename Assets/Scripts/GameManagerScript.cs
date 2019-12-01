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

    private List <GameObject> Asteroids;
    public GameObject[] AsteroidPrefabs;

    private bool menuShown; // If menu is shown
    private bool gameRunning; //If game has started
    private bool gamePaused; // If game is paused

    private const int x_Res = 1024;
    private const int y_Res = 576;

    private const uint spawnSizeAsteroids = 700;

    //Init game here, runs when start button is clicked
    public void StartGame()
    {
        ShowMenu(false);

        //Init game var values here ...
        ShowHUD();

        // Only toggle ship appearance at start
        if (!gameRunning) // Specific actions taken at new game
        {
            Player.Hide(false);
            gameRunning = true;
            SpawnAsteroids(spawnSizeAsteroids);
        }
        else // Actions taken only when proceeding from paused game
        {
            Player.Freeze(false);
            FreezeAsteroids(false);
        }


        // Initialize the player object: start thrusters
        Player.ThrusterPlay();

        gamePaused = false;
    }

    // Stops game and returns to game menu
    public void StopGame()
    {
        gameRunning = false;

        // Stopping of the game, a.k.a reset
        // Reset game params
        // Show start menu
    }

    // Paused game state, opens menu by default
    public void PauseGame()
    {
        // Leave player in screen, just pause the exhaust particle effect
        Player.ThrusterPause();
        Player.Freeze();
        FreezeAsteroids();
        gamePaused = true;
        ShowHUD(false);
        ShowMenu();
    }

    public bool IsPaused() => gamePaused;
    public bool IsRunning() => gameRunning;


    // Enable/disable menu rendering
    public void ShowMenu(bool enabled = true)
    {
        MenuCanvas.GetComponent<MenuControl>().Button1Text.text = gamePaused ? "Continue" : "Start";
        MenuCanvas.gameObject.SetActive(enabled);
        menuShown = enabled;
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
        Asteroids = new List<GameObject>();
        Player.Hide();
        ShowMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (menuShown)
                Application.Quit();//Ignored in editor
            else
                ShowMenu();//Re-enable menu
        }
    }

    // Spawns random asteroids in the entirety of the world at random places 
    private void SpawnAsteroids(uint n)
    {
        for (int i = 0; i < n; ++i)
        {
            /* Lets just assume no two asteroids get spawned too close to eachother */
            Vector3 position = new Vector3(Random.Range(Boundary.xMin, Boundary.xMax), 
                                           Random.Range(Boundary.yMin, Boundary.yMax), 
                                           Random.Range(Boundary.zMin, Boundary.zMax));
            Asteroids.Add(
                Instantiate(AsteroidPrefabs[Random.Range(0, 2)], 
                            position, 
                            Quaternion.identity) 
                as GameObject
            );
        }
    }

    private void FreezeAsteroids(bool on = true)
    {
        foreach (GameObject asteroid in Asteroids)
        {
            asteroid.GetComponent<RandomRotator>().Freeze(on);
        }
    }
}
