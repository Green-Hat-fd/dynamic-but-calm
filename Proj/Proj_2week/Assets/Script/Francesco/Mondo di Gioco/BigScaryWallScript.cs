using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigScaryWallScript : MonoBehaviour
{
    [SerializeField] float wallVel = 2;
    [SerializeField] Vector3 movingDirection = Vector3.right;


    
    void Update()
    {
        //Lo script che fa muovere 
        transform.position = Vector3.MoveTowards(transform.position,
                                                 transform.position + movingDirection,
                                                 Time.deltaTime * wallVel);
    }


    #region EXTRA - Cambiare l'inspector

    private void OnValidate()
    {
        //Normalizza il vettore di direzione
        //(li rende sempre tra 0 e 1)
        movingDirection.x = Mathf.Clamp(movingDirection.x, -1, 1);
        movingDirection.y = Mathf.Clamp(movingDirection.y, -1, 1);
        movingDirection.z = Mathf.Clamp(movingDirection.z, -1, 1);
    }

    #endregion


    #region EXTRA - Gizmos

    private void OnDrawGizmosSelected()
    {
        Quaternion _dir = Quaternion.Euler(movingDirection * 180f),
                   _rot = transform.rotation * _dir;
        Vector3 _pos = transform.position + transform.up * 2;

        //Disegna la direzione (in blu)
        //e gli altri due assi
        Gizmos.color = Color.red;
        Gizmos.DrawRay(_pos, _rot * Vector3.right * 0.5f);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(_pos, _rot * Vector3.up * 0.5f);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(_pos, _rot * Vector3.forward);
        Gizmos.color = Color.gray;
        Gizmos.DrawRay(_pos, movingDirection);
    }

    #endregion
}
