using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public Canvas EndScreenCanvas;

    private GameManagerScript gm;

    // Start is called before the first frame update
    void Start()
    {
        GameObject go = GameObject.Find("GameManager");
        gm = go.GetComponent<GameManagerScript>();
    }

    public void StartClicked() => gm.StartGame();

    public void OptionsClicked()
    {
        gm.ShowMenu(false);
        Cursor.visible = enabled; // Show cursor in Options menu

        // Now show options menu
        gm.MenuCanvas.GetComponent<MenuControl>().ShowOptions(); 
    }

    public void ReturnClicked()
    {
        // Stop showing options menu
        gm.MenuCanvas.GetComponent<MenuControl>().ShowOptions(false);

        // Start showing menu
        gm.ShowMenu();
    }

    public void RestartClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //Application.LoadLevel(Application.loadedLevel);

    }

    public void CreditsClicked()
    {
        // Move Restart button down to make space for credits text
        GameObject restartButton = GameObject.Find("RestartButton");
        Vector3 pos = restartButton.transform.position;
        pos.x -= 100f;
        restartButton.transform.position = pos;

        // Show credits
        EndScreenCanvas.GetComponent<MenuControl>().Button1Text.text = "CREDITS\n\nYenebeb\nPhilippe\nMartijn\nRob\nMohammad Ali";
        EndScreenCanvas.GetComponent<MenuControl>().Button1Text.fontSize = 500f;

        // Hide Credits button
        GameObject.Find("CreditsButton").SetActive(false);
    }

    public void ExitClicked() => Application.Quit(); // Ignored in editor

    public void MenuClicked() => gm.PauseGame();
}
