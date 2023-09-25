using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollowScript : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float zOffset = -10;

    [Space(20)]
    [SerializeField] Transform bigScaryWall;
    [Min(0)]
    [SerializeField] float xDistMin = 8.25f;
    [Min(-100)]
    [SerializeField] float yWorldMin = -25;

    [Header("—— DEBUG ——")]
    #region Tooltip()
    [Tooltip("YZ_dim.x = Y \nYZ_dim.y = Z")]
    #endregion
    [SerializeField] Vector2 YZ_dim = new Vector2(17.5f, 10);

    [Space(10)]
    [SerializeField] bool showCameraLimits = true;



    void Update()
    {
        //Controlla se la telecamera ha superato il limite
        float xMin = bigScaryWall.position.x + xDistMin;
        bool isOver = player.position.x <= xMin;
        bool isTooLow = player.position.y <= yWorldMin;


        //Segue il giocatore
        transform.position = player.position + Vector3.forward * zOffset;
        

        //Limita il movimento sull'asse X
        //della telecamera a quello del muro
        Vector3 newPos = transform.position;

        newPos.x = isOver ? xMin : player.position.x;        //Limita il movim. X se no segue il giocatore
        newPos.y = isTooLow ? yWorldMin : player.position.y;   //Limita il movim. Y fino al minimo

        transform.position = newPos;
    }


    #region EXTRA - Gizmos

    private void OnDrawGizmosSelected()
    {
        Vector3 rightOffset = bigScaryWall.right * (xDistMin / 2);
        Vector3 pos_wall = bigScaryWall.position + rightOffset;
        pos_wall.z = transform.position.z + (YZ_dim.y / 2);

        Vector3 downOffset = Vector3.up * yWorldMin;
        Vector3 pos_y = Vector3.zero + downOffset;
        pos_y.x = transform.position.x;


        if (showCameraLimits)
        {
            //Disegna un rettangolo che va dal muro fino al limite della X
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(pos_wall, new Vector3(xDistMin, YZ_dim.x/2, YZ_dim.y));

            //Disegna un rettangolo/linea che si trova al limite della Y
            Gizmos.color = Color.black;
            Gizmos.DrawWireCube(pos_y, new Vector3(50, 0, 50));
        }
    }

    #endregion
}
