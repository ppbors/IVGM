using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawn : MonoBehaviour
{
    public GameObject player;
    public GameObject[] AsteroidPrefabs;
    private List<GameObject> Asteroids;

    private bool spawn;
    private bool freeze;
    private const uint spawnSizeAsteroids = 400;
    private uint spawnCount = 0;
    private uint spawnRate = 1; // Asteroids spawn per second

    private void Start()
    {
        spawn = false;
        Asteroids = new List<GameObject>();
    }

    private void Update()
    {
        if (spawn )
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
            spawn = true;        
    }

    /*
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player && spawn)
            spawn = false;
    }*/


    // Spawns random asteroids in the entirety of the world at random places 
    private void SpawnAsteroids(uint n)
    {
        for (int i = 0; i < n; ++i)
        {
            /* Lets just assume no two asteroids get spawned too close to eachother */
            Vector3 position = new Vector3(Random.Range(Boundary.xMin/10.0f, Boundary.xMax/10.0f),
                                           Random.Range(Boundary.yMin/10.0f, Boundary.yMax/10.0f),
                                           Random.Range(Boundary.zMin/10.0f, Boundary.zMax/10.0f));

            position += transform.forward * 1000;
            GameObject asteroid = Instantiate(AsteroidPrefabs[Random.Range(0, 3)], position, Quaternion.identity);
            //asteroid.transform.SetParent(this.gameObject.transform);
            asteroid.transform.LookAt(transform.forward * -1000);
            Asteroids.Add(asteroid);
        }
    }

    public void FreezeAsteroids(bool on = true)
    {
        freeze = on;

        foreach (GameObject asteroid in Asteroids)
        {
            asteroid.GetComponent<AsteroidBehavior>().Freeze(on);
        }
    }
}
