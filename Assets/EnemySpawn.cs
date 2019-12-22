using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject player;
    public GameObject EnemyPrefab;
    public uint spawnSizeEnemies = 10;

    private bool spawn;
    private bool pause;
    
    private uint spawnCount = 0;
    private uint spawnRate = 1; // Enemies spawn per second

    private float partBound = 30.0f;

    private void Start()
    {
        spawn = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player && !spawn)
        {
            spawn = true;
            player.GetComponent<PlayerController>().GiveTip(1);
        }
    }

    private void Update()
    {
        if (spawn && !pause)
        {
            SpawnEnemies(spawnRate);
            spawnCount += spawnRate;
            if (spawnCount > spawnSizeEnemies)
                spawn = false;
        }

    }



    // Spawns random asteroids in the entirety of the world at random places 
    private void SpawnEnemies(uint n)
    {
        for (int i = 0; i < n; ++i)
        {
            /* Lets just assume no two asteroids get spawned too close to eachother */
            Vector3 position = new Vector3(Random.Range(Boundary.xMin / partBound, Boundary.xMax / partBound),
                                           Random.Range(Boundary.yMin / partBound, Boundary.yMax / partBound),
                                           Random.Range(Boundary.zMin / partBound, Boundary.zMax / partBound));

            position += transform.localPosition;
            GameObject enemy = Instantiate(EnemyPrefab, position, Quaternion.identity);
            enemy.transform.SetParent(this.gameObject.transform);
            enemy.transform.LookAt(player.transform);
        }
    }

    public void PauseSpawn(bool pause)
    {
        this.pause = pause;
    }

}
