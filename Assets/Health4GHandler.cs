using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health4GHandler : MonoBehaviour
{
    public Slider m_Slider;
    public Image m_BackGround;
    public Gradient m_MaxMinHealth;

    // Start is called before the first frame update
    void Start()
    {
        m_Slider.value = 100;
        m_BackGround.color = m_MaxMinHealth.Evaluate(1);

        InvokeRepeating("DecreaseAutomatically", 1, 1);
    }

    public void DecreaseAutomatically()
    {
        ChangeHealth(-1);
    }

    public void ChangeHealth(int amount)
    {
        m_Slider.value += amount;
        m_BackGround.color = m_MaxMinHealth.Evaluate(m_Slider.value / 100);
    }


}
