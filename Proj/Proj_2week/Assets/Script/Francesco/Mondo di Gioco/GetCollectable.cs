using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCollectable : MonoBehaviour
{
    [SerializeField] PlayerStatsSO_Script stats_SO;

    [Header("—— Feedback ——")]
    [SerializeField] float sinVel = 2.5f;
    [SerializeField] float sinHeight = 0.25f;
    private Vector3 startPos; // posizione di partenza del power-up



    void Start()
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

        if (playerCheck == null)
        {
            return;
        }

        //Raccogli la parte del collezionabile
        stats_SO.AddCollectableTaken();

        #region Feedback

        //Audio
        AudioSource collPickUpSfx = collision
                                       .GetComponent<PlayerStatsManager>()
                                       .GetCollectablePickUpSfx();

        collPickUpSfx.Play();

        #endregion

        gameObject.SetActive(false);
    }



    #region EXTRA - Gizmos

    Vector3 sinBottom, sinTop;

    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying)
        {
            sinBottom = transform.position + (transform.up * sinHeight);
            sinTop = transform.position + (-transform.up * sinHeight);
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(sinBottom, sinTop);
    }

    #endregion
}
