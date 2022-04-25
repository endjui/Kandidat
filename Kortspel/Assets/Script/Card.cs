using Newtonsoft.Json;

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

    [JsonProperty("keywords")]
    private string keywords;

    [JsonProperty("path")]
    private string path;

    //A boolean for if the card has attack this round or not
    //If == True => card has attacked this round
    private bool hasAttacked;

    //Constructor that sets the variables
    public Card(string _name, string _type, int _mana, int _hp, int _attack, string _tribe, string _description, string _keywords, string _path)
    {
        name = _name;
        type = _type;
        mana = _mana;
        hp = _hp;
        attack = _attack;
        tribe = _tribe;
        description = _description;
        hasAttacked = false;
        keywords = _keywords;
        path = _path;
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
        keywords = "";
        path = "";
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
    //Get the cards keywords
    public string getKeywords() { return keywords; }
    //Get the cards path
    public string getPath() { return path; }

    //Sets the values of the card to specified input
    public void setValues(string _name, string _type, int _mana, int _hp, int _attack, string _tribe, string _description, string _keywords, string _path)
    {
        name = _name;
        type = _type;
        mana = _mana;
        hp = _hp;
        attack = _attack;
        tribe = _tribe;
        description = _description;
        keywords = _keywords;
        path = _path;
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
}
