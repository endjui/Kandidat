using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


/***************************************/
/*******Main class for the game*********/
/***************************************/
public class Game : MonoBehaviour
{
    //A list containing both of the players. 
    //Static, can be accessed anywhere.
    public static List<Player> activePlayers = new List<Player>();

    //UI text for HP and mana
    public Text HPTEXTPlayer1;
    public Text ManaTEXTPlayer1;
    public Text HPTEXTPlayer2;
    public Text ManaTEXTPlayer2;

    //UI text for the player phases and end buttons
    public Text player1Phase;
    public Text player1EndTurnButton_text;
    public Text player2Phase;
    public Text player2EndTurnButton_text;

    //UI text for the clock
    public Text player1_textTimer;
    public Text player2_textTimer;


    // Start is called before the first frame update
    void Start()
    {
        //Adds 2 new Players to the activePlayers list.
        //A config could be added here to set max HP or other variables.
        activePlayers.Add(new Player(1, 40,false,"player1", player1Phase,player1EndTurnButton_text,player1_textTimer));
        activePlayers.Add(new Player(1, 40, false, "player2", player2Phase,player2EndTurnButton_text, player2_textTimer));

        //Randomly pick a player that goes first
        //For more information, check the coinflip() function
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
        
        //Is called everyframe to check if any information about the player has changed.
        //If information has changed, update the UI or end the game.
        //Could be added as a function to avoid repetitive code.
        if (activePlayers[0].hasChanged)
        {
            HPTEXTPlayer1.text = ("" + activePlayers[0].getHP());
            ManaTEXTPlayer1.text = ("" + activePlayers[0].getAvailableMana() + " / " + activePlayers[0].getManaLimit());

            if(activePlayers[0].getHP() <= 0)
            {
                Application.Quit();

            }
            activePlayers[0].hasChanged = false;
        }

        if (activePlayers[1].hasChanged)
        {
            HPTEXTPlayer2.text = ("" + activePlayers[1].getHP());
            ManaTEXTPlayer2.text = ("" + activePlayers[1].getAvailableMana() + " / " + activePlayers[1].getManaLimit());

            if (activePlayers[1].getHP() <= 0)
            {
                Application.Quit();

            }
            activePlayers[1].hasChanged = false;
        }

    }
    //flips a coin on which players should start
    public float coinflip()
    {
        //will return a value/float from 0 - 1.fs
        return Random.Range(0f, 1f);


    }

    //Set values for the match
    //???????
    void setStartValues()
    {


    }

}
