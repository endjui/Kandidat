using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void ReadyCheck()
    {
        GameObject player_1_is_ready = GameObject.Find("Checkmark_1");
        GameObject player_2_is_ready = GameObject.Find("Checkmark_2");
        //Startar spelet om bägge checkmarksen är aktiva
        if (player_1_is_ready && player_2_is_ready)
        {
            PlayGame();
        }
    }
    public void PlayGame()
    {
        Debug.Log("Game starts!");
        //Byter scen från meny till spelet
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        //Stänger av applikationen
        Debug.Log("Game quits");
        Application.Quit();
    }

    void Start()
    {
        //kan behövas för grafik och musik
    }

    void Update()
    {
        //kan ev. behövas för grafik och musik
    }
}
