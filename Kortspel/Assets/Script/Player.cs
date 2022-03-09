using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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

    //constructor that initilizes the variables.
    public Player(int MANA, int HP, bool isACTIVE, string playerNAME)
    {
        Hp = HP;
        Mana = MANA;
        isActive = isACTIVE;
        playerName = playerNAME;

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

}