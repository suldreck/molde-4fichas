using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DiceRoller : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        DiceValues = new int[1];
        theStateManager = GameObject.FindObjectOfType<StateManager>();
    }

    public StateManager theStateManager;

    public int[] DiceValues;

    public Sprite[] DiceImageOne;
    public Sprite[] DiceImageZero;




    // Update is called once per frame
    void Update()
    {
		
    }

    public void RollTheDice()
    {

        if (theStateManager.IsDoneRolling == true)
        {
            // We've already rolled this turn.
            return;
        }

        // In Ur, you roll four dice (classically Tetrahedron), which
        // have half their faces have a value of "1" and half have a value
        // of zero.

        // You COULD roll actual physics enabled dice.

        // We are going to use random number generation instead.
       // Debug.Log("longitud penal"+ theStateManager.penal.Length);
        theStateManager.DiceTotal = Random.Range(1, 6);
        theStateManager.DiceTotal = 2;
        if (theStateManager.penal[theStateManager.CurrentPlayerId] > 0)
        {
            
            SceneManager.LoadScene("Game");

            theStateManager.IsDoneRolling = true;

            //theStateManager.penal[theStateManager.CurrentPlayerId]--;
            //theStateManager.NewTurn();
        }
        else
        {

        theStateManager.IsDoneRolling = true;
        } 
       // theStateManager.CheckLegalMoves();


        //Debug.Log("Rolled: " + DiceTotal);
    }
}
