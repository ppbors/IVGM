using UnityEngine;
using System.Collections;

public class CannonBehavior : MonoBehaviour {

    public GameObject m_laserPrefab;
   // public GameManagerScript gm;



    public void Fire(float size)
    {
     
        GameObject go = GameObject.Instantiate(m_laserPrefab, transform.position, transform.rotation) as GameObject;
        go.transform.localScale = new Vector3(go.transform.localScale.x * size, go.transform.localScale.y * size,
            go.transform.localScale.z * (size/10)) ;

        // Destroys itself after 3 seconds
        GameObject.Destroy(go, 3f);
    }

 
}
