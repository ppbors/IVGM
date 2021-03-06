﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WifiAnimation : MonoBehaviour
{
    public Sprite[] sprites;
    private Image wifiIcon;
    public GameManagerScript gm;

    private Vector3 initialPosition;
    private float barTreshold;

    public void SetSignalStrength(int Strength)
    {
        wifiIcon.sprite = sprites[Strength];
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject go = GameObject.Find("WifiIcon");
        wifiIcon = go.GetComponent<Image>();

        initialPosition = new Vector3();
    }

    public void reset(Vector3 pos) // Should be called after every signal change
    {
        initialPosition = pos;
        // todo Finetuning?
        barTreshold = Vector3.Distance(gm.GetCurrentSignalSource(this.gameObject), initialPosition) - 200 ;
        
        if (barTreshold != 0)
            barTreshold /= 5;
    }

    // Update is called once per frame
    void Update()
    {
        
        //Debug.Log(gm.GetCurrentSignalSource(this.gameObject));
        float distance = Vector3.Distance(gm.GetCurrentSignalSource(this.gameObject), gm.GetPlayerCoordinates()) / barTreshold;
       
        //Debug.Log(Vector3.Distance(gm.GetCurrentSignalSource(this.gameObject), gm.GetPlayerCoordinates()));
        SetSignalStrength(Mathf.Abs(Mathf.Clamp((int) distance, 0, 4) - 4));

        
    }
}
