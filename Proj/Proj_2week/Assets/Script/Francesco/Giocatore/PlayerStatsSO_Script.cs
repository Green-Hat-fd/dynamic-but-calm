using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Player Stats (S.O.)", fileName = "PlayerStats_SO")]
public class PlayerStatsSO_Script : ScriptableObject
{
    int score;
    bool isCollectableTaken;
    PowerUp powerUp_toUse;
    PowerUp activePowerUp;


    /// <summary>
    /// Ritorna vero se ha messo da parte il power-up
    /// </summary>
    /// <param name="powUp"></param>
    /// <returns></returns>
    public bool PickUpPowerUp(PowerUp powUp)
    {
        //Se non c'e' gia' un power-up messo da parte...
        if (powerUp_toUse == null)
        {
            //Aggiunge il power-up a quello messo da parte
            powerUp_toUse = powUp;

            return true;
        }
        else
        {
            return false;
        }
    }



    /// <summary>
    /// Ritorna il power-up da utilizzare se ce n'è uno 
    /// </summary>
    /// <returns></returns>
    public PowerUp UsePowerUp()
    {
        if (powerUp_toUse != null)
        {
            //Utilizza il power-up messo da parte
            //e lo rimuove da quello messo da parte
            PowerUp _powUpToUse = powerUp_toUse;

            powerUp_toUse = null;

            activePowerUp = _powUpToUse;
            return _powUpToUse;
        }
        else
        {
            return null;
        }
    }



    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }

    public void SetIsCollectableTaken(bool value)
    {
        isCollectableTaken = value;
    }
}
