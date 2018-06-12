using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noDestruir : MonoBehaviour {
    public static noDestruir juegoVariable;
    private void Awake()
    {
        
        if(juegoVariable ==null)
        {juegoVariable=this;
            DontDestroyOnLoad(gameObject);
         }else if(juegoVariable!=this)//no es null pero tampoco es lo q teniamos antes  
            {
            Destroy(gameObject);
            }

    }
    
    // Use this for initialization
    void Start()
    {
        jugadores = GameObject.FindObjectsOfType<PlayerStone>();
        //hand = GameObject.Find("Player3-StoneStorage ");

        //if (hand.GetType() == typeof(PlayerStone))
        //    Debug.Log("Es del mismo tipo que playerstone");
    
        theStateManager = GameObject.FindObjectOfType<StateManager>();
        //switch (theStateManager.NumberOfPlayers)
        //{
        //    case 2:
        //        var player2=GameObject.Find("Player2-Stone");
        //        break;
        //    case 3:
        //        GameObject.Find("Player2-stone").SetActive(true);
        //        GameObject.Find("Player3-Stone").SetActive(true);
        //        break;
        //    case 4:
        //        GameObject.Find("Player2-Stone").SetActive(true);
        //        GameObject.Find("Player3-Stone").SetActive(true);
        //        GameObject.Find("Player4-Stone").SetActive(true);
        //        break;
        //    default:
        //        break;

        //}
        //GameObject.Find("Player1-Stone").SetActive(true);

    }
    StateManager theStateManager;
    PlayerStone [] jugadores;
    public GameObject hand;
    // Update is called once per frame
    void Update () {
		
	}
}
