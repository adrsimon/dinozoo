using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScriptTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float startTime;
    private bool timerActive = true;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        if (timerActive)
        {
            float t = Time.time - startTime;
            string minutes = ((int)t / 60).ToString();
            string seconds = (t % 60).ToString("f2");
            timerText.text = minutes + ":" + seconds;
        }
    }
    public void StopTimer()
    {
        timerActive = false;
    }
}
