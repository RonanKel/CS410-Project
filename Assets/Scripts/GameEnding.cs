using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;


public class GameEnding : MonoBehaviour
{
    bool timerActive = false;
    float currentTime;
    public int startMinutes;
    public TMP_Text currentTimeText;


     public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startMinutes * 60; // Converting start time into seconds
        timerActive = true;
        Debug.Log("START!");

    }

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StopTimer();
            Debug.Log("You Win!!!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive == true) {
            currentTime = currentTime - Time.deltaTime; // Countdown
            if (currentTime <= 0) { // Once timer reaches 0 it will stop
                StopTimer();
            }
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = "Timer: " + time.ToString(@"mm\:ss");
    }

    public void StartTimer(){
        timerActive = true;
    }

    public void StopTimer() {
        timerActive = false;
    }
}