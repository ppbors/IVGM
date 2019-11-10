using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


/* 
 * For easy control over menu parameters
 */
public class MenuControl : MonoBehaviour
{
    public Canvas OptionsCanvas;

    public TextMeshProUGUI Button1Text;

    public void ShowOptions(bool enabled = true) => 
        OptionsCanvas.gameObject.SetActive(enabled);
}
