using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public int life;

    private void Start()
    {
        Destroy(gameObject, life);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IEnemy playerCheck = collision.GetComponent<IEnemy>();

        if (playerCheck == null)
        {
            return;
        }

        // elimina il giocatore
        playerCheck.Die();
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
