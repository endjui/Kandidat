using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;
using TMPro;

public class Menu : MonoBehaviour
{
        private static float startHP;
        private static float startMana;
        private static float startTimer;
        private static bool tutorialActivation;
        private bool changeScene;
        private float timer;
        public GameObject UI;
        public PostProcessVolume blurvolume;
        public Camera gameCamera;
        public Text HPTEXTA;
        public Text ManaTEXTA;
        public Text TimerTEXTA;
        public Text HPTEXTB;
        public Text ManaTEXTB;
        public Text TimerTEXTB;
        public TextMeshProUGUI countdown1;
        public TextMeshProUGUI countdown2;
    //private canvas = GetCOmponent
    public void ReadyCheck()
    {
        GameObject player_1_is_ready = GameObject.Find("Checkmark_1");
        GameObject player_2_is_ready = GameObject.Find("Checkmark_2");
        //Startar spelet om b�gge checkmarksen �r aktiva
        if (player_1_is_ready && player_2_is_ready)
        {
            UI.SetActive(false); //tar bort all ui i meny scenen
            Debug.Log("Game starts!");
            changeScene = true; //f�r smooth transition i update
            Debug.Log(changeScene);
        }
    }
    public void PlayGame()
    {
        Debug.Log("Game starts!");
        //Byter scen fr�n meny till spelet
        
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
    /*
    public void wtf()
    {
        gameObject.SetActive(false);
        changeScene = true;
        for(int i = 0; i < 100; i++)
        {
            blurvolume.weight = blurvolume.weight - 1.0f;
            gameCamera.fieldOfView = gameCamera.fieldOfView - 1.0f;
        }
        //blurvolume.weight = 0.0f;
        //gameCamera.fieldOfView = 70.0f;
    }*/
    void Start()
    {
        
        startHP = 30;
        startMana = 1;
        startTimer = 15;
        timer = 3.0f;
        //kan beh�vas f�r grafik och musik
    }

    void Update()
    {
        if(changeScene == true)
        {
            timer -= Time.deltaTime;
            blurvolume.weight = blurvolume.weight - 0.001f;
            gameCamera.fieldOfView = gameCamera.fieldOfView - 0.1f;
            countdown1.text = (""+((int)timer+1));
            countdown2.text = ("" +((int)timer+1));
        }
        if(timer <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        //kan ev. beh�vas f�r grafik och musik
    }
}
