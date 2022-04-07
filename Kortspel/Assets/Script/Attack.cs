using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class Attack : MonoBehaviour
{

    public Player current = null;
    public Player opponent = null;
    public Button attackButton;
    // Start is called before the first frame update
    void Start()
    {
        Button atkbtn = attackButton.GetComponent<Button>();
        atkbtn.onClick.AddListener(TaskOnClick);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TaskOnClick()
    {
      current = Game.activePlayers[0];

        if (current.getIsActive()) opponent = Game.activePlayers[1];
        else {
            opponent = Game.activePlayers[0];
            current = Game.activePlayers[1];
             }
        
        int targetZone = GetZone(current);
       
        if (targetZone != -1 && current.getPlayerPhase().text == "Attack") 
        {
            AttackCreature(current, opponent, targetZone);
            RemoveCreature(current);
            RemoveCreature(opponent);
        }
        
        

    }


    public int GetZone(Player arg)
    {
        //zone initially -1, if no cards are found we return -1.
        //Otherwise we return the first zone containing a card that has not attacked this turn

        int zone = -1;
        Creature dummy = new Creature();
        dummy = null;
        
        //check all zones (5)
        for (int i = 0; i < arg.getMaxCards(); i++)
        {
            // check if zone contains a card.
            if (arg.playerCards[i] != null)
            {

                dummy = arg.playerCards[i].GetComponent<Creature>();

                Debug.Log("Creature found" + dummy.getName());
                if (dummy.getHasAttacked() == false)
                {
                    zone = i;
                    Debug.Log(dummy.getName() + "Can attack this round");
                    break;
                } else Debug.Log("No creature can attack this round");
            }

        }
        
        return zone;
    }

    
    public void AttackCreature(Player current, Player opponent, int targetZone) {

        Creature attacker = current.playerCards[targetZone].GetComponent<Creature>();

        if (opponent.playerCards[targetZone] != null)
        {
            Creature target = opponent.playerCards[targetZone].GetComponent<Creature>();
            Debug.Log("he attac");
            target.setHp(target.getHp() - attacker.getAttack());
            attacker.setHp(attacker.getHp() - target.getAttack());
        }
        else
        {
            opponent.setHP(opponent.getHP() - attacker.getAttack());
            Debug.Log("he attac HP, opponents hp is: " + opponent.getHP());
        }

        attacker.setHasAttacked();

    
    }


    public void RemoveCreature(Player arg)
    {
        
        for (int i = 0; i < arg.getMaxCards(); i++)
        {
            if (arg.playerCards[i] != null)
            {
                if (arg.playerCards[i].GetComponent<Creature>().getHp() <= 0)
                {

                    
                    Destroy(arg.playerCards[i]);
                    arg.playerCards[i] = null;
                    Debug.Log("Card ded");
                }
            }
            
        }
    }
    
}
