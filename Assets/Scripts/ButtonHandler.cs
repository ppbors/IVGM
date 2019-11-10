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
        gm = go.GetComponent<GameManagerScript>();
    }

    public void StartClicked()
    {
        gm.StartGame();
    }

    public void OptionsClicked()
    {
        //Todo...
    }

    public void ExitClicked()
    {
        Application.Quit();//Ignored in editor
    }

    public void MenuClicked()
    {
        gm.ShowMenu();
    }

}
