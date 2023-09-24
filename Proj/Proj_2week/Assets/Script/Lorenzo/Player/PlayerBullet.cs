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
        var enBulletCheck = collision.GetComponent<EnemyBullet>();
        var plBulletCheck = collision.GetComponent<PlayerBullet>();
        bool isABullet = enBulletCheck != null || plBulletCheck != null;

        IEnemy enemyCheck = collision.GetComponent<IEnemy>();
        
        if (enemyCheck == null || isABullet)
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
