using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    private string trigger;
    private string keyword;
    private string power;

    public Ability()
    {
        trigger = "";
        keyword = "";
        power = "";
    }

    public Ability(string _trigger, string _keyword, string _power)
    {
        trigger = _trigger;
        keyword = _keyword;
        power = _power;
    }

    public string getTrigger() { return trigger; }
    public string getKeyword() { return keyword; }
    public string getPower() { return power; }

    public void setTrigger(string _trigger) { trigger = _trigger; }

    public void setKeyword(string _keyword) { keyword = _keyword; }

    public void setPower(string _power) { power = _power; }

    // Ability functions

    public void cardTriggered(Player player, Player opponent, int cardPosition)
    {
        switch (this.keyword)
        {
            case "playerDrawCard":
                // TBA
                break;

            case "pickCreatureToHand":
                // TBA
                break;

            case "changePlayerMana":
                // Add 1 mana to the player
                player.setAvailableMana(player.getAvailableMana() + int.Parse(power));
                break;

            case "changeOpponentMana":
                // Remove 1 mana from the opponent
                opponent.setAvailableMana(opponent.getAvailableMana() + int.Parse(power));
                break;

            case "attackPlayer":
                // TBA
                break;

            case "scry":
                // TBA
                break;

            case "stun":
                // TBA
                break;

            default:
                Debug.Log("Unvalid keyword. Check database for card: " + player.playerCards[cardPosition].name);
                break;
        }
    }


    // Triggers
        /* cardOnPlay -> When a card is played, trigger in SpawnCard file -> Klar!
         * cardDies -> When a card dies 
         * cardIsAttacked -> When a card is attacked
         * opponentPlayCard -> When opponent plays a card -> Klar!
         * overHP -> When player is over X amount of HP 
         * underHP -> When player is under X amound of HP 
         */ 

    // Keywords
        /* playerDrawCard -> A player draws X amount of cards in pile, UI element 
         * pickCreatureToHand -> A player picks out a specific creature, UI element
         * changePlayerMana -> Increase player mana, send in which player to change and how much -> Klar men hårdkodad
         * changeOpponentMana -> Decrease opponents mana -> Tror inte den funkar som den ska men kanske funkar
         * attackPlayer -> Attacks opponent instead of cards, call on attack player
         * scry -> Look at the top cards in the card pile -> UI element
         * stun -> Choose a creature that is unable to attack for x rounds
         */
}
