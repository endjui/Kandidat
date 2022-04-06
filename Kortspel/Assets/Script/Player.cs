using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Player : MonoBehaviour
{
    const int maxCards = 5;
    //public SortedList<int, Cards> zonesAndCards = new SortedList<int, Cards>();
    public List<GameObject> playerCards = new List<GameObject>(); 
    int Hp;
    bool isActive;
    int manaLimit;
    int maxHP = 40;
    int maxMana = 10;
    int availableMana = 0;
    string playerName;
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

       GameObject hej = null;



         
         for(int counter = 0; counter < maxCards; counter++)
         {
             
             playerCards.Add(hej);

         }
        


    }

    void Update()
    {
        
    }

    //returns if the players is active or not
    public bool getIsActive()
    {
       return isActive;
    }
    //return maxCards
    public int getMaxCards() { return maxCards; }

    //set if the players isactive variable
    public void setIsActive(bool y)
    {

        isActive = y;
    }

    public void setHP(int y){
        Hp = y;
        hasChanged = true;

    }
    public void setAvailableMana(int arg)
    {
        availableMana = arg;
        hasChanged = true;
    }
    public int getAvailableMana()
    {
        return availableMana;
    }

    public int getHP()
    {
        return Hp;
    }

    public void setManaLimit(int y)
    {
        manaLimit = y;
        hasChanged = true;
    }

    public int getManaLimit()
    {
        return manaLimit;
    }

    public void setPlayerPhase(String s)
    {
        playerPhase.text = s;
    }

    public Text getPlayerPhase()
    {
        return playerPhase;
    }

    public void setPlayerEndTurnButton_text(String s)
    {
        playerEndTurnButton_text.text = s;
    }

    public void setPlayerTurnCounter(int i)
    {
        playerTurnCounter = i;
    }

    public void setTimer(String s)
    {
        textTimer.text = s;
    }

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


//attackfas -> target selection dyker upp för varje kort, selection visar zoner som är tomma eller ej för motståndaren, target & attackerande räknar ut damage och updaterar spelares HP
//ta bort kort och updatera zoner och selection, ta bort selection för kort som attackerat