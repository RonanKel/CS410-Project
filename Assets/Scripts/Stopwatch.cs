using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Stopwatch : MonoBehaviour
{
    bool stopwatchActive = false;
    float currentTime;
    public TMP_Text currentTimeText;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0; // 
    }

    // Update is called once per frame
    void Update()
    {
        if (stopwatchActive == true) {
            currentTime = currentTime + Time.deltaTime; // Countup
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = "stopwatch: " + time.ToString(@"mm\:ss\:fff");
    }

    public void StartStopwatch(){
        stopwatchActive = true;
    }

    public void StopStopwatch() {
        stopwatchActive = false;
    }
}
