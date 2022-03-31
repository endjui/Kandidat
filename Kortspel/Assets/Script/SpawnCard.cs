using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class SpawnCard : MonoBehaviour
{
    public Button scanButton;
    public GameObject creaturePrefab;
    const int maxZones = 5;
    public GameObject[] zonesP1 = new GameObject[maxZones];
    public GameObject[] zonesP2 = new GameObject[maxZones];

    public static cards cardToSpawn = null;

    public TextAsset jsonFile;

    public CardList cardsInJson;
    public WebCamTexture webCam;
    public Texture2D[] images;
    public Eigenface scanner;
    

    // Start is called before the first frame update
    void Start()
    {
        Button btn = scanButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        cardsInJson = JsonConvert.DeserializeObject<CardList>(jsonFile.text);

        // Get all camera devices and play the correct Camera
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length > 1)
        {
            webCam = new WebCamTexture(devices[1].name);
        }
        else
        {
            webCam = new WebCamTexture(devices[0].name);
        }

        webCam.Play();

        images = new Texture2D[cardsInJson.cardList.Length];
        int i = 0;

        IEnumerator DownloadImage(string MediaUrl, Texture2D[] imageArray)
        {
            Debug.Log(MediaUrl);
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
                Debug.Log(request.error);
            else
            {
                Debug.Log("i = " + i);
                images[i] = ((DownloadHandlerTexture)request.downloadHandler).texture;
                Debug.Log(((DownloadHandlerTexture)request.downloadHandler).texture);
                i++;
            }
        }

        foreach (cards card in cardsInJson.cardList)
        {
            StartCoroutine(DownloadImage(card.getUrl(),  images));
        }

        Debug.Log(images[0]);

        scanner = new Eigenface(webCam, images);
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    void TaskOnClick()
    {
        string url = scanner.getId(webCam);

        //This is really bad coding. It should be re-written but I will have it like this for now.
        if (Game.activePlayers[0].getIsActive())
        {
            // Check if it is Attack phase, if it is not, let the active player play a card
            if (Game.activePlayers[0].getPlayerPhase().text != "Attack")
            {
                spawnCard(matchCard(url), Game.activePlayers[0], zonesP1);
            }
         }
        else if(Game.activePlayers[1].getIsActive())
        {
            // Check if it is Attack phase, if it is not, let the active player play a card
            if (Game.activePlayers[1].getPlayerPhase().text != "Attack")
            {
                spawnCard(matchCard(url), Game.activePlayers[1], zonesP2);
            }
        }
    }


    //Matches card in database and returns the card in cards format
    public cards matchCard(string cardUrl)
    {
        cards foundCard = new cards();
        Debug.Log("Trying to match card");
        foreach(cards thisCard in cardsInJson.cardList)
        {
            if(thisCard.getUrl() == cardUrl)
            {
                Debug.Log("Found creature: " + thisCard.getName() + "! " + thisCard.getDescription());

                foundCard.setValues(thisCard.getName(),
                                     thisCard.getType(),
                                     thisCard.getMana(),
                                     thisCard.getHp(),
                                     thisCard.getAttack(),
                                     thisCard.getTribe(),
                                     thisCard.getDescription(),
                                     thisCard.getKeywords(),
                                     thisCard.getUrl());
            }
        }

        if(foundCard.getHp() == -1)
        {

            Debug.Log("Card was not found");

        }
        return foundCard;

    }


    public void spawnCard(cards spawn,Player p, GameObject[] zones)
    {
        if (spawn.getHp() != -1)
        {
            int zone = getZone(p);
            if (zone != -1)
            {

                if (spawn.getType() == "creature")
                {
                    Debug.Log("Spawning creature");
                    Instantiate(creaturePrefab, zones[zone].transform.position, zones[zone].transform.rotation, zones[zone].transform);
                    p.zonesAndCards[zone] = spawn;
                }
                Debug.Log("Zone was not -1 trying to spawn creature...");
            }
            else
            {
                Debug.Log("No Zone available");

            }
        } else {
            Debug.Log("spawn was null");
        }



    }

    //Returns the first available zone for the player from left to right
    public int getZone(Player p)
    {
        int slot = -1;
        for (int i = 0; i < p.getMaxCards(); i++)
        {
            if(p.zonesAndCards.GetByIndex(i) == null)
            {
                Debug.Log("Found available slot " + p.zonesAndCards.GetKey(i));
                slot = i;
                break;
            }

        }
        return slot;
    }

}
