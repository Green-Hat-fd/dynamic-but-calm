using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject bulletPrefab; 
    public Transform firePoint; 
    public float bulletSpeed = 10f;
    public float delayFire = 1f;

    private bool canShoot = true; // Aggiungiamo una variabile per controllare se è possibile sparare

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && canShoot)
        {
            Shoot();
            canShoot = false; // Disabilita temporaneamente la possibilità di sparare
            Invoke("EnableShooting", delayFire); // Richiama EnableShooting dopo 1 secondo
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.localRotation);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        rb.velocity = firePoint.transform.right * bulletSpeed;
    }

    void EnableShooting()
    {
        canShoot = true; // Abilita nuovamente la possibilità di sparare
    }
}