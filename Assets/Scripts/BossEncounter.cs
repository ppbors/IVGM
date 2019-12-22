using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEncounter : MonoBehaviour
{
    public List<GameObject> BossModels;
    public GameManagerScript gm;
    private BoxCollider bc;
    private EnemyControl boss;

    private bool tipGiven = false;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == gm.GetPlayer() && !tipGiven)
        {
            gm.GiveTip(3);
            tipGiven = true;
        }
    }

    public void SpawnBoss(int i)
    {
        if (i > BossModels.Count - 1)
            return;

        boss = Instantiate(BossModels[i], GetCoordinates(), Quaternion.identity).GetComponent<EnemyControl>();
        boss.transform.LookAt(gm.GetPlayerCoordinates());
        boss.transform.SetParent(gameObject.transform);
    }

    public Vector3 GetCoordinates()
    {
        return transform.TransformPoint(bc.center);
    }
}
