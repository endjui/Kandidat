using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Newtonsoft.Json;

public class SpawnCard : MonoBehaviour
{
    //UI Button to scan/spawn monsters
    public Button scanButton;

    //The cre   aturePrefab to spawn
    public GameObject creaturePrefab;

    //Variable for the creature class
    public Creature myCreature;

    //The spellPrefab to spawn
    public GameObject spellPrefab;

    //Variable for the spell class
    public Spell mySpell;

    //Max zones available for both players

    const int maxZones = 5;

    //Zones for spells
    public GameObject spellZoneP1;
    public GameObject spellZoneP2;

    //Arrays of gameobjects containing the position/transform for the
    //zones for player 1 and player2.
    public GameObject[] zonesP1 = new GameObject[maxZones];
    public GameObject[] zonesP2 = new GameObject[maxZones];

    public static Card cardToSpawn = null;

    //The database file
    public TextAsset jsonFile;

    //An array of Cards
    public CardList cardsInJson;

    //Image recognition variables
    public WebCamTexture webCam;
    public Texture2D[][] images;
    public Eigenface scanner;

    // Start is called before the first frame update
    void Start()
    {
        //Initilize the button
        Button btn = scanButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);

        //Convert JSon database and add to the CardList array
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

        images = new Texture2D[cardsInJson.cardList.Length][];
        for (int i = 0; i < images.Length; i++)
        {
            //images[i] = Resources.Load<Texture2D>(cardsInJson.cardList[i].getPath());
            images[i] = Resources.LoadAll<Texture2D>(cardsInJson.cardList[i].getPath());
        }

        //scanner = new Eigenface(webCam, images);
        scanner = new Eigenface(ref images);
    }

    void TaskOnClick()
    {
        //Check if Player1 is active.
        if (Game.activePlayers[0].getIsActive())
        {
            // Check if Player 1 is in Attack phase, if not, let the player scan a card
            if (Game.activePlayers[0].getPlayerPhase().text != "Attack")
            {

                Card cardToBeSpawned = matchCard(scanner.matchImage(ref webCam, ref cardsInJson.cardList));
                //Card cardToBeSpawned = matchCard("Images/RandomFlyingArrow");

                // Check if there is available mana
                if (cardToBeSpawned.getCardHP() != -1 && (Game.activePlayers[0].getAvailableMana() - cardToBeSpawned.getCardMana()) >= 0)
                {
                    // Loop through the card to be spawneds abilites
                    for (int i = 0; i < cardToBeSpawned.getAbilities().Count; i++)
                    {
                        // Check if the current ability has cardOnPlay trigger
                        if (cardToBeSpawned.getAbility(i).getTrigger() == "cardOnPlay")
                        {
                            // Call cardTriggered for the ability
                            cardToBeSpawned.getAbility(i).cardTriggered(Game.activePlayers[0], Game.activePlayers[1], i);
                            Debug.Log("CardOnPlay was triggered!");
                        }
                    }
                    //Spawns the card being scanned if the player has enough avilable Mana and there are available zones
                    spawnCard(cardToBeSpawned, Game.activePlayers[0], zonesP1, spellZoneP1);

                    // Loop through opponents cards
                    for (int i = 0; i < Game.activePlayers[1].getCards().Count; i++)
                    {
                        // Check if zone is empty
                        if (Game.activePlayers[1].getCard(i) != null)
                        {
                            Creature dummyCreature = Game.activePlayers[1].getCard(i).GetComponent<Creature>();
                            Card dummyCard = new Card();
                            // Find cards that the opponent has in play
                            foreach (Card thisCard in cardsInJson.cardList)
                            {
                                // If a match from the CardsInJason is found, put it in dummyCard
                                if (dummyCreature.getCreatureName() == thisCard.getCardName())
                                {
                                    dummyCard = matchCard(thisCard.getPath());
                                }
                            }
                            // Loop through one of the opponent card abilities
                            for (int j = 0; j < dummyCard.getAbilities().Count; j++)
                            {
                                // Check if current trigger = opponentPlayCard
                                if (dummyCard.getAbility(j).getTrigger() == "opponentPlayCard")
                                {
                                    // Call cardTriggered for the ability to be triggered
                                    dummyCard.getAbility(j).cardTriggered(Game.activePlayers[1], Game.activePlayers[0], i);
                                    Debug.Log("opponentPlayCard was triggered!");
                                }
                            }
                        }
                    }
                }
            }
         }

        //Check if Player2 is active.
        else if (Game.activePlayers[1].getIsActive())
        {
            // Check if Player 2 is in Attack phase, if not, let the player scan a card
            if (Game.activePlayers[1].getPlayerPhase().text != "Attack")
            {
                Card cardToBeSpawned = matchCard(scanner.matchImage(ref webCam, ref cardsInJson.cardList));
                //Card cardToBeSpawned = matchCard("Images/RandomFlyingArrow");

                // Check if there is available mana
                if (cardToBeSpawned.getCardHP() != -1 && (Game.activePlayers[1].getAvailableMana() - cardToBeSpawned.getCardMana()) >= 0)
                {
                    // Loop through the card to be spawneds abilites
                    for (int i = 0; i < cardToBeSpawned.getAbilities().Count; i++)
                    {
                        // Check if the current ability has cardOnPlay trigger
                        if (cardToBeSpawned.getAbility(i).getTrigger() == "cardOnPlay")
                        {
                            // Call cardTriggered for the ability
                            cardToBeSpawned.getAbility(i).cardTriggered(Game.activePlayers[1], Game.activePlayers[0], i);
                            Debug.Log("CardOnPlay was triggered!");
                        }
                    }
                    //Spawns the card being scanned if the player has enough avilable Mana and there are available zones
                    spawnCard(cardToBeSpawned, Game.activePlayers[1],zonesP2 ,spellZoneP2);
                    
                    // Loop through opponents cards
                    for (int i = 0; i < Game.activePlayers[0].getCards().Count; i++)
                    {
                        // Check if zone is empty
                        if (Game.activePlayers[0].getCard(i) != null)
                        {
                            Creature dummyCreature = Game.activePlayers[0].getCard(i).GetComponent<Creature>();
                            Card dummyCard = new Card();
                            // Find cards that the opponent has in play
                            foreach (Card thisCard in cardsInJson.cardList)
                            {
                                // If a match from the CardsInJason is found, put it in dummyCard
                                if (dummyCreature.getCreatureName() == thisCard.getCardName())
                                {
                                    dummyCard = matchCard(thisCard.getPath());
                                }
                            }
                            // Loop through one of the opponent card abilities
                            for (int j = 0; j < dummyCard.getAbilities().Count; j++)
                            {
                                // Check if current trigger = opponentPlayCard
                                if (dummyCard.getAbility(j).getTrigger() == "opponentPlayCard")
                                {
                                    // Call cardTriggered for the ability to be triggered
                                    dummyCard.getAbility(j).cardTriggered(Game.activePlayers[0], Game.activePlayers[1], i);
                                    Debug.Log("opponentPlayCard was triggered!");
                                }
                            }
                        }
                    }
                }
            }
        }
    }


    //Matches card in database and returns the card in Cards format
    //Uses the Path of the card to match
    //This function will return a card with HP =-1, if no card was found
    public Card matchCard(string cardPath)
    {
        Card foundCard = new Card();
        Debug.Log("Trying to match card");

        //Goes through the database to find a card with the same path as the input argument
        foreach(Card card in cardsInJson.cardList)
        {
            //If a card is found set foundCard's variables to the correct
            //values from the database
            if (card.getPath() == cardPath)
            {
                Debug.Log("Found creature: " + card.getCardName() + "! " + card.getDescription());

                foundCard.setValues(card.getCardName(),
                                     card.getType(),
                                     card.getCardMana(),
                                     card.getCardHP(),
                                     card.getAttack(),
                                     card.getTribe(),
                                     card.getDescription(),
                                     card.getPath(),
                                     card.getTriggers(),
                                     card.getKeywords(),
                                     card.getPowers(),
                                     card.getImage());
            }
        }
        //Card was not found
        if (foundCard.getCardHP() == -1)
        {
            Debug.Log("Card was not found");
        }
        return foundCard;
    }

    //Spawns the card "spawn" for Player "P" in available zone
    //If no zone is avaialble no card will spawn
    //If input argument spawn.getHp() = -1, no card will be spawned
    public void spawnCard(Card spawn, Player p, GameObject[] zones, GameObject spellZone)
    {
        //Check if conditions are met to spawn the card
        if (spawn.getCardHP() != -1 && (p.getAvailableMana() - spawn.getCardMana()) >= 0)
        {
            if (spawn.getType() == "spell")
            {
                if(p.spellCard == null){ 
                Debug.Log("Playing spellcard.");

                GameObject instantiatedSpell = Instantiate(spellPrefab, spellZone.transform.position, spellZone.transform.rotation, spellZone.transform) as GameObject;
                instantiatedSpell.name = spawn.getCardName();

                mySpell = instantiatedSpell.GetComponent<Spell>();

                mySpell.setCardInformation(spawn);

                p.setAvailableMana(p.getAvailableMana() - mySpell.getSpellMana());

                p.spellCard = instantiatedSpell;
                }
                else
                {
                    Debug.Log("Player already cast a spell this round..");
                }
        }

            int zone = p.getAvailableZone();
            if (zone != -1)
            {
                //Check if the card is of type creature
                if (spawn.getType() == "creature")
                {
                    Debug.Log("Spawning creature ...");

                    //Instantiate the creaturePrefab with the transfrom of the first available zone.
                    //This creates an GameOjbect with the name of the creature and places it
                    //in the available zone for the player(in the hierarchy tree)
                    GameObject instantiatedCreature = Instantiate(creaturePrefab, zones[zone].transform.position, zones[zone].transform.rotation, zones[zone].transform) as GameObject;
                    instantiatedCreature.name = spawn.getCardName();

                    //To access the Creature script on the instantiated Prefab
                    myCreature = instantiatedCreature.GetComponent<Creature>();

                    //Set the scripts variables for the gameobject to match the card to spawn
                    myCreature.setCardInformation(spawn);

                    //Reduce the players AvailableMana with the creatures mana cost
                    p.setAvailableMana(p.getAvailableMana() - myCreature.getCreatureMana());

                    //Place the instantiateCreature in the playersCards list in the same slot as
                    //the zone the card was spawned in
                    p.playerCards[zone] = instantiatedCreature;
                }
            }
            else
            {
                Debug.Log("No Zone available");

            }
        } else {
            Debug.Log("No mana available for current player");
        }
    }

    //Returns the first available zone for the player from left to right
    public int getZone(Player p){return p.getAvailableZone();}
}
