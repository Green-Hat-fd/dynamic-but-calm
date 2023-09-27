using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Canvas mainMenuCanvas;
    public Canvas comandiCanvas;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        comandiCanvas.gameObject.SetActive(false);
    }

    public void PlayGame()
    {
        Debug.Log("Maria è fantastica");
        SceneManager.LoadScene("Main game");
    }

    public void ComandiMenu()
    {
        comandiCanvas.gameObject.SetActive(true);
        mainMenuCanvas.gameObject.SetActive(false);
    }

    public void BackToMainMenu()
    {
        comandiCanvas.gameObject.SetActive(false);
        mainMenuCanvas.gameObject.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}