using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{ 
    public DeathManager deathMng;

    public Canvas pauseMenu;
    public Canvas hud;

    public bool isPaused = false;

    private void Start()
    {
        pauseMenu.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            deathMng.ActivateScripts(false);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            pauseMenu.gameObject.SetActive(true);
            hud.gameObject.SetActive(false);
        }
        else
        {
            deathMng.ActivateScripts(true);
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            pauseMenu.gameObject.SetActive(false);
            hud.gameObject.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        TogglePause();
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
