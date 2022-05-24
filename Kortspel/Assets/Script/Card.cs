using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    //these variables are case sensitive and must match the strings in the JSON.
    //Sets the variables from the Json database
    [JsonProperty("name")]
    private string name;

    [JsonProperty("type")]
    private string type;

    [JsonProperty("mana")]
    private int mana;

    [JsonProperty("hp")]
    private int hp;

    [JsonProperty("attack")]
    private int attack;

    [JsonProperty("tribe")]
    private string tribe;

    [JsonProperty("description")]
    private string description;

    [JsonProperty("path")]
    private string path;

    [JsonProperty("triggers")]
    private string triggers;

    [JsonProperty("keywords")]
    private string keywords;

    [JsonProperty("powers")]
    private string powers;

    [JsonProperty("image")]
    private string image;

    //A boolean for if the card has attack this round or not
    //If == True => card has attacked this round
    private bool hasAttacked;

    private Sprite artwork;

    // List of abilities for the card
    private List<Ability> abilities = new List<Ability>();

    //Constructor that sets the variables
    public Card(string _name, string _type, int _mana, int _hp, int _attack, string _tribe, string _description, string _path, string _triggers, string _keywords, string _powers, string _image)
    {
        name = _name;
        type = _type;
        mana = _mana;
        hp = _hp;
        attack = _attack;
        tribe = _tribe;
        description = _description;
        hasAttacked = false;
        path = _path;
        triggers = _triggers;
        keywords = _keywords;
        powers = _powers;

        //Create sprite
        artwork = Resources.Load<Sprite>(_image);

        setAbilities(_triggers, _keywords, _powers);
    }

    //Default constructor. Sets the mana,hp & attack to -1
    public Card()
    {
        name = "";
        type = "";
        mana = -1;
        hp = -1;
        attack = -1;
        tribe = "";
        description = "";
        hasAttacked = false;
        path = "";
        triggers = "";
        keywords = "";
        powers = "";
    }

    //Get the name of the card
    public string getCardName() { return name; }
    //Get the typ of the card
    public string getType() { return type; }
    //Get the mana cost for the card
    public int getCardMana() { return mana; }
    //Get the Hp for the card
    public int getCardHP() { return hp; }
    //Get the Attack power for the card
    public int getAttack() { return attack; }
    //Get the cards tribe
    public string getTribe() { return tribe; }
    //Get the cards description text
    public string getDescription() { return description; }
    //Get the HasAttacked boolean
    public bool getHasAttacked() { return hasAttacked; }
    //Get the cards path
    public string getPath() { return path; }
    //Get the cards triggers
    public string getTriggers() { return triggers; }
    //Get the cards keywords
    public string getKeywords() { return keywords; }
    //Get the cards powers
    public string getPowers() { return powers; }
    //Get the cards abilities
    public List<Ability> getAbilities() { return abilities; }
    //Get a specific ability from a card
    public Ability getAbility(int i) { return abilities[i]; }

    public Sprite getArtworkSprite(){return artwork;}

    public string getImage() { return image;}


    //Sets the values of the card to specified input
    public void setValues(string _name, string _type, int _mana, int _hp, int _attack, string _tribe, string _description, string _path, string _triggers, string _keywords, string _powers, string _image)
    {
        name = _name;
        type = _type;
        mana = _mana;
        hp = _hp;
        attack = _attack;
        tribe = _tribe;
        description = _description;
        path = _path;
        triggers = _triggers;
        keywords = _keywords;
        powers = _powers;

        //Create sprite
        artwork = Resources.Load<Sprite>(_image);

        setAbilities(_triggers, _keywords, _powers);
    }

    //Set the cards attack variable
    public void setAttack(int arg){attack = arg;}

    //Set the cards hp variable
    public void setCardHP(int arg){hp = arg;}

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

    //Set the abilities for the card by splitting the database strings
    public void setAbilities(string _triggers, string _keywords, string _powers)
    {
        // Split the keywords and triggers into multiple strings
        // Split when encountering a comma (',')
        char splitWhen = ',';
        string[] splitTriggers = _triggers.Split(splitWhen);
        string[] splitKeywords = _keywords.Split(splitWhen);
        string[] splitPowers = _powers.Split(splitWhen);

        // Triggers and keywords need to be the same length
        // If there is more then one keyword that have the same trigger, repeat the trigger in the list
        Debug.Log(splitTriggers.Length);
        for (int i = 0; i < splitTriggers.Length; i++)
        {
            abilities.Add(new Ability(splitTriggers[i], splitKeywords[i], splitPowers[i]));
            Debug.Log("Ability added with trigger: " + abilities[i].getTrigger());
            Debug.Log("With the keyword: " + abilities[i].getKeyword());
            Debug.Log("And the power: " + abilities[i].getPower());
        }
    }
}
