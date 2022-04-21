using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Creature : MonoBehaviour
{
    //Variables for the the Creature
    public int HP = 0;
    public int Attack = 0;
    public string Tribe = "";
    public string Name = "";
    public string Description = "";
    public int Mana = 0;
    public int Zone = 0;
    public bool hasAttacked = false;

    //UI Text for the Creature
    public Text HPTEXT;
    public Text AttackTEXT;
    public Text TribeTEXT;
    public Text NameTEXT;
    public Text DescriptionTEXT;
    public Text ManaTEXT;


    //This is what calls on initialization
    void Start() { }

    //This is called every frame
    void Update() {
    }

    //Set card variables with the input 
    //argument card
    public void setCardInformation(Cards card)
    {
        HP = card.getHp();
        Attack = card.getAttack();
        Zone = 0;
        Tribe = card.getTribe();
        Name = card.getName();
        Description = card.getDescription();
        Mana = card.getMana();
        hasAttacked = false;
        setCardVisuals();
    }

    //Set the UI visuals on the card to match the variables
    public void setCardVisuals()
    {

        HPTEXT.text = "" + HP;
        AttackTEXT.text = "" + Attack;
        TribeTEXT.text = "" + Tribe;
        NameTEXT.text = "" + Name;
        DescriptionTEXT.text = "" + Description;
        ManaTEXT.text = "" + Mana;
        Debug.Log("wooooow");
    }
    
    //Get the name of the creature
    public string getName() { return Name; }
    
    //Get the mana cost of the creature
    public int getMana() { return Mana; }
    
    //Get the creatures HP
    public int getHp() { return HP; }
    
    //Get the Attack power of the creature
    public int getAttack() { return Attack; }
    
    //Get the creatures tribe
    public string getTribe() { return Tribe; }

    //Get the creatures description
    public string getDescription() { return Description; }

    //Get the variable hasAttacked
    public bool getHasAttacked() { return hasAttacked; }

    //Sets the name of the creature
    //Both UI and variable
    public void setName(string arg)
    {
        Name = arg;
        NameTEXT.text = "" + Name;
    }

    //Sets the Attack power of the creature
    //Both UI and variable
    public void setAttack(int arg)
    {
        Attack = arg;
        AttackTEXT.text = "" + Attack;
    }

    //Sets the HP of the creature
    //Both UI and variable
    public void setHp(int arg)
    {
        HP = arg;
        HPTEXT.text = "" + HP;
    }

    //Changes the value of hasAttacked
    //If hasAttacked == true, will change to false.
    //If hasAttacked == false, will change to true.
    public void setHasAttacked()
    {
        if (hasAttacked)
        {
            hasAttacked = false;
        }
        else hasAttacked = true;
    }


}
