using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLose : MonoBehaviour
{
    public Canvas mainWin;
    public Canvas crediti;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        crediti.gameObject.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Crediti()
    {
        crediti.gameObject.SetActive(true);
        mainWin.gameObject.SetActive(false);
    }

    public void CloseCrediti()
    {
        crediti.gameObject.SetActive(false);
        mainWin.gameObject.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
