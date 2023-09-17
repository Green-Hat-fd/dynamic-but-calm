using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour, IDamageable
{
    [SerializeField] PlayerStatsSO_Script stats_SO;
    
    bool isDead;

    [Header("—— Feedback ——")]
    [SerializeField] AudioSource deathSfx;



    void Update()
    {
        //Quando devo utilizzare il powerup
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            PowerUp used_PU = stats_SO.UsePowerUp();

            //TODO: sistema
            //
            //used_PU.Activate();
        }


        //Quando muore
        if (isDead)
        {
            ;
        }
    }


    public void Die()
    {
        isDead = true;


        #region Feedback

        deathSfx.Play();

        #endregion
    }
}
