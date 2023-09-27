using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public float normalScaleImage = 1.0f;

    public void IngrandisciImmagine()
    {
        Debug.Log("Ingrandito");
        // Ingrossa l'immagine del bottone
        transform.localScale = new Vector3(normalScaleImage * 1.2f, normalScaleImage * 1.2f, 1.0f);
    }
}
