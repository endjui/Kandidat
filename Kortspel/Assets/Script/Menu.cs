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
    public void setHP(float arg)
    {
        //sätter hp på elementen och andra slidern, sparar värdet
        HPTEXTA.text = ("" + arg);
        HPTEXTB.text = HPTEXTA.text;
        startHP = arg;
    }
    public void setMana(float arg)
    {
        //sätter mana på elementen och andra slidern, sparar värdet
        ManaTEXTA.text = ("" + arg + "/" + arg);
        ManaTEXTB.text = ManaTEXTA.text;
        startMana = arg;
    }
    public void setTimer(float arg)
    {
        //sätter timer tiden på elementen och andra slidern, sparar värdet
        TimerTEXTA.text = ("" + arg);
        TimerTEXTB.text = TimerTEXTA.text;
        startTimer = arg;
    }
    public void setTutorial(bool arg)
    {
        tutorialActivation = arg;
    }
    //Returneringsfunktioner, hämtas i Game
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
        //kan behövas för grafik och musik
    }

    void Update()
    {
        //kan ev. behövas för grafik och musik
    }
}
