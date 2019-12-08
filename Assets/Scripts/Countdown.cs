using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using TMPro;

public class Countdown : MonoBehaviour
{
    private int timeLeft = 3;
    public TextMeshProUGUI countdown;
    private bool started = false;
    private bool stopped = false;

    void Start()
    {
        Time.timeScale = 1;
    }

    void Update()
    {
        if (started)
            countdown.text = ("" + timeLeft);
        if (timeLeft < 0)
        {
            Destroy(gameObject);
            stopped = true;
        }
    }

    public void startCountdown()
    {
        started = true;
        StartCoroutine("LoseTime");
    }

    public bool finished()
    {
        return stopped;
    }

    private IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
    }
}