using UnityEngine;
using UnityEngine.UI;

public class TurnSystem : MonoBehaviour
{
    //UI Buttons for Player1 and Player2s
    public Button player1button;
    public Button player2button;

    void Start()
    {
        // Player 1 end button
        Button btn1 = player1button.GetComponent<Button>();
        btn1.onClick.AddListener(Player1_endButtonTask);

        // Player 2 end button
        Button btn2 = player2button.GetComponent<Button>();
        btn2.onClick.AddListener(Player2_endButtonTask);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Player1_endButtonTask()
    {
        // If Player 1 is active, let them press the button and change phase, otherwise do nothing
        Debug.Log("Player 1 end button press.");
        if (Game.activePlayers[0].getIsActive())
        {
            Debug.Log("Player 1 active and end button pressed.");

            //If the player is in Attack phase end their round
            //Reset monsters attack bool, increase mana limit and set available mana.
            if (Game.activePlayers[0].getPlayerPhase().text == "Attack")
            {
                Game.activePlayers[0].resetHasAttacked();
                Game.activePlayers[0].setManaLimit(Game.activePlayers[0].getManaLimit() + 1);
                Game.activePlayers[0].setAvailableMana(Game.activePlayers[0].getManaLimit());
            }
            changeUI(Game.activePlayers[0], Game.activePlayers[1]);
        }
    }

    public void Player2_endButtonTask()
    {
        // If Player 2 is active, let them press the button and change phase, otherwise do nothing
        Debug.Log("Player 2 end button press.");
        if (Game.activePlayers[1].getIsActive())
        {
            Debug.Log("Player 2 active and end button pressed.");

            //If the player is in Attack phase end their round
            //Reset monsters attack bool, increase mana limit and set available mana.
            if (Game.activePlayers[1].getPlayerPhase().text == "Attack"){
                Game.activePlayers[1].resetHasAttacked();
                Game.activePlayers[1].setManaLimit(Game.activePlayers[1].getManaLimit() + 1);
                Game.activePlayers[1].setAvailableMana(Game.activePlayers[1].getManaLimit());
            }
            changeUI(Game.activePlayers[1], Game.activePlayers[0]);
        }
    }

    public static void changeUI(Player currentplayer, Player opponent)
    {
        // If on current player's attack phase
        if (currentplayer.getPlayerPhase().text == "Attack")
        {
            // Increase player's turn counter
            currentplayer.playerTurnCounter++;

            // Change player phase text and opponent phase text
            currentplayer.setPlayerPhase("Opponents Turn");
            opponent.setPlayerPhase("Play Card");
            
            // Change player end turn button text and opponent end turn button text
            currentplayer.setPlayerEndTurnButton_text("");
            opponent.setPlayerEndTurnButton_text("Go To Attack");

            // End the current player's turn and start opponent's turn
            currentplayer.setIsActive(false);
            opponent.setIsActive(true);
        }
        else
        {
            // If not on attack phase: Go to current player's attack phase (Update phase text to attack and button text to end turn)
            currentplayer.setPlayerPhase("Attack");
            currentplayer.setPlayerEndTurnButton_text("End Turn");
        }
    }

    public static void firstUIChange(Player currentplayer, Player opponent)
    {
        // Change player phase text and opponent phase text
        currentplayer.setPlayerPhase("Play Card");
        opponent.setPlayerPhase("Opponents Turn");

        // Change player end turn button text and opponent end turn button text
        currentplayer.setPlayerEndTurnButton_text("Go To Attack");
        opponent.setPlayerEndTurnButton_text("");
    }
}