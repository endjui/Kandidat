using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    //To obtain values set by players in menu
    public Menu menu;
    public Timer timer;
    
    //UI to show active player, covers opposite part of screen
    public GameObject coverUI1;
    public GameObject coverUI2;
    
    // Start is called before the first frame update
    void Start()
    {
        //Gets the values set by the player from the menuscreen
        int startMana = menu.getMana();
        int startHP = menu.getHP();
        //Passes on the turntimer to timer class
        timer.setTime(menu.getTimer());

        //Adds 2 new Players to the activePlayers list.
        //A config could be added here to set max HP or other variables.
        activePlayers.Add(new Player(startMana, startHP, false, "player1", player1Phase,player1EndTurnButton_text, player1_textTimer));
        activePlayers.Add(new Player(startMana, startHP, false, "player2", player2Phase,player2EndTurnButton_text, player2_textTimer));
        
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
        if(activePlayers[1].getIsActive() == true)
        {
            coverUI1.SetActive(true);
            coverUI2.SetActive(false);
        }
        else
        {
            coverUI1.SetActive(false);
            coverUI2.SetActive(true);
        }

        //Is called every frame to check if any information about the player has changed.
        //If information has changed, update the UI or end the game.
        if (activePlayers[0].hasChanged)
        {
            Debug.Log("Update");
            HPTEXTPlayer1.text = ("" + activePlayers[0].getPlayerHP());
            ManaTEXTPlayer1.text = ("" + activePlayers[0].getAvailableMana() + " / " + activePlayers[0].getManaLimit());

            if(activePlayers[0].getPlayerHP() <= 0)
            {
                Application.Quit();
            }
            activePlayers[0].hasChanged = false;
        }

        if (activePlayers[1].hasChanged)
        {
            
            HPTEXTPlayer2.text = ("" + activePlayers[1].getPlayerHP());
            ManaTEXTPlayer2.text = ("" + activePlayers[1].getAvailableMana() + " / " + activePlayers[1].getManaLimit());

            if (activePlayers[1].getPlayerHP() <= 0)
            {
                Application.Quit();
                
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); 

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
   
}
