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
        public bool canSpawnMultiple = true;

        float _spriteLength;
        Vector3 _startPos;
        float _speedMult;


        public void SetPositionAtStart()
        {
            _startPos = obj.position;
            _spriteLength = obj.GetComponent<SpriteRenderer>().bounds.size.x;
        }


        public float GetXPosition() => _startPos.x;
        public float GetSpriteLength() => _spriteLength;
        public float GetSpeedMultiplier() => _speedMult;

        public float GetRightSpriteCenter() => GetXPosition() + _spriteLength;
        public float GetLeftSpriteCenter() => GetXPosition() - _spriteLength;


        public void SetSpeedMultiplier(float value)
        {
            _speedMult = value;
        }

        public void UpdatePosition(Vector3 posToAdd)
        {
            obj.position = _startPos + posToAdd;
        }

        public void MoveToRightSide()
        {
            _startPos.x += _spriteLength;
        }
        public void MoveToLeftSide()
        {
            _startPos.x -= _spriteLength;
        }
    }

    #endregion


    [SerializeField] Camera playerCam;
    [SerializeField] Vector2 parallaxDist = new Vector2(5, 60);
    Transform playerCam_tr;
    
    [Space(20)]
    [SerializeField] List<Parallax_Class> parallaxObjs;

    [Header("—— DEBUG ——")]
    [SerializeField] bool showGizmo_NotSelected = false;


    
    
    private void Awake()
    {
        playerCam_tr = playerCam.transform;


        foreach (Parallax_Class parObj in parallaxObjs)
        {
            //Aggiorna la distanza della posizione
            //per ogni oggetto nella lista
            parObj.SetPositionAtStart();

            //Calcola quanto veloce si deve
            //muovere ogni oggetto tramite
            //la sua distanza dalla telecamera
            float _boxDim = parallaxDist.y - parallaxDist.x;

            Vector3 start = playerCam_tr.position + playerCam_tr.forward * parallaxDist.x,
                    end = parObj.obj.position;
            float distFromCamera = Vector3.Distance(start, end);

            float finalMoltVal = Mathf.Clamp01(distFromCamera / _boxDim);    //Limitando il moltiplicatore tra 0 e 1

            parObj.SetSpeedMultiplier(finalMoltVal);


            //Mette gli oggetti che sono fissi
            //come figli della telecamera del giocat.
            if (finalMoltVal >= 1)
                parObj.obj.parent = playerCam_tr;



            #region Creazione degli sprite a destra e sinistra

            if (parObj.canSpawnMultiple)    //Se ne può creare più di 1...
            {
                //Crea il figlio destro e sinistro,
                //e li mette figli dell'oggetto del parallasse
                Transform childRight = Instantiate(parObj.obj),
                          childLeft = Instantiate(parObj.obj);

                childRight.parent = parObj.obj;
                childLeft.parent = parObj.obj;

                childRight.name += " (Dx)";
                childLeft.name += " (Sx)";


                //Calcola la distanza dello sprite e li
                //mette a destra e sinistra dell'originale
                Vector3 spriteLgt_right = parObj.obj.right * parObj.GetSpriteLength();

                childRight.position += spriteLgt_right;
                childLeft.position -= spriteLgt_right;
            }

            #endregion
        }
    }
    
    void FixedUpdate()
    {
        Vector3 camRight = new Vector3(playerCam_tr.position.x, 0);

        //Fa muovere ogni oggetto nella lista
        //piu' lentamente se si trova piu' lontano
        foreach (Parallax_Class parObj in parallaxObjs)
        {
            if(parObj.GetSpeedMultiplier() < 1)
                parObj.UpdatePosition(camRight * parObj.GetSpeedMultiplier());


            //Prende la posizione della telecamera rispetto allo sprite,
            //calcola se si trova al centro dello sprite destro o sinistro...
            float camPos_toSprite = camRight.x * (1 - parObj.GetSpeedMultiplier());
            bool isOnRightSprite = camPos_toSprite > parObj.GetRightSpriteCenter(),
                 isOnLeftSprite  = camPos_toSprite < parObj.GetLeftSpriteCenter();

            //...E lo sistema nel lato corrispondente
            if (isOnRightSprite)
                parObj.MoveToRightSide();
            else 
                if (isOnLeftSprite)
                    parObj.MoveToLeftSide();
        }
    }


    #region EXTRA - Cambiare l'inspector

    private void OnValidate()
    {
        //Rende sempre la X come il minimo
        //e la Y come il massimo
        parallaxDist.x = Mathf.Clamp(parallaxDist.x, 0, parallaxDist.y);
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

        Transform _playerCam_tr = playerCam.transform;
        Vector3 _pos = _playerCam_tr.position
                        + _playerCam_tr.forward * parallaxDist.x
                        + _playerCam_tr.forward * _halfDist,
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
