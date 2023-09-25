using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour, IPlayer
{
    #region Classi

    [System.Serializable]
    class ObjToDesaturate_Class
    {
        public SpriteRenderer objToDesaturate;
        public Sprite desaturatedSprite;
        Sprite normalSprite;

        public void GetNormalSprite()
        {
            normalSprite = objToDesaturate.sprite;
        }

        public void Saturate()
        {
            objToDesaturate.sprite = normalSprite;
        }

        public void DeSaturate()
        {
            objToDesaturate.sprite = desaturatedSprite;
        }
    }

    #endregion

    DeathManager deathMng;
    PlayerMovRB playerMovScr;

    [SerializeField] PlayerStatsSO_Script stats_SO;
    public bool isDamageable = true;
    bool isDead;

    [Space(20)]
    [Range(-100, 0)]
    [SerializeField] float yMinDeath = -10;

    [Header("�� Feedback ��")]
    [SerializeField] AudioSource deathSfx;
    [SerializeField] Canvas deathCanvas;
    [SerializeField] SpriteRenderer normalSpr;
    [SerializeField] SpriteRenderer deathSpr;

    [Space(20)]
    [SerializeField] List<ObjToDesaturate_Class> objToDesaturate;

    [Space(10)]
    [SerializeField] AudioSource powUpPickUpSfx;
    [SerializeField] AudioSource powUpTimer_usedSfx;
    [SerializeField] AudioSource powUpTimer_endedSfx;
    [SerializeField] AudioSource powUpInvincibile_usedSfx;
    [SerializeField] AudioSource powUpInvincibile_endedSfx;
    [Space(5)]
    [SerializeField] AudioSource collectablePickUpSfx;

    [Header("�� DEBUG ��")]
    [SerializeField] float deathZoneSize = 15;


    #region Costanti

    const PowerUp.PowerUpType_Enum POW_EMPTY = PowerUp.PowerUpType_Enum._empty;
    const PowerUp.PowerUpType_Enum POW_TIMER = PowerUp.PowerUpType_Enum.Timer;
    const PowerUp.PowerUpType_Enum POW_INVINCIBLE = PowerUp.PowerUpType_Enum.Invincible;

    #endregion




    private void Awake()
    {
        deathMng = FindObjectOfType<DeathManager>();
        playerMovScr = FindObjectOfType<PlayerMovRB>();

        //Ritorna ogni oggetto saturato
        foreach (var obj in objToDesaturate)
        {
            obj.GetNormalSprite();
        }

        //Toglie i power-up dal giocatore
        stats_SO.ResetPowerUps();
        stats_SO.ResetCollectableTaken();

        //Reset degli sprite
        SwapToDeathSprite(false);


        //Fissa il frame-rate da raggiungere dal gioco a 60 fps
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        //Quando devo utilizzare il powerup
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            //Attiva il power-up messo da parte,
            //sovrascrivendolo e passandolo in questo script
            PowerUp.PowerUpType_Enum used_PU = stats_SO.UsePowerUp();
            
            if(used_PU != POW_EMPTY)
            {
                float dur = stats_SO.GetPowerUpDuration();    //Prende la durata dell'effetto
                stats_SO.ResetPowerUpDuration();              //La toglie dallo Scrip.Obj.

                StopAllCoroutines();

                //Reset tutti gli altri power-up
                EndSlowTimerPowUp();
                EndInvincibilePowerUp();

                //Attiva il corrispettivo effetto,
                //passando anche la durata del power-up
                switch (used_PU)
                {
                    case POW_TIMER:
                        float _tSpeed = stats_SO.GetTimeSpeed_TimerPowUp();

                        StartCoroutine(ActivateSlowTimerPowUp(dur, _tSpeed));
                        break;

                    case POW_INVINCIBLE:
                        StartCoroutine(ActivateInvincibilePowUp(dur));
                        break;
                }
            }
        }


        //Muore quando supera il limite minimo sulla Y
        if (transform.position.y <= yMinDeath)
        {
            Die();
        }
    }

    public void Die()
    {
        if (isDamageable)   //Se si puo' uccidere
        {
            isDead = true;
            SwapToDeathSprite(true);    //Toglie lo sprite del giocatore
                                        //e mostra quello di morte

            deathMng.ActivateScripts(false);    //Disattiva tutti gli script nella lista

            #region Feedback

            //Mostra la canvas di Game Over
            deathCanvas.gameObject.SetActive(true);

            //Audio
            deathSfx.Play();

            #endregion
        }
    }

    public void SwapToDeathSprite(bool isDead)
    {
        normalSpr.gameObject.SetActive(!isDead);
        deathSpr.gameObject.SetActive(isDead);
    }


    #region Power-Up - Slow Time

    IEnumerator ActivateSlowTimerPowUp(float powUpTime, float timeSpeed)
    {
        print("inizio SlowTime");


        //Inizio effetti
        Time.timeScale = timeSpeed;
        playerMovScr.SetPlayerSpeedMultip(2);    // funzione per raddoppiare velocita

        #region Feedback - inizio effetti

        DeSaturateAllSprites();

        powUpTimer_usedSfx.Play();    //Audio

        #endregion


        yield return new WaitForSeconds(powUpTime);


        //Fine effetti
        EndSlowTimerPowUp();

        #region Feedback - fine effetti

        SaturateAllSprites();

        powUpTimer_endedSfx.Play();    //Audio

        #endregion


        stats_SO.ResetActivePowerUp();    //Toglie il ppower-up da quello attivo

        print("fine SlowTime");
    }


    public void SaturateAllSprites()
    {
        //Per ogni sprite (prop, nemici, ecc...)
        //li satura
        foreach (var obj in objToDesaturate)
        {
            obj.Saturate();
        }
    }
    public void DeSaturateAllSprites()
    {
        //Per ogni sprite (prop, nemici, ecc...)
        //li de-satura
        foreach (var obj in objToDesaturate)
        {
            obj.DeSaturate();
        }
    }


    void EndSlowTimerPowUp()
    {
        Time.timeScale = 1;
        playerMovScr.ResetPlayerSpeedMultip();    // funzione per ripristinare la velocita
    }

    #endregion


    #region Power-Up - Invincibile

    IEnumerator ActivateInvincibilePowUp(float powUpTime)
    {
        print("inizio Invinc");


        //Inizio effetti
        isDamageable = false;

        #region Feedback - inizio effetti

        powUpInvincibile_usedSfx.Play();    //Audio

        #endregion


        yield return new WaitForSeconds(powUpTime);
        

        //Fine effetti
        EndInvincibilePowerUp();

        #region Feedback - fine effetti

        powUpInvincibile_endedSfx.Play();    //Audio

        #endregion


        stats_SO.ResetActivePowerUp();
        
        print("fine Invinc");
    }

    void EndInvincibilePowerUp()
    {
        isDamageable = true;
    }

    #endregion


    public bool GetIsDead() => isDead;

    public AudioSource GetPowUpPickUpSfx() => powUpPickUpSfx;
    public AudioSource GetCollectablePickUpSfx() => collectablePickUpSfx;



    #region EXTRA - Gizmos

    private void OnDrawGizmosSelected()
    {
        Vector3 deathOffset = Vector3.up * yMinDeath,
                cubeOffset = Vector3.up * (deathZoneSize / 2),
                pos_yDeath = Vector3.zero + deathOffset - cubeOffset;
        pos_yDeath.x = transform.position.x;

        //Disegna un rettangolo dove il giocatore muore
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(pos_yDeath, new Vector3(deathZoneSize * 2,
                                                    deathZoneSize ,
                                                    deathZoneSize * 2));
    }

    #endregion
}
