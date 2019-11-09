using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    private GameManagerScript gm;

    // Start is called before the first frame update
    void Start()
    {
        GameObject go = GameObject.Find("GameManager");
        gm = (GameManagerScript)go.GetComponent(typeof(GameManagerScript));
    }

    public void StartClicked()
    {
        gm.StartGame();
    }
}
