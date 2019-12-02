using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawn : MonoBehaviour
{
    public GameObject player;
    public GameObject[] AsteroidPrefabs;
    private List<GameObject> Asteroids;

    private bool spawn;
    private const uint spawnSizeAsteroids = 1000;
    private uint spawnCount = 0;
    private void Start()
    {
        spawn = false;
        Asteroids = new List<GameObject>();
    }

    private void Update()
    {
        if (spawn && spawnCount < spawnSizeAsteroids)
        {
            SpawnAsteroids(1);
            spawnCount += 1;
            //spawnCount += x
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Trigger");

        if (other.gameObject == player && !spawn)
            spawn = true;
            
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player && spawn)
            spawn = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("HIT");
        StartSpawn();
    }

    public void StartSpawn()
    {
        this.transform.LookAt(player.transform.position);
        SpawnAsteroids(spawnSizeAsteroids);
    }

    // Spawns random asteroids in the entirety of the world at random places 
    private void SpawnAsteroids(uint n)
    {
        for (int i = 0; i < n; ++i)
        {
            /* Lets just assume no two asteroids get spawned too close to eachother */
            Vector3 position = new Vector3(Random.Range(Boundary.xMin/10.0f, Boundary.xMax/10.0f),
                                           Random.Range(Boundary.yMin/10.0f, Boundary.yMax/10.0f),
                                           Random.Range(Boundary.zMin/10.0f, Boundary.zMax/10.0f));

            position = position + player.transform.position + (player.transform.forward * 500);
            GameObject asteroid = Instantiate(AsteroidPrefabs[Random.Range(0, 2)], position, Quaternion.identity);
            //asteroid.transform.SetParent(this.gameObject.transform);
            asteroid.transform.LookAt(player.transform.forward * -1000);
            Asteroids.Add(asteroid);
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
