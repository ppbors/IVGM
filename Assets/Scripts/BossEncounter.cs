using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEncounter : MonoBehaviour
{
    public List<GameObject> BossModels;
    public GameManagerScript gm;
    private BoxCollider bc;
    private EnemyControl boss;

    // Start is called before the first frame update
    void Awake()
    {
        bc = GetComponent<BoxCollider>();
        gm = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!boss)
            return;
    }

    public void SpawnBoss(int i)
    {
        if (i > BossModels.Count - 1)
            return;

        boss = Instantiate(BossModels[i], GetCoordinates(), Quaternion.identity).GetComponent<EnemyControl>();
        boss.transform.LookAt(gm.GetPlayerCoordinates());
    }

    public Vector3 GetCoordinates()
    {
        return transform.TransformPoint(bc.center);
    }
}
