using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovRB : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] float playerVel = 7.5f;
    [SerializeField] float jumpPower = 8.5f;
    float xMov;
    Vector3 moveAxis;
    float _speedMultip = 1;

    [Space(20)]
    [Min(0)]
    [SerializeField] float limitGroundCheck = 0.25f;
    float halfPlayerHeight;
    float radiusSpherecast = 0.25f;

    bool isOnGround = false;
    bool hasJumped = false;

    RaycastHit2D hitBase;
    RaycastHit2D slopeHit;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;

        halfPlayerHeight = GetComponent<CapsuleCollider2D>().size.y / 2;
    }

    private void Update()
    {
        //Prende gli assi dall'input di movimento
        //xMov = ;GameManager.inst.inputManager.Giocatore.Movimento.ReadValue<Vector2>().x;
        if (Input.GetKey(KeyCode.A))
            xMov = -1;
        else if (Input.GetKey(KeyCode.D))
            xMov = 1;
        else
            xMov = 0;

        moveAxis = transform.right * xMov;      //Vettore movimento orizzontale


        //Prende l'input di salto
        //hasJumped = GameManager.inst.inputManager.Giocatore.Salto.ReadValue<float>() > 0;
        hasJumped = Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.Space);
    }

    void FixedUpdate()
    {
        //TODO: sistema il salto / ground check

        //Calcolo se si trova a terra
        //("~0" significa che collide con tutti i layer)
        hitBase = Physics2D.CircleCast(transform.position + (-transform.up * (halfPlayerHeight + limitGroundCheck)),
                                       radiusSpherecast,
                                       -transform.up,
                                       /*halfPlayerHeight + limitGroundCheck*/ + radiusSpherecast,
                                       ~0);
        
        isOnGround = hitBase.transform != null;


        float airVelMultip = !isOnGround ? 0.65f : 1;   //Diminuisce la velocita' orizz. se si trova in aria


        //Salta se premi Spazio e si trova a terra
        if (hasJumped && isOnGround)
        {
            //Resetta la velocita' Y e applica la forza d'impulso verso l'alto
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(transform.up * jumpPower, ForceMode2D.Impulse);
        }


        //Movimento orizzontale (semplice) del giocatore
        rb.AddForce(moveAxis.normalized * playerVel * _speedMultip * 10f, ForceMode2D.Force);


        //Applica l'attrito dell'aria al giocatore
        //(Riduce la velocita' se il giocatore e' in aria e si sta muovendo)
        if (!isOnGround
            &&
            (rb.velocity.x >= 0.05f || rb.velocity.y >= 0.05f))
        {
            rb.AddForce(new Vector3(-rb.velocity.x * 0.1f, 0, -rb.velocity.y * 0.1f), ForceMode2D.Force);
        }


        #region Limitazione della velocita'

        //Prende la velocita' orizzontale del giocatore
        Vector2 horizVel = new Vector2(rb.velocity.x, 0);

        //Controllo se si accelera troppo, cioe' si supera la velocita'
        if (horizVel.magnitude >= playerVel)
        {
            //Limita la velocita' a quella prestabilita, riportandola al RigidBody
            Vector2 limitazione = horizVel.normalized * playerVel * airVelMultip;
            rb.velocity = new Vector2(limitazione.x, rb.velocity.y);
        }

        #endregion


        #region Controllo in pendenza

        //

        #endregion
    }

    bool InSlope()
    {
        if (Physics2D.Raycast(transform.position, -transform.up, halfPlayerHeight + limitGroundCheck).transform)
        {
            //Prende l'angolo della pendenza
            //(tra il vett. "sotto" e la normale del terreno)
            float angle = Vector3.Angle(transform.up, slopeHit.normal);

            return angle != 0;
        }

        return false;   //Nel caso non colpisce nulla
    }


    public void SetPlayerSpeedMultip(float newMultip)
    {
        _speedMultip = newMultip;
    }

    public void ResetPlayerSpeedMultip()
    {
        _speedMultip = 1;
    }


    #region EXTRA - Gizmos

    private void OnDrawGizmos()
    {
        //Disegna lo SphereCast per capire se e' a terra o meno (togliendo l'altezza del giocatore)
        Gizmos.color = new Color(0.85f, 0.85f, 0.85f, 1);
        Gizmos.DrawWireSphere(transform.position + (-transform.up * halfPlayerHeight)
                               + (-transform.up * limitGroundCheck)
                               - (-transform.up * radiusSpherecast),
                              radiusSpherecast);

        //Disegna dove ha colpito se e' a terra e se ha colpito un'oggetto solido (no trigger)
        Gizmos.color = Color.green;
        if (isOnGround && hitBase.collider)
        {
            Gizmos.DrawLine(hitBase.point + ((Vector2)transform.up * hitBase.distance), hitBase.point);
            Gizmos.DrawCube(hitBase.point, Vector3.one * 0.1f);
        }
    }

    #endregion
}
