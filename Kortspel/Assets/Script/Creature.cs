using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Creature : MonoBehaviour
{
    public int HP;
    public int Attack;
    public string Tribe;
    public string Name;
    public string Description;
    public int Mana;
    public int Zone;

    public Text HPTEXT;
    public Text AttackTEXT;
    public Text TribeTEXT;
    public Text NameTEXT;
    public Text DescriptionTEXT;
    public Text ManaTEXT;

    //This is what calls on initialization
    void Start()
    {
        //We need to call getCard() here and save the information in our list
        setCardInformation();
        setCardVisuals();
    }

    //This is called every frame
    void Update()
    {
        //In here we need to apply damage and other things applied to the card.
        //Example: If(Card == Attacked){ Set the new visuals, deleted card if dead etc.}
        
    }


    // Set card information, random for now.
    void setCardInformation()
    {
        HP = Random.Range(0,  5);
        Attack = Random.Range( 0, 5);
        Zone = 0;
        Tribe = "pog";
        Name = "Death";
        Description = "me is a very stronk";
        Mana = Attack = Random.Range( 0,  5);

    }
    //Set the visuals on the card to match the information.
    void setCardVisuals()
    {

        HPTEXT.text = "" + HP;
        AttackTEXT.text = "" + Attack;
        TribeTEXT.text = "" + Tribe;
        NameTEXT.text = "" + Name;
        DescriptionTEXT.text = "" + Description;
        ManaTEXT.text = "" + Mana;
    }

    //Get the card information from the database
    void getCard()
    {
      //Get information from the database about the specific card.

    }
}
