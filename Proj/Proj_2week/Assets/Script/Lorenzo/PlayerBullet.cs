using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float life;

    private void Start()
    {
        Destroy(gameObject, life);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IEnemy enemyCheck = collision.GetComponent<IEnemy>();

        if (enemyCheck == null)
        {
            return;
        }

        // elimina il giocatore
        enemyCheck.Die();
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
