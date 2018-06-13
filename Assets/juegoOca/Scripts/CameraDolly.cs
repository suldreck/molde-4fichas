using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDolly : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        theStateManager = GameObject.FindObjectOfType<StateManager>();
        stone = GameObject.FindObjectsOfType<PlayerStone>();
        distancia = new float[stone.Length];
        baseCamara = this.transform.position;// posicion inicial de la camara
        Debug.Log("numero piedras" + stone.Length);
        for (int i = 0; i < stone.Length; i++)
        {
         Debug.Log("nombre"+stone[i].name+
             "indice  "+i +" posicion x "+stone[i].transform.position.x);
        }
        Id = 0;
        IdAnterior = -1;
        IdCorrecta = 0;
    }

    StateManager theStateManager;
    PlayerStone[] stone;
    Vector3 baseCamara;

    public float PivotAngle = 35f;
    float pivotVelocity;
    float[] distancia;
    Vector3 posicionActual;
    int IdAnterior;//estado q se mantendra hasta q cambie Id
    int Id;//estado q se esta actualiando constantemente , activara el if si id es diferente de IdActual q es el estado mantenido 
    int IdCorrecta;//como la id de stone no coincide  despues del while saldra con la posicion correspondiente

    // Update is called once per frame
    void Update()
    {

        //float theAngle = this.transform.rotation.eulerAngles.y;
        //if(theAngle > 180)
        //    theAngle -= 360f;

        //theAngle = Mathf.SmoothDamp( 
        //    theAngle, 
        //    (theStateManager.CurrentPlayerId==0 ? PivotAngle : -PivotAngle), 
        //    ref pivotVelocity, 
        //    0.25f );
        //posicionActual= baseCamara + new Vector3(stone[theStateManager.CurrentPlayerId].transform.position.x, 0, 0);
        Id = theStateManager.CurrentPlayerId;
        
        if (Id != IdAnterior)
        {
            IdAnterior = Id;
            int i = 0;
            while (stone[i].PlayerId!=IdAnterior && i< stone.Length)
            {
                i++;
            }
            IdCorrecta = i;
        }
        
      
        posicionActual = baseCamara + new Vector3(stone[IdCorrecta].transform.position.x, 0, 0);
       // Debug.Log("la posicion de la camara actual es " + posicionActual);
        this.transform.position = posicionActual;
        //this.transform.rotation = Quaternion.Euler( new Vector3(0, theAngle, 0) );
    }


}
