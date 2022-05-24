using UnityEngine;
using UnityEngine.UI;

public class Spell : MonoBehaviour
{
    //Variables for the the Spell
    public int hp = 0;
    public int attack = 0;
    public string tribe = "";
    public string name = "";
    public string description = "";
    public int mana = 0;
    public Sprite artworkSprite;

    //UI Text for the Spell
    public Text AttackTEXT;
    public Text TribeTEXT;
    public Text NameTEXT;
    public Text DescriptionTEXT;
    public Text ManaTEXT;
    public Image artworkImage;

    //Set card variables with the input 
    //argument card
    public void setCardInformation(Card card)
    {
        hp = card.getCardHP();
        attack = card.getAttack();
        tribe = card.getTribe();
        name = card.getCardName();
        description = card.getDescription();
        mana = card.getCardMana();
        artworkSprite = card.getArtworkSprite();
        setCardVisuals();
    }

    //Set the UI visuals on the card to match the variables
    public void setCardVisuals()
    {
        TribeTEXT.text = "" + tribe;
        NameTEXT.text = "" + name;
        DescriptionTEXT.text = "" + description;
        ManaTEXT.text = "" + mana;
        artworkImage.sprite = artworkSprite;
    }
    
    //Get the name of the Spell
    public string getSpellName() { return name; }

    //Get the mana cost of the Spell
    public int getSpellMana() { return mana; }

    //Get the Spell HP
    public int getSpellHP() { return hp; }

    //Get the Attack power of the Spell
    public int getSpellAttack() { return attack; }

    //Get the Spell tribe
    public string getSpellTribe() { return tribe; }

    //Get the Spell description
    public string getSpellDescription() { return description; }

    //Sets the name of the Spell
    //Both UI and variable
    public void setSpellName(string arg)
    {
        name = arg;
        NameTEXT.text = "" + name;
    }

    //Sets the Attack power of the Spell
    //Both UI and variable
    public void setSpellAttack(int arg)
    {
        attack = arg;
        AttackTEXT.text = "" + attack;
    }

    //Sets the HP of the Spell
    //Both UI and variable
    public void setSpellHP(int arg)
    {
        hp = arg;
    }
}
