using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStone : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        theStateManager = GameObject.FindObjectOfType<StateManager>();
        targetPosition = this.transform.position;
       
    }

    public Tile StartingTile;
    public Tile currentTile;
    public int PlayerId;
    public StoneStorage MyStoneStorage;

    public int penal = 0;//si entra en la casilla adquiere una penalizacion,turnos sin jugar
    StateManager theStateManager;

    Tile[] moveQueue;
    int moveQueueIndex;

    bool isAnimating = false;

    Vector3 targetPosition;
    Vector3 velocity;
    float smoothTime = 0.25f;
    float smoothTimeVertical = 0.1f;
    float smoothDistance = 0.01f;//extra 0.01f+0.8f
    float smoothHeight =0.5f ;//+0.5/*+0.8f*/;

    public PlayerStone stoneToBop;

	
    // Update is called once per frame
    void Update()
    {
        if (isAnimating == false)
        {
            // Nothing for us to do.
            return;
        }
              
        bool respuestaDistancia = Vector3.Distance(
               new Vector3(this.transform.position.x, targetPosition.y, this.transform.position.z),
               targetPosition) < smoothDistance;
        if (respuestaDistancia)
        {
            // We've reached the target position -- do we still have moves in the queue?
            if( 
                (moveQueue == null || moveQueueIndex == (moveQueue.Length))
                &&
                ((this.transform.position.y-smoothDistance) > targetPosition.y)
            )
            {
                // We are totally out of moves (and too high up), the only thing left to do is drop down.
                this.transform.position = Vector3.SmoothDamp(
                    this.transform.position, 
                    new Vector3(this.transform.position.x, targetPosition.y, this.transform.position.z), 
                    ref velocity, 
                    smoothTimeVertical);

               
            }
            else
            {
                // Right position, right height -- let's advance the queue
                AdvanceMoveQueue();
            }
        }
        else if (this.transform.position.y < (smoothHeight - smoothDistance))
        {
            // We want to rise up before we move sideways.
            this.transform.position = Vector3.SmoothDamp(
                this.transform.position, 
                new Vector3(this.transform.position.x, smoothHeight, this.transform.position.z), 
                ref velocity, 
                smoothTimeVertical);
        }
        else
        {//nada
            // Normal movement (sideways)
            this.transform.position = Vector3.SmoothDamp(
                this.transform.position, 
                new Vector3(targetPosition.x, smoothHeight, targetPosition.z), 
                ref velocity, 
                smoothTime);
        }

    }

    void AdvanceMoveQueue()
    {
        if (moveQueue != null && moveQueueIndex < moveQueue.Length  && penal==0)
        {

            Tile nextTile = moveQueue[moveQueueIndex];
          //  Debug.Log("nextTitle : " + nextTile);
            if (nextTile == null)
            {
                // We are probably being scored
                // TODO: Move us to the scored pile
                SetNewTargetPosition(this.transform.position + Vector3.right * 10f);
            }
            else
            {
                SetNewTargetPosition(nextTile.transform.position);
                moveQueueIndex++;
            }
        }
        else
        {
            // The movement queue is empty, so we are done animating!
            this.isAnimating = false;
            theStateManager.IsDoneAnimating = true;

            // Are we on a roll again space?
            if (currentTile != null)
            {
                if (currentTile.tag.Equals("Finish"))
                {
                    theStateManager.isFinish[theStateManager.CurrentPlayerId] = true;
                    theStateManager.finalistas++;

                }
                if ( currentTile.IsRollAgain)
                {
                   // moveQueue = null;
                    theStateManager.RollAgain();
                    
                }
                if (penal== 0)
                {
                    if (currentTile.isCarcel)
                    {//ToDo pregunta
                       theStateManager.penal[theStateManager.CurrentPlayerId]= 3;
                       
                    }
                    if (currentTile.isPosada)
                    {//ToDo pregunta
                       theStateManager.penal[theStateManager.CurrentPlayerId]= 2;
                       
                    }
                    if (currentTile.isPozo)
                    {//ToDo pregunta
                       theStateManager.penal[theStateManager.CurrentPlayerId]= 5;
                      
                    }
                    if (currentTile.isOca)
                    {//ToDo pregunta
                        if (currentTile.ocaAoca != null)
                        {
                            Vector3 oca = currentTile.ocaAoca.transform.position;
                            this.transform.position = oca;
                            currentTile = currentTile.ocaAoca;
                            // targetPosition = Vector3.zero;
                            targetPosition = currentTile.transform.position;
                        }
                    }
                }

            }
        }

    }

    void SetNewTargetPosition(Vector3 pos)
    {
        Vector3 desplazamiento = new Vector3(0, 0, 0);
        float altura = 0 ;
        
       
        switch (theStateManager.CurrentPlayerId)
        {
            case 0:
                desplazamiento = new Vector3(0.5f, altura, 0.5f);
                break;
            case 1:
                desplazamiento = new Vector3(-0.5f, altura, 0.5f);
                break;
            case 2:
                desplazamiento = new Vector3(0.5f, altura, -0.5f);
                break;
            case 3:
                desplazamiento = new Vector3(-0.5f, altura, -0.5f);
                break;

            default:

                break;
        }
        targetPosition = pos + desplazamiento;
        velocity = Vector3.zero;     
        isAnimating = true;
    }

    void OnMouseUp()
    {
        // TODO:  Is the mouse over a UI element? In which case, ignore this click.

        // Is this the correct player?
        
        if (theStateManager.CurrentPlayerId != PlayerId)
        {
            return;
        }

        // Have we rolled the dice?
        if (theStateManager.IsDoneRolling == false)
        {
            // We can't move yet.
            return;
        }
        if (theStateManager.IsDoneClicking == true)
        {
            // We've already done a move!
            return;
        }
        if (theStateManager.penal[theStateManager.CurrentPlayerId] > 0)
        {
            if (theStateManager.contestacion == true)
            {
                theStateManager.penal[theStateManager.CurrentPlayerId] = 0;//penal a 0
            }
            else
            {
                theStateManager.penal[theStateManager.CurrentPlayerId]--;//un turno menos de penalizacion pero paso al siguiente 
                theStateManager.NewTurn();
                return;
            }
        }

            int spacesToMove = theStateManager.DiceTotal;
        if (spacesToMove == 0)
        {
            return;
        }

        // Where should we end up?
        moveQueue = GetTilesAhead(spacesToMove);
        Tile finalTile = moveQueue[ moveQueue.Length-1 ];

        // TODO: Check to see if the destination is legal!

       

        this.transform.SetParent(null); // Become Batman

        // Remove ourselves from our old tile
        if(currentTile != null)
        {
            currentTile.PlayerStone = null;
        }

        // Put ourselves in our new tile.
        finalTile.PlayerStone = this;

        moveQueueIndex = 0;
        currentTile = finalTile;
        theStateManager.IsDoneClicking = true;
        this.isAnimating = true;
    }

    // Return the list of tiles __ moves ahead of us
    Tile[] GetTilesAhead( int spacesToMove )
    {
        if (spacesToMove == 0)
        {
            return null;
        }

        // Where should we end up?

        Tile[] listOfTiles = new Tile[spacesToMove];
        Tile finalTile = currentTile;

        for (int i = 0; i < spacesToMove; i++)
        {
            if (finalTile == null)
            {
                finalTile = StartingTile;
            }
            else
            {
                if (finalTile.NextTiles[0] == null )
                {
                    // We are overshooting the victory -- so just return some nulls in the array
                    // Just break and we'll return the array, which is going to have nulls
                    // at the end.

                    for (int j = i; j < spacesToMove; j++)
                    {
                        finalTile = finalTile.NextTiles[1];
                        listOfTiles[j] = finalTile;
                    }
                    break;
                }
                //else if (finalTile.NextTiles.Length > 1)
                //{
                //    //// Branch based on player id
                //    //finalTile = finalTile.NextTiles[PlayerId];
                //}
                else
                {
                    finalTile = finalTile.NextTiles[0];
                }
            }

            listOfTiles[i] = finalTile;
        }

        return listOfTiles;
    }

    // Return the final tile we'd land on if we moved __ spaces
    Tile GetTileAhead( int spacesToMove )
    {
        Tile[] tiles = GetTilesAhead( spacesToMove );

        if(tiles == null)
        {
            // We aren't moving at all, so just return our current tile?
            return currentTile;
        }

        return tiles[ tiles.Length-1 ];
    }

    public bool CanLegallyMoveAhead( int spacesToMove )
    {
        Tile theTile = GetTileAhead( spacesToMove );

        return CanLegallyMoveTo( theTile );
    }

    bool CanLegallyMoveTo( Tile destinationTile )
    {

        if(destinationTile == null)
        {
            // NOTE!  A null tile means we are overshooting the victory roll
            // and this is NOT legal (apparently) in the Royal Game of Ur
            return false;


            // We're trying to move off the board and score, which is legal
            //Debug.Log("We're trying to move off the board and score, which is legal");
            //return true;
        }

        // Is the tile empty?
        if(destinationTile.PlayerStone == null)
        {
            return true;
        }

        // Is it one of our stones?
        if(destinationTile.PlayerStone.PlayerId == this.PlayerId)
        {
            // We can't land on our own stone.
            return false;
        }

        // If it's an enemy stone, is it in a safe square?
        if( destinationTile.IsRollAgain == true )
        {
            // Can't bop someone on a safe tile!
            return false;
        }

        // If we've gotten here, it means we can legally land on the enemy stone and
        // kick it off the board.
        return true;
    }

    public void ReturnToStorage()
    {
        //currentTile.PlayerStone = null;
        //currentTile = null;

        moveQueue = null;

        // Save our current position
        Vector3 savePosition = this.transform.position;

        MyStoneStorage.AddStoneToStorage( this.gameObject );

        // Set our new position to the animation target
        SetNewTargetPosition(this.transform.position);

        // Restore our saved position
        this.transform.position = savePosition;
    }

}

