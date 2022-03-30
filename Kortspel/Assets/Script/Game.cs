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

    // Text for the player phases and buttons
    public Text player1Phase;
    public Text player1EndTurnButton_text;
    public Text player2Phase;
    public Text player2EndTurnButton_text;

    // The text for the clock
    public Text player1_textTimer;
    public Text player2_textTimer;


    // Start is called before the first frame update
    void Start()
    {

        Player player1 = new Player(0, 40, false, "player1", player1Phase, player1EndTurnButton_text, player1_textTimer);
        Player player2 = new Player(0, 40, false, "player2", player2Phase, player2EndTurnButton_text, player2_textTimer);

        //Setting player0 to isActive = true for now, later we should coinflip this
        activePlayers.Add(player1);
        activePlayers.Add(player2);

        //flip a coin on who should start
        if (coinflip() <= 0.5f)
        {
            activePlayers[0].setIsActive(true);
            TurnSystem.firstUIChange(activePlayers[0], activePlayers[1]);
            Debug.Log("player1 will start");
        }
        else
        {
            activePlayers[1].setIsActive(true);
            TurnSystem.firstUIChange(activePlayers[1], activePlayers[0]);
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
