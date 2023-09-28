using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        IPlayer playerCheck = collision.GetComponent<IPlayer>();

        if (playerCheck == null)
        {
            return;
        }

        SceneManager.LoadScene("Win");
    }
}
