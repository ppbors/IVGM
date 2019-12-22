using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FlavourTextControl : MonoBehaviour
{
    public Text FlavourText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangeText(string text)
    {
        FlavourText.gameObject.SetActive(true);
        FlavourText.text = text;
        Invoke("Dissapear", 5);
    }

    private void Dissapear()
    {
        if(!IsInvoking("Dissapear"))
            FlavourText.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
