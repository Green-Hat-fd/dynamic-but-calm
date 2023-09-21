using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour, IEnemy
{
    public Transform player; 
    public GameObject bulletPrefab; 
    private float fireRate = 1f; 
    private float bulletSpeed = 10f; 
    public Transform firePoint;

    private float nextFireTime; 

    void Update()
    {
        // Calcola la distanza tra il nemico e il giocatore
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Imposta una distanza massima a cui il nemico pu� sparare
        float maxShootDistance = 10f; 

        if (distanceToPlayer <= maxShootDistance && Time.time >= nextFireTime)
        {
            // Calcola la direzione verso il giocatore
            Vector3 direction = (player.position - transform.position).normalized;

            // Calcola l'angolo in radianti tra la direzione e il vettore destro (1,0)
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Forza l'angolo a essere 0 o 180 gradi
            angle = Mathf.Abs(angle) > 90f ? 180f : 0f;

            // Imposta la rotazione dell'oggetto in base all'angolo calcolato
            transform.rotation = Quaternion.Euler(0, angle, 0);

            Shoot();

            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        rb.velocity = transform.right * bulletSpeed;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        IPlayer playerCheck = collision.GetComponent<IPlayer>();

        if (playerCheck == null)
        {
            return;
        }

        // elimina il giocatore
        playerCheck.Die();
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
