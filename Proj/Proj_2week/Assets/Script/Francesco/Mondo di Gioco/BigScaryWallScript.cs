using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigScaryWallScript : MonoBehaviour
{
    [SerializeField] float wallVel = 2;
    [SerializeField] Vector2 movingDirection = Vector2.right;
        
    [Space(20)]
    [SerializeField] SpriteRenderer monsterSpr;
    [SerializeField] float yLimit = 5f;
    GameObject playerObj;
    Transform monsterTransf;



    private void Awake()
    {
        playerObj = FindAnyObjectByType<PlayerMovRB>().gameObject;
        monsterTransf = monsterSpr.transform;
    }

    void Update()
    {
        //Sposta lo Sprite in base al giocatore
        float playerY = playerObj.transform.position.y;
        Vector3 positionToGo_up = monsterTransf.position;
        
        positionToGo_up.y = Mathf.MoveTowards(monsterTransf.position.y,
                                              playerY + yLimit,
                                              Time.deltaTime * wallVel);
        
        monsterTransf.position = positionToGo_up;


        //Il codice di movimento
        transform.position = Vector3.MoveTowards(transform.position,
                                                 transform.position + (Vector3)movingDirection,
                                                 Time.deltaTime * wallVel);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Controllo se cio' che e'
        //entrato puo' prendere danno
        IEnemy enDamageCheck = collision.GetComponent<IEnemy>();
        IPlayer plDamageCheck = collision.GetComponent<IPlayer>();

        //Ogni cosa che viene a
        //contatto con il muro, muore
        if (enDamageCheck != null) { enDamageCheck.Die(); }
        if (plDamageCheck != null) { plDamageCheck.DieFromWall(); }
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


        //Disegna il limite max dove non andrà oltre (in grigio)
        //e la linea tra il mostro e il giocatore(in nero)
        Transform _mnstrTr = monsterSpr.transform;
        Vector3 _cubePos_YLim = _mnstrTr.position + _mnstrTr.up * (-yLimit/2);
        Vector3 _cubeDim_YLim = new Vector3(1, yLimit, 1);

        Gizmos.DrawWireCube(_cubePos_YLim, _cubeDim_YLim);

        Vector3 _plPos_down = new Vector3(_mnstrTr.position.x,
                                          playerObj.transform.position.y,
                                          _mnstrTr.position.z);

        Gizmos.color = Color.black;
        Gizmos.DrawLine(_mnstrTr.position, _plPos_down);
    }

    #endregion
}
