using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float zOffset = -10;

    [Space(20)]
    [SerializeField] Transform bigScaryWall;
    [Min(0)]
    [SerializeField] float xDistMin;

    [Header("—— DEBUG ——")]
    #region Tooltip()
    [Tooltip("YZ_dim.x = Y \nYZ_dim.y = Z")]
    #endregion
    [SerializeField] Vector2 YZ_dim = new Vector2(17.5f, 10);



    void Update()
    {
        //Controlla se la telecamera ha superato il limite
        float xMin = bigScaryWall.position.x + xDistMin;
        bool isOver = player.position.x <= xMin;


        //Segue il giocatore
        transform.position = player.position + Vector3.forward * zOffset;
        

        //Limita il movimento sull'asse X
        //della telecamera a quello del muro
        Vector3 newPos = transform.position;
        newPos.x = isOver ? xMin : player.position.x;   //Limita il movim. se no segue il giocatore
        transform.position = newPos;
    }


    #region EXTRA - Gizmos

    private void OnDrawGizmosSelected()
    {
        Vector3 rightOffset = bigScaryWall.right * (xDistMin / 2);
        Vector3 pos = bigScaryWall.position + rightOffset;
        pos.z = transform.position.z + (YZ_dim.y / 2);

        //Disegna un rettangolo che va dal muro fino al limite della X
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pos, new Vector3(xDistMin, YZ_dim.x/2, YZ_dim.y));
    }

    #endregion
}
