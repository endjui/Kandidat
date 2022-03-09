using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Game : MonoBehaviour
{
    public static List<Player> activePlayers = new List<Player>();


    public Text HPTEXTPlayer1;
    public Text ManaTEXTPlayer1;
    public Text HPTEXTPlayer2;
    public Text ManaTEXTPlayer2;


    // Start is called before the first frame update
    void Start()
    {
        //Setting player0 to isActive = true for now, later we should coinflip this
        activePlayers.Add(new Player(0,40,false,"player1"));
        activePlayers.Add(new Player(0, 40, false, "player2"));

        //flip a coin on who should start
        if (coinflip() <= 0.5f)
        {
            activePlayers[0].setIsActive(true);
            Debug.Log("player1 will start");
        }
        else
        {
            activePlayers[1].setIsActive(true);
            Debug.Log("player2 will start");
        }



    }   

    // Update is called once per frame
    void Update()
    {

        //if statmenet for which players turn it is. 
        if(activePlayers[0].getIsActive())
        {
        

        }else if(activePlayers[1].getIsActive())
        {


        }

    }
    //flips a coin on which players should start
    float coinflip()
    {
        //will return a value from 0 - 1
        return Random.Range(0f, 1f);


    }

    //Set values for the match
    void setStartValues()
    {


    }

}
