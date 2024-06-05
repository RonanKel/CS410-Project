using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    GameObject pauseMenu;
    GameObject scoreboard;

    bool isPaused = false;
    bool isPausable = true;
    // Start is called before the first frame update

    void Awake()
    {
        pauseMenu = GameObject.Find("Pause Menu") ? GameObject.Find("Pause Menu") : null;
        scoreboard = GameObject.Find("Scoreboard") ? GameObject.Find("Scoreboard") : null;

        // Lock the cursor when the game is running
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Start()
    {
        if (pauseMenu) {
            UnpauseGame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && isPausable) {
            if (isPaused) {
                UnpauseGame();
            }
            else {
                PauseGame();
            }
        }
    }

    // Menu Features
    public void PauseGame() {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        isPaused = true;
        if (scoreboard != null) {
            scoreboard.SetActive(true);
        }
        // Lock the cursor when the game is running
        UnhideMouse();
    }

    public void UnpauseGame() {
        pauseMenu.SetActive(false);
        isPaused = false;
        if (scoreboard != null) {
            scoreboard.SetActive(false);
        }
        ReturnToGame();
    }

    public void SetPausability(bool status) {
        isPausable = status;
    }

    public void ReturnToGame() {
        Time.timeScale = 1f;
        HideMouse();
        
    }

    public void RestartLevel() {
        ReturnToGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    public void LevelSelect() {
        ReturnToGame();
        SceneManager.LoadScene(0);
        
    }

    public void ExitGame() {
        Application.Quit();
    }

    //Mouse Features
    public void HideMouse() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UnhideMouse() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
