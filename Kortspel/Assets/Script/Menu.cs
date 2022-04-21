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
        //Startar spelet om b�gge checkmarksen �r aktiva
        if (player_1_is_ready && player_2_is_ready)
        {
            PlayGame();
        }
    }
    public void PlayGame()
    {
        Debug.Log("Game starts!");
        //Byter scen fr�n meny till spelet
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        //St�nger av applikationen
        Debug.Log("Game quits");
        Application.Quit();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
