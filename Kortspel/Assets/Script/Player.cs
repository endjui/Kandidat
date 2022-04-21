using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Player : MonoBehaviour
{
    //Set the maxCards allowed on the playing field.
    //If != 5, the gameobjects, which represent the zones,
    //must be changed.
    const int maxCards = 5;

    //A list contining all of the players cards.
    public List<GameObject> playerCards = new List<GameObject>(); 

    //Players HP
    int Hp;

    //Boolean that states if it's the players turn or not
    bool isActive;

    //The maximum mana a player can spend per round.
    //This is increamented after everyround.
    int manaLimit = 1;

    //Maximum Player HP
    int maxHP = 40;

    //Maximum Player Mana
    int maxMana = 10;

    //Available mana a player can use.
    int availableMana = 1;

    //Player name
    string playerName;

    //If anything needs to be updated on the UI
    //hasChanged = true
    public bool hasChanged = false;

    //For player phases
    public Text playerPhase;
    public Text playerEndTurnButton_text;
    public int playerTurnCounter;

    // FOr player timer
    // The text for the clock
    public Text textTimer;

    //constructor that initilizes the variables.
    public Player(int MANA, int HP, bool isACTIVE, string playerNAME, Text playerPHASE, Text playerBUTTON, Text playerTIMER)
    {
        Hp = HP;
        manaLimit = MANA;
        isActive = isACTIVE;
        playerName = playerNAME;
        playerPhase = playerPHASE;
        playerEndTurnButton_text = playerBUTTON;
        textTimer = playerTIMER;

        //Create a dummy gameobject to initilaze the PlayerCards list
        //Every gamobject is set to null
        GameObject dummyObject = null;

         for(int counter = 0; counter < maxCards; counter++)
         {
             
             playerCards.Add(dummyObject);

         }
    }

    void Update()
    {
        
    }

    //Get if the players is active or not
    public bool getIsActive(){ return isActive;}

    //Get maxCards allowed on the field
    public int getMaxCards() { return maxCards;}

    //set players isActive variable
    public void setIsActive(bool arg){ isActive = arg;}

    //Set the players current HP
    //set hasChanged = true,  to update UI
    public void setHP(int arg){
        Hp = arg;
        hasChanged = true;
    }

    //Set the players current available Mana
    //set hasChanged = true,  to update UI
    public void setAvailableMana(int arg)
    {
        availableMana = arg;
        hasChanged = true;
    }

    //Get the players availableMana
    public int getAvailableMana(){ return availableMana;}

    //Get the players current HP
    public int getHP(){return Hp;}

    //Set a manaLimit
    public void setManaLimit(int arg)
    {
        manaLimit = arg;
        hasChanged = true;
    }

    //Get the players manaLimit
    public int getManaLimit(){return manaLimit;}

    //Set the players current phase
    public void setPlayerPhase(String arg){playerPhase.text = arg;}

    //Get the playesr current phase
    public Text getPlayerPhase(){return playerPhase;}

    //Set the player Endturn button text
    public void setPlayerEndTurnButton_text(String arg){playerEndTurnButton_text.text = arg;}

    //Set the players turnCounter
    public void setPlayerTurnCounter(int arg){playerTurnCounter = arg;}

    //Set the timers text
    public void setTimer(String arg){textTimer.text = arg;}

    //Get the first none occupide zone
    //Will return the first zone available in the PlayerCards list
    //If no zone is available, returns -1
    public int getAvailableZone()
    {
        int slot = -1;
        for (int i = 0; i < getMaxCards(); i++)
        {
            if (playerCards[i] == null)
            {
                Debug.Log("Found available slot " + i);
                slot = i;
                break;
            }

        }
        return slot;
    }

    //Resets the bool HasAttacked, for every Card/Gameobject in PlayerCards
    //(Sets the boolean to false for every card)
    public void resetHasAttacked()
    {
        for(int i = 0; i < maxCards; i++)
        {
            if(playerCards[i] != null)
            {
                if(playerCards[i].GetComponent<Creature>().getHasAttacked())
                {
                     playerCards[i].GetComponent<Creature>().setHasAttacked();
                }
            }
        }
        Debug.Log("Reset all cards");
    }
}