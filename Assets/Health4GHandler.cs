using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health4GHandler : MonoBehaviour
{
    public PlayerController player;
    public Slider m_Slider;
    public Image m_BackGround;
    public Gradient m_MaxMinHealth;

    // Start is called before the first frame update
    void Start()
    {
        m_Slider.value = 100;
        m_BackGround.color = m_MaxMinHealth.Evaluate(1);

    }

    private void Update()
    {
        float x = player.getHealth();
        
        m_Slider.value = x;
        m_BackGround.color = m_MaxMinHealth.Evaluate(x / 100);
    }

}
