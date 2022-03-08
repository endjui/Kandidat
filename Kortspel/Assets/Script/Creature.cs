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

    void Start() { }

    void Update() {
        HP = 24;
        Attack = 5;
        Zone = 0;
        Tribe = "pog";
        Name = "ADHD";
        Description = "me is a very stronk";
        Mana = 5;

        HPTEXT.text = "" + HP;
        AttackTEXT.text = ""+ Attack;
        TribeTEXT.text = "" + Tribe;
        NameTEXT.text = "" + Name;
        DescriptionTEXT.text = "" + Description;
        ManaTEXT.text = "" + Mana;

}

}
