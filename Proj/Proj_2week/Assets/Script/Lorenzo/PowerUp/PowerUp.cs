using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] PlayerStatsSO_Script stats_SO;
    private Vector3 startPos; // posizione di partenza del power-up
    
    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        // movimento verticale in base al tempo
        float yPos = Mathf.Sin(Time.time * 2) * 0.5f;

        Vector3 newPosition = startPos + new Vector3(0, yPos, 0);
        transform.position = newPosition;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        IPlayer playerCheck = collision.GetComponent<IPlayer>();

        if(playerCheck == null)
        {
            return;
        }

        // raccogli l'oggetto
        bool isPowUpTaken = stats_SO.AddPowerUp_toUse(gameObject.GetComponent<PowerUp>());

        if (isPowUpTaken)
        {
            gameObject.SetActive(false);
        }
    }
}
