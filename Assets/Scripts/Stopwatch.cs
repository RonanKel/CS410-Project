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
    // Set the current time to 0
    void Start()
    {
        currentTime = 0; // 
    }

    // Update is called once per frame
    // If the stopwatch is active, increase the current time and display it
    void Update()
    {
        if (stopwatchActive == true) {
            currentTime = currentTime + Time.deltaTime; // Countup
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = "stopwatch: " + time.ToString(@"mm\:ss\:fff");
    }
    // Start the stopwatch
    public void StartStopwatch(){
        stopwatchActive = true;
    }
    // Stop the stopwatch
    public void StopStopwatch() {
        stopwatchActive = false;
    }
}
