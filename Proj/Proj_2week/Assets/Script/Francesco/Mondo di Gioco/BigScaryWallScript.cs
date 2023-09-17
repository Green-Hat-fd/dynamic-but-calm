using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigScaryWallScript : MonoBehaviour
{
    [SerializeField] float wallVel = 2;
    [SerializeField] Vector2 movingDirection = Vector2.right;


    
    void Update()
    {
        //Lo script che fa muovere 
        transform.position = Vector3.MoveTowards(transform.position,
                                                 transform.position + (Vector3)movingDirection,
                                                 Time.deltaTime * wallVel);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Controllo se cio' che e'
        //entrato puo' prendere danno
        IDamageable damageCheck = collision.GetComponent<IDamageable>();

        if (damageCheck != null)
        {
            //Ogni cosa che viene a
            //contatto con il muro, muore
            damageCheck.Die();
        }
    }


    #region EXTRA - Cambiare l'inspector

    private void OnValidate()
    {
        //Normalizza il vettore di direzione
        //(li rende sempre tra 0 e 1)
        movingDirection.x = Mathf.Clamp(movingDirection.x, -1, 1);
        movingDirection.y = Mathf.Clamp(movingDirection.y, -1, 1);
    }

    #endregion


    #region EXTRA - Gizmos

    private void OnDrawGizmosSelected()
    {
        Vector3 _pos = transform.position + transform.up * 2;

        //Disegna la direzione (in grigio)
        Gizmos.color = Color.gray;
        Gizmos.DrawRay(_pos, movingDirection);
    }

    #endregion
}
