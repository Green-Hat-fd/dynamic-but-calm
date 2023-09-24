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
        IEnemy enemyCheck = collision.GetComponent<IEnemy>();


        if (enemyCheck != null || playerCheck == null)
        {
            Destroy(gameObject);
            return;
        }

        // elimina il giocatore
        playerCheck.Die();
        Destroy(gameObject);
    }
}
