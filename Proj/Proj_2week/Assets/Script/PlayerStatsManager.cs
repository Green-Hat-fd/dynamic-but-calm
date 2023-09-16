using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    [SerializeField] PlayerStatsSO_Script stats_SO;
    
    bool isDead;



    void Update()
    {
        //Quando devo utilizzare il powerup
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            //TODO: usa il powerup
        }
    }


    public void DamagePlayer()
    {
        isDead = true;
    }
}
