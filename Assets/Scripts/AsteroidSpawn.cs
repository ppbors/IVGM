using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawn : MonoBehaviour
{
    public GameObject player;
    public GameObject[] AsteroidPrefabs;
    private List<GameObject> Asteroids;

    private bool spawn;
    private bool pause;
    private const uint spawnSizeAsteroids = 300;
    private uint spawnCount = 0;
    private uint spawnRate = 1; // Asteroids spawn per second

    private float partBound = 10.0f;

    private void Start()
    {
        spawn = false;
        Asteroids = new List<GameObject>();
    }

    private void Update()
    {
        if (spawn && !pause)
        {
            SpawnAsteroids(spawnRate);
            spawnCount += spawnRate;
            if (spawnCount > spawnSizeAsteroids)
                spawn = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player && !spawn)
        {
            spawn = true;
            player.GetComponent<PlayerController>().GiveTip(2);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("Asteroid"))
            Destroy(other.gameObject);
    }


    // Spawns random asteroids in the entirety of the world at random places 
    private void SpawnAsteroids(uint n)
    {
        for (int i = 0; i < n; ++i)
        {
            /* Lets just assume no two asteroids get spawned too close to eachother */
            Vector3 position = new Vector3(Random.Range(Boundary.xMin/partBound, Boundary.xMax/partBound),
                                           Random.Range(Boundary.yMin/partBound, Boundary.yMax/partBound),
                                           Random.Range(Boundary.zMin/partBound, Boundary.zMax/partBound));

            position += transform.localPosition;
            position += transform.forward * 100;
            GameObject asteroid = Instantiate(AsteroidPrefabs[Random.Range(0, 3)], position, Quaternion.identity);
            asteroid.transform.SetParent(this.gameObject.transform);
            asteroid.transform.LookAt(player.transform);
            Asteroids.Add(asteroid);
        }
    }

    public void PauseSpawn(bool pause)
    {
        this.pause = pause;
    }

}
