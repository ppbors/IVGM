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
    private bool stop = false;

    void Start()
    {
        Time.timeScale = 1;
    }

    void Update()
    {
        if (timeLeft == 0 && !stop)
        {
            stop = true;
            this.transform.position = new Vector3(this.transform.position.x - 100, this.transform.position.y, 0);
        }
        if (timeLeft == 0)
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y+5,0);
        if (started)
            countdown.text = (timeLeft==0?"Start!":""+timeLeft);

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
            this.GetComponent<AudioSource>().Play();
            if (timeLeft==0)
                this.GetComponents<AudioSource>()[1].Play();
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
    }
}