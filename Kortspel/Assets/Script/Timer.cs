using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    //UI buttons for Player 1 and Player 2
    public Button player1button;
    public Button player2button;
    public Image circle;
    //Variables for how long every phase should be.
    //30.f = 30 seconds
    public float timeRemaining;
    public float resetvalue;
    public float percent;

    void Start()
    {
        
        // Player 1 end button
        Button btn1 = player1button.GetComponent<Button>();
        btn1.onClick.AddListener(Player1_endButtonTask);

        // Player 2 end button
        Button btn2 = player2button.GetComponent<Button>();
        btn2.onClick.AddListener(Player2_endButtonTask);
    }
    //gets called by Game with starting time set by Player in menu
    public void setTime(float arg){
        timeRemaining = arg;
        resetvalue = arg;
    }
    void Player1_endButtonTask()
    {
        // If Player 1 is active, let them press the button and reset the timer, otherwise do nothing
        if (Game.activePlayers[0].getIsActive())
        {
            timeRemaining = resetvalue;
        }
    }

    void Player2_endButtonTask()
    {
        // If Player 2 is active, let them press the button and reset the timer, otherwise do nothing
        if (Game.activePlayers[1].getIsActive())
        {
            timeRemaining = resetvalue;
        }
    }
    void updateCircle(float arg)
    {
        percent = arg / resetvalue;
        circle.fillAmount = percent;
    }
    void Update()
    {
        updateCircle(timeRemaining);
        if (timeRemaining > 0.0f)
        {
            // Countdown timer
            // Takes away one second per frame
            timeRemaining -= Time.deltaTime;
            DisplayTime(timeRemaining);
        }
        else
        {
            Debug.Log("Time has run out!");

            // Resets timer
            if (Game.activePlayers[0].getIsActive())
            {
                //If the timer has run out and the player is in Attack phase
                //Reset hasAttacked, increase mana limit and set available mana
                if (Game.activePlayers[0].getPlayerPhase().text == "Attack"){
                    Game.activePlayers[0].resetHasAttacked();
                    Game.activePlayers[0].setManaLimit(Game.activePlayers[0].getManaLimit() + 1);
                    Game.activePlayers[0].setAvailableMana(Game.activePlayers[0].getManaLimit());
                }
                TurnSystem.changeUI(Game.activePlayers[0], Game.activePlayers[1]);
                
            }
            else if (Game.activePlayers[1].getIsActive())
            {
                //If the timer has run out and the player is in Attack phase
                //Reset hasAttacked, increase mana limit and set available mana
                if (Game.activePlayers[1].getPlayerPhase().text == "Attack"){
                    Game.activePlayers[1].resetHasAttacked();
                    Game.activePlayers[1].setManaLimit(Game.activePlayers[1].getManaLimit() + 1);
                    Game.activePlayers[1].setAvailableMana(Game.activePlayers[1].getManaLimit());
                }
                TurnSystem.changeUI(Game.activePlayers[1], Game.activePlayers[0]);
                
            }
            timeRemaining = resetvalue;
        }
    }

    void DisplayTime(float timeToDisplay)
    {   
        // Calculates minutes and seconds
        // Converts double to an int
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        // Converts float to string for each player
        Game.activePlayers[0].setTimer(string.Format("{0:00}:{1:00}", minutes, seconds));
        Game.activePlayers[1].setTimer(string.Format("{0:00}:{1:00}", minutes, seconds));
    }
}
