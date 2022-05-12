using UnityEngine;
using UnityEngine.UI;

public class Creature : MonoBehaviour
{
    //Variables for the the Creature
    public int hp = 0;
    public int attack = 0;
    public string tribe = "";
    public string name = "";
    public string description = "";
    public int mana = 0;
    public int zone = 0;
    public bool hasAttacked = false;

    //UI Text for the Creature
    public Text HPTEXT;
    public Text AttackTEXT;
    public Text TribeTEXT;
    public Text NameTEXT;
    public Text DescriptionTEXT;
    public Text ManaTEXT;

    //Set card variables with the input 
    //argument card
    public void setCardInformation(Card card)
    {
        hp = card.getCardHP();
        attack = card.getAttack();
        zone = 0;
        tribe = card.getTribe();
        name = card.getCardName();
        description = card.getDescription();
        mana = card.getCardMana();
        hasAttacked = false;
        setCardVisuals();
    }

    //Set the UI visuals on the card to match the variables
    public void setCardVisuals()
    {

        HPTEXT.text = "" + hp;
        AttackTEXT.text = "" + attack;
        TribeTEXT.text = "" + tribe;
        NameTEXT.text = "" + name;
        DescriptionTEXT.text = "" + description;
        ManaTEXT.text = "" + mana;
    }

    //Get the name of the creature
    public string getCreatureName() { return name; }

    //Get the mana cost of the creature
    public int getCreatureMana() { return mana; }

    //Get the creatures HP
    public int getCreatureHP() { return hp; }

    //Get the Attack power of the creature
    public int getAttack() { return attack; }

    //Get the creatures tribe
    public string getTribe() { return tribe; }

    //Get the creatures description
    public string getDescription() { return description; }
    public void getCard()
    {
        //Get information from the database about the specific card.

    }

    //Get the variable hasAttacked
    public bool getHasAttacked() { return hasAttacked; }

    //Sets the name of the creature
    //Both UI and variable
    public void setCreatureName(string arg)
    {
        name = arg;
        NameTEXT.text = "" + name;
    }

    //Sets the Attack power of the creature
    //Both UI and variable
    public void setAttack(int arg)
    {
        attack = arg;
        AttackTEXT.text = "" + attack;
    }

    //Sets the HP of the creature
    //Both UI and variable
    public void setCreatureHP(int arg)
    {
        hp = arg;
        HPTEXT.text = "" + hp;
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
