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
            Player.gameObject.SetActive(true);
            gameRunning = true;
            InitialSpawnAsteroids(spawnSizeAsteroids);

        }
        else // Actions taken only when proceeding from paused game
        {
            Player.Freeze(false);
            FreezeAsteroids(false);

        }

        // Initialize the player object: start thrusters
        Player.ThrusterPlay();

        // Call updateSpawnAsteroids after 2 seconds. And then every 2 seconds.
        // Turn off with CancelInvoke
        InvokeRepeating("UpdateSpawnAsteroids", 2.0f, 2.0f);

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
        CancelInvoke();
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


    // Update Asteroid positions to keep having asteroids throughout the game
    // Turn this off to stop 
    private void UpdateSpawnAsteroids()
    {
        foreach(GameObject asteroid in Asteroids)
        {
            float x = Mathf.Abs(asteroid.transform.position.x);
            float y = Mathf.Abs(asteroid.transform.position.y);
            float z = Mathf.Abs(asteroid.transform.position.z);
            float boundary_x = Mathf.Abs(Player.transform.position.x) + Boundary.xMax;
            float boundary_y = Mathf.Abs(Player.transform.position.y) + Boundary.yMax;
            float boundary_z = Mathf.Abs(Player.transform.position.z) + Boundary.zMax;
            if (Vector3.Distance(asteroid.transform.position, Player.transform.position) > 350)
            {
                if (asteroid.GetComponent<Rigidbody>().IsSleeping())
                    asteroid.transform.position = Player.transform.forward * 300;
                else
                    asteroid.transform.position = -1.0f * asteroid.transform.position;
            }
        }
    }

    // Spawns random asteroids in the entirety of the world at random places 
    private void InitialSpawnAsteroids(uint n)
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
