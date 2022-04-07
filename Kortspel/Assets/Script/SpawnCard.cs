using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Newtonsoft.Json;

public class SpawnCard : MonoBehaviour
{
    public Button scanButton;
    public GameObject creaturePrefab;
    public Creature myCreature;
    const int maxZones = 5;
    public GameObject[] zonesP1 = new GameObject[maxZones];
    public GameObject[] zonesP2 = new GameObject[maxZones];

    public static Cards cardToSpawn = null;

    public TextAsset jsonFile;

    public CardList CardsInJson;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = scanButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        CardsInJson = JsonConvert.DeserializeObject<CardList>(jsonFile.text);

    }

    // Update is called once per frame
    void Update()
    {
       
    }


    void TaskOnClick()
    {
        //This is really bad coding. It should be re-written but I will have it like this for now.
        if (Game.activePlayers[0].getIsActive())
        {
            // Check if it is Attack phase, if it is not, let the active player play a card
            if (Game.activePlayers[0].getPlayerPhase().text != "Attack")
            {
                spawnCard(matchCard("https://drive.google.com/uc?export=download&id=1YBJnwk_kGLlgzbyQPFpG8XeQpWv60mIX"), Game.activePlayers[0], zonesP1);
            }
         }
        else if(Game.activePlayers[1].getIsActive())
        {
            // Check if it is Attack phase, if it is not, let the active player play a card
            if (Game.activePlayers[1].getPlayerPhase().text != "Attack")
            {

                spawnCard(matchCard("Assets/Resources/Images/robin_stick.png"), Game.activePlayers[1], zonesP2);
            }
        }
    }


    //Matches card in database and returns the card in Cards format
    //Uses URL right now, can be changed to name or anything else
    public Cards matchCard(string cardUrl)
    {
        Cards foundCard = new Cards();
        Debug.Log("Trying to match card");
        foreach(Cards thisCard in CardsInJson.cardList)
        {
            //Before thisCard.getName() == cardName
            if (thisCard.getPath() == cardUrl)
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
                                     thisCard.getPath());
            }
        }

        if(foundCard.getHp() == -1)
        {

            Debug.Log("Card was not found");

        }
        return foundCard;

    }


    public void spawnCard(Cards spawn, Player p, GameObject[] zones)
    {
        if (spawn.getHp() != -1 && (p.getAvailableMana() - spawn.getMana()) >= 0)
        {
            int zone = p.getAvailableZone();
            if (zone != -1)
            {

                if (spawn.getType() == "creature")
                {
                    //Debug.Log("Spawning creature");
                    GameObject cardName = Instantiate(creaturePrefab, zones[zone].transform.position, zones[zone].transform.rotation, zones[zone].transform) as GameObject;
                    cardName.name = spawn.getName();
                    //Create an acessable "creature"
                    myCreature = cardName.GetComponent<Creature>();

                    myCreature.setCardInformation(spawn);
                    //Debug.Log(myCreature.NameTEXT.text);

                    p.setAvailableMana(p.getAvailableMana() - myCreature.getMana());

                    p.playerCards[zone] = cardName;
                    //Debug.Log("" + spawn.getName()); 
                }
                //Debug.Log("Zone was not -1 trying to spawn creature...");
            }
            else
            {
                //Debug.Log("No Zone available");

            }
        } else {
            Debug.Log("No mana available for current player");
        }



    }

    //Returns the first available zone for the player from left to right
    public int getZone(Player p)
    {
        return p.getAvailableZone();
    }
}
