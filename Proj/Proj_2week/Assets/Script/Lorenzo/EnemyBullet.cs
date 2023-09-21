using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private float life = 2f;

    private void Start()
    {
        Destroy(gameObject, life);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IPlayer playerCheck = collision.GetComponent<IPlayer>();

        if (playerCheck == null)
        {
            return;
        }

        // elimina il giocatore
        playerCheck.Die();
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collider2D collision)
    {
        IEnemy playerCheck = collision.GetComponent<IEnemy>();

        if (playerCheck != null)
        {
            return;
        }

        Destroy(gameObject);
    }
}
