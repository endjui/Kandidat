using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class Attack : MonoBehaviour
{
    //Variables for the players
    public Player current = null;
    public Player opponent = null;

    //Button UI
    public Button attackButton;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize the button
        Button atkbtn = attackButton.GetComponent<Button>();
        atkbtn.onClick.AddListener(TaskOnClick);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Attack button press
    //Checks which players turn is is and make their creature attack the opponent HP or 
    //the opponents creature
    public void TaskOnClick()
    {
        //check if Player1 or Player2 is active
        //Set the variable current to the active player
        //and opponent to the inactive player
        if (Game.activePlayers[0].getIsActive())
        {
            current = Game.activePlayers[0];
            opponent = Game.activePlayers[1];

        } else { 
            
            current = Game.activePlayers[1];
            opponent = Game.activePlayers[0];
        }

        //Get the first zone that contains a creature which
        //can attack this round
        int targetZone = GetZone(current);
       
        //Check if the zone is != -1 and the player is in attack phase
        if (targetZone != -1 && current.getPlayerPhase().text == "Attack") 
        {
            //AttackCreature and remove their creatures if their HP 0 =<
            AttackCreature(current, opponent, targetZone);
            RemoveCreature(current);
            RemoveCreature(opponent);
        }
        
        

    }

    //Returns the first available zone where a Player arg
    //has a Creature that may attack this round
    public int GetZone(Player arg)
    {
        //zone initially -1, if no cards are found OR creature has attacked this round, we return -1.
        //Otherwise we return the first zone containing a card that has not attacked this turn

        int zone = -1;
        Creature dummy = new Creature();
        dummy = null;
        
        //check all zones 
        for (int i = 0; i < arg.getMaxCards(); i++)
        {
            // check if zone contains a card.
            if (arg.playerCards[i] != null)
            {
                dummy = arg.playerCards[i].GetComponent<Creature>();
                Debug.Log("Creature found" + dummy.getName());

                //Check if the Creature is able to attack this round or if 
                //the creature already has attacked
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

    //current and opponents cards placed in targetZone attack each other.
    //If opponent has no card in targetZone the current players creature
    //Attack the opponents current HP
    public void AttackCreature(Player current, Player opponent, int targetZone) {

        //get the Creature component for the attacker card
        Creature attacker = current.playerCards[targetZone].GetComponent<Creature>();

        //Check if the opponent has a card in the opposite zone as the attacker
        //else attack opponents HP
        if (opponent.playerCards[targetZone] != null)
        {
            //get the Creature component for the target card
            Creature target = opponent.playerCards[targetZone].GetComponent<Creature>();
            Debug.Log("The creature attacks the opponents creature");

            //Remove the attack value from the creatures HP
            //Both creatures take damage
            target.setHp(target.getHp() - attacker.getAttack());
            attacker.setHp(attacker.getHp() - target.getAttack());
        }
        else
        {
            //Attack the opponents current hp with the creatures Attack value
            opponent.setHP(opponent.getHP() - attacker.getAttack());
            Debug.Log("he attac HP, opponents hp is: " + opponent.getHP());
        }

        //Change the attacking cards bool hasAttacked to true.
        attacker.setHasAttacked();
    }

    //Check if any card of Player arg has HP 0<=.
    //If true, remove/Destroy the card
    public void RemoveCreature(Player arg)
    {
        
        for (int i = 0; i < arg.getMaxCards(); i++)
        {
            if (arg.playerCards[i] != null)
            {
                if (arg.playerCards[i].GetComponent<Creature>().getHp() <= 0)
                {
                    //Destroy the game object in playerCards and set the value to null
                    Destroy(arg.playerCards[i]);
                    arg.playerCards[i] = null;
                    Debug.Log("Creature has died in zone " + i);
                }
            }
            
        }
    }
    
}
