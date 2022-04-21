using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;


public class Cards
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
    public Cards(string Name, string Type, int Mana, int Hp, int Attack, string Tribe, string Description, string Keywords, string Path)
    {
        name = Name;
        type = Type;
        mana = Mana;
        hp = Hp;
        attack = Attack;
        tribe = Tribe;
        description = Description;
        hasAttacked = false;
        keywords = Keywords;
        path = Path;
    }

    //Default constructor. Sets the mana,hp & attack to -1
    public Cards()
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
    public string getName() { return name; }
    //Get the typ of the card
    public string getType() { return type; }
    //Get the mana cost for the card
    public int getMana() { return mana; }
    //Get the Hp for the card
    public int getHp() { return hp; }
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
    public void setValues(string Name, string Type, int Mana, int Hp, int Attack, string Tribe, string Description, string Keywords, string Path)
    {
        name = Name;
        type = Type;
        mana = Mana;
        hp = Hp;
        attack = Attack;
        tribe = Tribe;
        description = Description;
        keywords = Keywords;
        path = Path;
    }

    //Set the cards attack variable
    public void setAttack(int arg){attack = arg;}

    //Set the cards hp variable
    public void setHp(int arg){hp = arg;}

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
