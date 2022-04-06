using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Creature : MonoBehaviour
{

    public int HP = 0;
    public int Attack = 0;
    public string Tribe = "huh";
    public string Name = "fe";
    public string Description = "jhhiji";
    public int Mana = 0;
    public int Zone = 0;
    public bool hasAttacked = false;

    public Text HPTEXT;
    public Text AttackTEXT;
    public Text TribeTEXT;
    public Text NameTEXT;
    public Text DescriptionTEXT;
    public Text ManaTEXT;


    //This is what calls on initialization
    void Start() { }


        //We need to call getCard() here and save the information in our list

   



    //This is called every frame
    void Update() {
        //setCardVisuals();
        //In here we need to apply damage and other things applied to the card.
        //Example: If(Card == Attacked){ Set the new visuals, deleted card if dead etc.}

    }

    // Set card information, random for now.
    public void setCardInformation(Cards card)
    {

        HP = card.getHp();
        Attack = card.getAttack();
        Zone = 0;
        Tribe = "pog";
        Name = card.getName();
        Description = "me is a very stronk";
        Mana = card.getMana();
        hasAttacked = false;
        setCardVisuals();


    }
    //Set the visuals on the card to match the information.
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

    //Get the card information from the database
    public void getCard()
    {
      //Get information from the database about the specific card.

        }

    public string getName() { return Name; }
    public int getMana() { return Mana; }
    public int getHp() { return HP; }
    public int getAttack() { return Attack; }
    public string getTribe() { return Tribe; }
    public string getDescription() { return Description; }
    public bool getHasAttacked() { return hasAttacked; }

    public void setName(string var)
    {
        Name = var;
        NameTEXT.text = "" + Name;
    }

    public void setAttack(int arg)
    {
        Attack = arg;
        AttackTEXT.text = "" + Attack;
    }
    public void setHp(int arg)
    {
        HP = arg;
        HPTEXT.text = "" + HP;
    }

    public void setHasAttacked()
    {
        if (hasAttacked)
        {
            hasAttacked = false;
        }
        else hasAttacked = true;
    }


}
