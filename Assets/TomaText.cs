using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TomaText : MonoBehaviour {

	// Use this for initialization
	void Start () {
        caja = GameObject.FindObjectOfType<Text>();
        DontDestroyOnLoad(gameObject);  
	}
    Text caja;
  
    public int numero { get; set; }
                                       // Update is called once per frame
    void Update () {
        numero =int.Parse( caja.text);
	}
}
