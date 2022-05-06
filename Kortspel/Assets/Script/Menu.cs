using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
        private static float startHP;
        private static float startMana;
        private static float startTimer;
        private static bool tutorialActivation;
        public Text HPTEXTA;
        public Text ManaTEXTA;
        public Text TimerTEXTA;
        public Text HPTEXTB;
        public Text ManaTEXTB;
        public Text TimerTEXTB;
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
    public void setHP(float arg)
    {
        //s�tter hp p� elementen och andra slidern, sparar v�rdet
        HPTEXTA.text = ("" + arg);
        HPTEXTB.text = HPTEXTA.text;
        startHP = arg;
    }
    public void setMana(float arg)
    {
        //s�tter mana p� elementen och andra slidern, sparar v�rdet
        ManaTEXTA.text = ("" + arg + "/" + arg);
        ManaTEXTB.text = ManaTEXTA.text;
        startMana = arg;
    }
    public void setTimer(float arg)
    {
        //s�tter timer tiden p� elementen och andra slidern, sparar v�rdet
        TimerTEXTA.text = ("" + arg);
        TimerTEXTB.text = TimerTEXTA.text;
        startTimer = arg;
    }
    public void setTutorial(bool arg)
    {
        tutorialActivation = arg;
    }
    //Returneringsfunktioner, h�mtas i Game
    public int getHP()
    {
        return (int)startHP;
    }
    public int getMana()
    {
        return (int)startMana;
    }
    public float getTimer()
    {
        return startTimer;
    }
    public bool getTutorial()
    {
        return tutorialActivation;
    }
    void Start()
    {
        startHP = 30;
        startMana = 1;
        startTimer = 15;
        //kan beh�vas f�r grafik och musik
    }

    void Update()
    {
        //kan ev. beh�vas f�r grafik och musik
    }
}
