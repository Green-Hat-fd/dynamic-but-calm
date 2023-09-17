using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxManager : MonoBehaviour
{
    #region Classi

    [System.Serializable]
    public class Parallax_Class
    {
        public Transform obj;
        public bool canReplicate_TODO_RENAME = true;
        [Range(1, 100)]
        public int manymany_TODO_RENAME;

        Vector3 _startPos;
        public float _speedMult;


        public void SetPositionAtStart()
        {
            _startPos = obj.position;
        }


        public float GetXPosition() => _startPos.x;
        public float GetSpeedMultiplier() => _speedMult;


        public void SetSpeedMultiplier(float value)
        {
            _speedMult = value;
        }

        public void UpdatePosition(Vector3 posToSubtract)
        {
            obj.position = _startPos - posToSubtract;
        }
    }

    #endregion


    [SerializeField] Transform playerCamera;
    [SerializeField] Vector2 parallaxDist = new Vector2(10, 60);
    
    [Space(20)]
    [SerializeField] List<Parallax_Class> parallaxObjs;

    [Header("�� DEBUG ��")]
    [SerializeField] bool showGizmo_NotSelected = false;


    
    
    private void Awake()
    {
        foreach (Parallax_Class parObj in parallaxObjs)
        {
            //Aggiorna la distanza della posizione
            //per ogni oggetto nella lista
            parObj.SetPositionAtStart();

            //Calcola quanto veloce si deve
            //muovere ogni oggetto tramite
            //la sua distanza dalla telecamera
            float _boxDim = parallaxDist.y - parallaxDist.x;

            Vector3 start = playerCamera.position + playerCamera.forward * parallaxDist.x,
                    end = parObj.obj.position;
            float distFromCamera = Vector3.Distance(start, end);

            float finalVal = distFromCamera / _boxDim; 
            finalVal = Mathf.Clamp01(finalVal);    //Limita il valore tra 0 e 1

            parObj.SetSpeedMultiplier(finalVal);


            //Sets the object to the 
            if (finalVal >= 1)
                parObj.obj.parent = playerCamera.transform;
        }
    }
    
    void FixedUpdate()
    {
        Vector3 camRight = new Vector3(playerCamera.position.x, 0);

        //Fa muovere ogni oggetto nella lista
        //piu' lentamente se si trova piu' lontano
        foreach (Parallax_Class parObj in parallaxObjs)
        {
            if(parObj.GetSpeedMultiplier() < 1)
             parObj.UpdatePosition(camRight / parObj.GetSpeedMultiplier());
        }
    }


    #region EXTRA - Cambiare l'inspector

    private void OnValidate()
    {
        //Rende sempre la X come il minimo
        //e la Y come il massimo
        parallaxDist.x = Mathf.Clamp(parallaxDist.x, 0, parallaxDist.y);

        //Rimuove il numero di quanti
        //ne deve creare dell'Inspector
        foreach (Parallax_Class parObj in parallaxObjs)
        {
            if (!parObj.canReplicate_TODO_RENAME)
            {
                parObj.manymany_TODO_RENAME = 0;
            }
        }
    }

    #endregion


    #region EXTRA - Gizmos

    private void OnDrawGizmos()
    {
        if (showGizmo_NotSelected)
            DrawAllGizmos();
    }
    private void OnDrawGizmosSelected()
    {
        if (!showGizmo_NotSelected)
            DrawAllGizmos();
    }

    void DrawAllGizmos()
    {
        float _halfDist = (parallaxDist.y - parallaxDist.x) / 2;

        Vector3 _pos = playerCamera.position
                        + playerCamera.forward * parallaxDist.x
                        + playerCamera.forward * _halfDist,
                _dim = new Vector3(Screen.currentResolution.width * 0.005f,
                                   Screen.currentResolution.height * 0.005f,
                                   _halfDist * 2);

        //Disegna il cubo del
        //parallasse (in blu)
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(_pos, _dim);
    }

    #endregion
}
