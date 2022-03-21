using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer: MonoBehaviour
{
    public Button player1button;
    public Button player2button;

    public float timeRemaining = 30.0f;
    public float resetvalue = 30.0f;

    void Start()
    {
        // Player 1 end button
        Button btn1 = player1button.GetComponent<Button>();
        btn1.onClick.AddListener(Player1_endButtonTask);

        // Player 2 end button
        Button btn2 = player2button.GetComponent<Button>();
        btn2.onClick.AddListener(Player2_endButtonTask);
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

    void Update()
    {
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
                TurnSystem.changeUI(Game.activePlayers[0], Game.activePlayers[1]);
            }
            else if (Game.activePlayers[1].getIsActive())
            {
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
