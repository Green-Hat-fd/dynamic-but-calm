using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    [SerializeField] List<MonoBehaviour> scriptsToDeactivate;


    private void Awake()
    {
        //Attiva ogni script nella lista
        ActivateScripts(true);
    }


    public void ActivateScripts(bool active)
    {
        foreach (MonoBehaviour scr in scriptsToDeactivate)
        {
            scr.enabled = active;
        }
    }
}
