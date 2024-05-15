using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    GameObject pauseMenu;
    // Start is called before the first frame update

    void Awake()
    {
        pauseMenu = GameObject.Find("Pause Menu") ? GameObject.Find("Pause Menu") : null;

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
        if (Input.GetKeyDown(KeyCode.Escape)) {
            PauseGame();
        }
    }

    // Menu Features
    public void PauseGame() {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        // Lock the cursor when the game is running
        UnhideMouse();
    }

    public void UnpauseGame() {
        pauseMenu.SetActive(false);
        ReturnToGame();
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