using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;


public class GameEnding : MonoBehaviour
{
    bool timerActive = false;
    public float currentTime;
    public int startMinutes;
    public TMP_Text currentTimeText;
    public GameObject victoryScreen;
    public GameObject player;
    public GameObject scoreboard;

    private GameManager gameManager;
    private World world;


    // Start is called before the first frame update
    void Start()
    {
        currentTime = startMinutes * 60; // Converting start time into seconds
        timerActive = true;
        //Debug.Log("START!");

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        world = GameObject.Find("World").GetComponent<World>();

    }

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StopTimer();
            //Debug.Log("You Win!!!");
            victoryScreen.SetActive(true);
            scoreboard.GetComponent<Scoreboard>().SaveScore(((startMinutes * 60f) - currentTime));
            scoreboard.SetActive(true);
            world.SetRotationStatus(false);
            gameManager.UnhideMouse();
            gameManager.SetPausability(false);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive == true) {
            currentTime = currentTime - Time.deltaTime; // Countdown
            if (currentTime <= 0) { // Once timer reaches 0 it will stop
                StopTimer();
                player.GetComponent<GameBallScript>().RestartLevel();
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
