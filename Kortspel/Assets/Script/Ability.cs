using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
            case "changePlayerMana":
                // Add mana to the player
                player.setAvailableMana(player.getAvailableMana() + int.Parse(power));
                break;

            case "changeOpponentMana":
                // Remove mana from the opponent
                opponent.setAvailableMana(opponent.getAvailableMana() + int.Parse(power));
                break;

            case "changePlayerHP":
                // Change the players HP (healing)
                player.setPlayerHP(player.getPlayerHP() + int.Parse(power));
                break;

            case "changeOpponentHP":
                // Change the opponent HP (damage)
                opponent.setPlayerHP(opponent.getPlayerHP() + int.Parse(power));
                break;

            case "scry":
                // Player may look at cards (no change in game, only in real life)
                Debug.Log("You may look at the top " + int.Parse(power) + " cards in the draw pile. When you are finished put them back in the same order.");
                break;

            case "drawNewCards":
                // Player may draw cards (no change in game, only in real life)
                Debug.Log("You may draw " + int.Parse(power) + " cards from the draw pile.");
                break;

            case "stealCards":
                // Player may steal cards from opponent (no change in game, only in real life)
                Debug.Log("You may take " + int.Parse(power) + " cards blindly from your opponent.");
                break;

            case "changeOneCreatureHP":
                // Check if the player has any creatures on the board, if they don't: break
                if (player.playerCards.Count == 0)
                {
                    Debug.Log("Player has no creature to change HP.");
                    break;
                }
                // Get a random number between 0 and x (the active zones)
                int rand = randomZone(player.playerCards.Count);

                // Get a random Creature component to change the HP (Loop through zones to find the random creature)
                for (int i = 0; i < player.getMaxCards(); i++)
                {
                    if (player.playerCards[i] != null)
                    {
                        rand--;
                    }
                    if (rand == 0)
                    {
                        Creature randomCreature = player.playerCards[i].GetComponent<Creature>();
                        randomCreature.setCreatureHP(randomCreature.getCreatureHP() + int.Parse(power));
                        break;
                    }

                }
                break;

            case "changeAllCreaturesHP":
                // Check if the player has any creatures on the board, if they don't: break
                if (player.playerCards.Count == 0)
                {
                    Debug.Log("Player has no creature to change HP.");
                    break;
                }

                // Change HP for all creatures that the player has
                for (int i = 0; i < player.getMaxCards(); i++)
                {
                    // If zone is not empty, change that creature HP
                    if (player.playerCards[i] != null)
                    {
                        Creature currentCreature = player.playerCards[i].GetComponent<Creature>();
                        currentCreature.setCreatureHP(currentCreature.getCreatureHP() + int.Parse(power));
                    }
                }
                break;

            case "changeAllCreaturesAttack":
                // Check if the player has any creatures on the board, if they don't: break
                if (player.playerCards.Count == 0)
                {
                    Debug.Log("Player has no creature to change attack.");
                    break;
                }

                // Change attack power for all creatures that the player has
                for (int i = 0; i < player.getMaxCards(); i++)
                {
                    // If zone is not empty, change that creature attack power
                    if (player.playerCards[i] != null)
                    {
                        Creature currentCreature = player.playerCards[i].GetComponent<Creature>();
                        currentCreature.setAttack(currentCreature.getAttack() + int.Parse(power));
                    }
                }
                break;

            case "changeOneOpponetCreatureAttack":
                // Check if the opponent has any creatures on the board, if they don't: break
                if (opponent.playerCards.Count == 0)
                {
                    Debug.Log("Opponent has no creature to change attack.");
                    break;
                }

                // Get a random number between 0 and x (the active zones)
                int ran = randomZone(opponent.playerCards.Count);

                // Get a random Creature component to change the attack (Loop through zones to find the random creature)
                for (int i = 0; i < opponent.getMaxCards(); i++)
                {
                    if (opponent.playerCards[i] != null)
                    {
                        ran--;
                    }
                    if (ran == 0)
                    {
                        // Get a random Creature component for to change the HP
                        Creature randomCreature = opponent.playerCards[ran].GetComponent<Creature>();
                        randomCreature.setAttack(randomCreature.getAttack() + int.Parse(power));
                    }
                }
                break;

            case "changeAllOpponentCreaturesHP":
                // Check if the player has any creatures on the board, if they don't: break
                if (opponent.playerCards.Count == 0)
                {
                    Debug.Log("Opponent has no creature to change HP.");
                    break;
                }

                // Change HP for all creatures that the opponent has
                for (int i = 0; i < opponent.getMaxCards(); i++)
                {
                    // If zone is not empty, change that creature HP
                    if (opponent.playerCards[i] != null)
                    {
                        Creature currentCreature = opponent.playerCards[i].GetComponent<Creature>();
                        currentCreature.setCreatureHP(currentCreature.getCreatureHP() + int.Parse(power));
                    }
                }
                break;

            case "changeAllOpponentCreaturesAttack":
                // Check if the player has any creatures on the board, if they don't: break
                if (opponent.playerCards.Count == 0)
                {
                    Debug.Log("Opponent has no creature to change attack.");
                    break;
                }

                // Change attack power for all creatures that the opponent has
                for (int i = 0; i < opponent.getMaxCards(); i++)
                {
                    // If zone is not empty, change that creature atack power
                    if (opponent.playerCards[i] != null)
                    {
                        Creature currentCreature = opponent.playerCards[i].GetComponent<Creature>();
                        currentCreature.setAttack(currentCreature.getAttack() + int.Parse(power));
                    }
                }
                break;

            case "changeThisCreatureHP":
                // Change the current creature HP
                Creature curCreature = player.playerCards[cardPosition].GetComponent<Creature>();
                curCreature.setCreatureHP(curCreature.getCreatureHP() + int.Parse(power));
                break;

            case "changeThisCreatureAttack":
                // Change the current creature attack power
                Creature currCreature = player.playerCards[cardPosition].GetComponent<Creature>();
                currCreature.setAttack(currCreature.getAttack() + int.Parse(power));
                break;

            case "changeThisCreatureAttackByTribe":
                // Change this creatures attack power based on how many creatures of the same tribe exists
                int tribePower = 0;
                Creature thisCreature = player.playerCards[cardPosition].GetComponent<Creature>();

                // Check for the players tribe creatures
                for (int i = 0; i < player.getMaxCards(); i++)
                {
                    // Get creature in slot i
                    Creature currentCreature = player.playerCards[i].GetComponent<Creature>();
                    // Check if the creature has the same tribe (if true, increment tribePower)
                    if (currentCreature.getTribe() == thisCreature.getTribe()) { tribePower++; }
                }

                // Check for the opponents tribe creatures
                for (int i = 0; i < opponent.getMaxCards(); i++)
                {
                    // Get creature in slot i
                    Creature currentCreature = opponent.playerCards[i].GetComponent<Creature>();
                    // Check if the creature has the same tribe (if true, increment tribePower)
                    if (currentCreature.getTribe() == thisCreature.getTribe()) { tribePower++; }
                }

                // Set attack power according to the tribePower
                thisCreature.setAttack(thisCreature.getAttack() + tribePower);
                break;

            case "changeAllTribeAttack":
                // Change all creatures of same tribe attack power (based on power)
                Creature thisTribeCreature = player.playerCards[cardPosition].GetComponent<Creature>();

                // Check for the players tribe creatures
                for (int i = 0; i < player.getMaxCards(); i++)
                {
                    // Get creature in slot i
                    Creature currentCreature = player.playerCards[i].GetComponent<Creature>();
                    // Check if the creature has the same tribe (if true, increase power)
                    if (currentCreature.getTribe() == thisTribeCreature.getTribe())
                    {
                        currentCreature.setAttack(currentCreature.getAttack() + int.Parse(power));
                    }
                }

                // Check for the opponents tribe creatures
                for (int i = 0; i < opponent.getMaxCards(); i++)
                {
                    // Get creature in slot i
                    Creature currentCreature = opponent.playerCards[i].GetComponent<Creature>();
                    // Check if the creature has the same tribe (if true, increase power)
                    if (currentCreature.getTribe() == thisTribeCreature.getTribe())
                    {
                        currentCreature.setAttack(currentCreature.getAttack() + int.Parse(power));
                    }
                }
                break;

            case "opponentCantAttack":
                // Not sure how to make this yet...
                Debug.Log("opponentCantAttack is not yet implemented.");
                break;

            default:
                Debug.Log("Unvalid keyword. Check database for card: " + player.playerCards[cardPosition].name);
                break;
        }
    }

    public int randomZone(int max)
    {
        // Will return a int between 0 and max
        float randFloat = UnityEngine.Random.Range(0f, max - 0.00001f);
        return (int)Math.Round(randFloat);
    }

    // Triggers
    /* cardOnPlay -> When a card is played (SpawnCard file) -> Klar!
     * opponentPlayCard -> When opponent plays a card (SpawnCard file) -> Klar! (Används ej?)
     * cardDies -> When a card dies (Attack file) -> Klar!
     * cardIsAttacked -> When a card is attacked (Attack file) -> Klar!
     * cardIsAttacking -> When a card is attacking (Attack file) -> Klar!
     * underHP -> When player is under X amound of HP (Player file) -> Klar! (MEN DEN FUNKAR INTE OM OPPONENT SKA ÄNDRAS AV ETT KEYWORD I TRIGGERN)
     */

    // Keywords
    /* changePlayerMana -> Increase player mana, send in which player to change and how much
     * changeOpponentMana -> Decrease opponents mana
     * changePlayerHP -> Change the HP of the current player (heal)
     * changeOpponentHP  -> Attacks opponent instead of cards (damage)
     * 
     * scry -> Look at the top X cards in the card pile -> UI element
     * drawNewCards -> A player draws X amount of cards in pile, UI element 
     * stealCards -> A player steals X amount of cards from the opponent, UI element 
     * 
     * changeOneCreatureHP -> Change the HP of one creature that the current player controls
     * changeAllCreaturesHP -> Change the HP of all creatures that the current player controls
     * changeAllCreaturesAttack -> Change the attack power of all creatures that the current player controls
     * 
     * changeOneOpponetCreatureAttack -> Change the attack power of one creature that the opponent controls
     * changeAllOpponentCreaturesHP -> Change the HP of all creatures that the opponent controls
     * changeAllOpponentCreaturesAttack -> Change the attack power of all creatures that the opponent controls
     * 
     * changeThisCreatureHP -> Change the HP of the creature that activated this ability
     * changeThisCreatureAttack -> Change the attack power of the creature that activated this ability
     * changeThisCreatureAttackByTribe -> Change the attack power of the creature that activated this ability based on the amount of Tribe cards
     * changeAllTribeAttack -> Change the attack power of all creature of a specific Tribe
     * 
     * opponentCantAttack -> Make the player unable to attack on their next turn -> Ej klar
     */
}
