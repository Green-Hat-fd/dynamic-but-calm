using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType_Enum
    {
        [InspectorName("-- Empty --")]
        _empty,
        Timer,
        Invincible
    }

    [SerializeField] PlayerStatsSO_Script stats_SO;

    [Space(20)]
    [SerializeField] PowerUpType_Enum powUpType = 0;

    [Space(10)]
    [SerializeField] float powUpDuration_sec = 5;

    [Header("—— Feedback ——")]
    [SerializeField] float sinVel = 1.5f;
    [SerializeField] float sinHeight = 0.25f;
    private Vector3 startPos; // posizione di partenza del power-up

    [Space(10)]
    [SerializeField] AudioSource deniedSfx;

    [Header("—— DEBUG ——")]
    [SerializeField] bool showGizmos;
    



    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        // movimento verticale in base al tempo
        float yPos = Mathf.Sin(Time.time * sinVel) * sinHeight;

        Vector3 newPosition = startPos + new Vector3(0, yPos, 0);
        transform.position = newPosition;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        IPlayer playerCheck = collision.GetComponent<IPlayer>();

        if(playerCheck == null)
        {
            return;
        }

        // raccogli l'oggetto

        if (isPowUpTaken)
        {
            AudioSource pickUpSfx = collision
                                    .GetComponent<PlayerStatsManager>()
                                    .GetPowUpPickUpSfx();

            pickUpSfx.Play();    //Audio

            gameObject.SetActive(false);
        }
        else
        {
            //Riproduce l'audio quando
            //non raccoglie il power-up
            deniedSfx.Play();
        }
    }



    #region EXTRA - Gizmos

    Vector3 sinBottom, sinTop;

    private void OnDrawGizmosSelected()
    {
        if (showGizmos)
        {
            if (!Application.isPlaying)
            {
                sinBottom = transform.position + (transform.up * sinHeight);
                sinTop = transform.position + (-transform.up * sinHeight);
            }

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(sinBottom, sinTop);
        }
    }

    #endregion
}
