using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Canvas mainMenuCanvas;
    public Canvas optionCanvas;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        optionCanvas.gameObject.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void OptionMenu()
    {
        optionCanvas.gameObject.SetActive(true);
        mainMenuCanvas.gameObject.SetActive(false);
    }

    public void BackToMainMenu()
    {
        optionCanvas.gameObject.SetActive(false);
        mainMenuCanvas.gameObject.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}