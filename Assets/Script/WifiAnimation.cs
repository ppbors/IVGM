using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WifiAnimation : MonoBehaviour
{
    public Sprite[] sprites;
    private Image wifiIcon;

    /*  WifiAnimation wa = (WifiAnimation)GameObject.Find("WifiIcon").GetComponent(typeof(WifiAnimation));
     *  wa.SetSignalStrength(Random.Range(0, 5)); */
    public void SetSignalStrength(int Strength)
    {
        wifiIcon.sprite = sprites[Strength];
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject go = GameObject.Find("WifiIcon");
        wifiIcon = (Image)go.GetComponent(typeof(Image));
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % 60==0)
            SetSignalStrength(Random.Range(0, 5));
    }
}
