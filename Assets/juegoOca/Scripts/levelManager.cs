using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class levelManager : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }
    //public void cargarNombreNivel(string nombre)
    //{

    //    Debug.Log("entra  en el cargar nombre");
    //    SceneManager.LoadScene(nombre);
    //}

    public void cargarNombreNivel(string nombre)
    {

        Debug.Log("entra  en el cargar nombre"+nombre);
        SceneManager.LoadScene(nombre);
    }
}
