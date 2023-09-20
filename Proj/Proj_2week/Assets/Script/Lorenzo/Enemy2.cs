using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public Transform player; 
    public GameObject bulletPrefab; 
    public float fireRate = 2f; 
    public float bulletSpeed = 10f; 
    public Transform firePoint;

    private float nextFireTime; 

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            // Calcola la direzione verso il giocatore
            Vector3 direction = (player.position - transform.position).normalized;

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
}
