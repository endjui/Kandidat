using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

unsafe public class Player : MonoBehaviour
{
    const int maxCards = 5;
    public SortedList zonesAndCards = new SortedList();
    //public List<cards> playersCards = new List<cards>(); 
    int Hp;
    bool isActive;
    int Mana;
    int maxHP = 40;
    int maxMana = 10;
    string playerName;

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
        Mana = MANA;
        isActive = isACTIVE;
        playerName = playerNAME;
        playerPhase = playerPHASE;
        playerEndTurnButton_text = playerBUTTON;
        textTimer = playerTIMER;

        for(int counter = 0; counter < maxCards; counter++)
        {
            zonesAndCards.Add(counter, null);

        }
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
    }

    public int getHP()
    {
        return Hp;
    }

    public void setMana(int y)
    {
        Mana = y;
    }

    public int getMana()
    {
        return Mana;
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
}