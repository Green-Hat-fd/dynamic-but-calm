using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpTimer : PowerUp
{
    private float timeSpeed;

    public void SlowsTime()
    {
        StartCoroutine(SlowsTimeFor()); 
    }

    private IEnumerator SlowsTimeFor()
    {
        timeSpeed = 0.5f;

        Time.timeScale = timeSpeed;

        // funzione per raddoppiare velocita

        yield return new WaitForSeconds(2f);

        timeSpeed = 1f;

        Time.timeScale = timeSpeed;

       // funzione per ripristinare la velocita
    }
}
