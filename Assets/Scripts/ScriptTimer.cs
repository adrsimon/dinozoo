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
    // affiche à l'ecran les minutes et les secondes
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
    // fonction appelée lors du Game Over pour stoper le timer
    public void StopTimer()
    {
        timerActive = false;
    }
}
