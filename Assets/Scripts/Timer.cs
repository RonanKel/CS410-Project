using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    bool timerActive = false;
    float currentTime;
    public int startMinutes;
    public TMP_Text currentTimeText;

    // Start is called before the first frame update
    // Set the current time to the start time in seconds
    void Start()
    {
        currentTime = startMinutes * 60; // Converting start time into seconds
        
    }

    // Update is called once per frame
    // If the timer is active, decrease the current time and display it
    void Update()
    {
        if (timerActive == true) {
            currentTime = currentTime - Time.deltaTime; // Countdown
            if (currentTime <= 0) { // Once timer reaches 0 it will stop
                timerActive = false;
            }
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = "Timer: " + time.ToString(@"mm\:ss");
    }
    // Start the timer
    public void StartTimer(){
        timerActive = true;
    }
    // Stop the timer
    public void StopTimer() {
        timerActive = false;
    }
}
