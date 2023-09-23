using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour, IPlayer
{
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

    DeathManager deathMng;

    [SerializeField] PlayerStatsSO_Script stats_SO;
    public bool isDamageable = true;
    bool isDead;

    [Space(20)]
    [Range(-100, 0)]
    [SerializeField] float yMinDeath = -10;

    [Header("—— Feedback ——")]
    [SerializeField] AudioSource deathSfx;
    [SerializeField] Canvas deathCanvas;

    [Space(20)]
    [SerializeField] List<ObjToDesaturate_Class> objToDesaturate;

    [Header("—— DEBUG ——")]
    [SerializeField] float deathZoneSize = 15;



    private void Awake()
    {
        deathMng = FindObjectOfType<DeathManager>();


        foreach (var obj in objToDesaturate)
        {
            obj.GetNormalSprite();
        }
    }

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


        //Muore quando supera il limite minimo sulla Y
        if (transform.position.y <= yMinDeath)
        {
            Die();
        }
    }

    public void Die()
    {
        isDead = true;
        Destroy(gameObject);    //Toglie il giocatore

        deathMng.ActivateScripts(false);    //Disattiva tutti gli script nella lista

        #region Feedback

        //Mostra la canvas di Game Over
        deathCanvas.gameObject.SetActive(true);

        //Audio
        deathSfx.Play();

        #endregion
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


    public bool GetIsDead() => isDead;



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
