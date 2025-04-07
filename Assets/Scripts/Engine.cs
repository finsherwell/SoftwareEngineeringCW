using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using JetBrains.Annotations;

public class Engine : MonoBehaviour
{
    [SerializeField] public List<Player> players;
    public GameObject playerPrefab;

    [SerializeField] public int parkingFines = 0;
    [SerializeField] private int startingMoney = 1500;
    [SerializeField] private int passGoMoney = 200;
    [SerializeField] private int maxPlayers = 5;
    [SerializeField] private int currentPlayerIndex = 0;
    [SerializeField] private TextMeshProUGUI currentPlayerText;
    [SerializeField] private TextMeshProUGUI propertyBuyText;
    [SerializeField] public TextMeshProUGUI logText;
    private bool gameOver = false;
    [SerializeField] private int playerCount = 0;
    [SerializeField] public Dice dice1;
    [SerializeField] public Dice dice2;
    [SerializeField] private Button rollButton;
    [SerializeField] public Button nextTurnButton;
    [SerializeField] private Button buyHouseButton;
    [SerializeField] private Button sellHouseButton;
    [SerializeField] private Button WarningOKbutton;
    [SerializeField] private Tile startTile;
    [SerializeField] private AuctionSystem auctionSystem;
    private bool doubleRolled = false;
    private int doubleCount;
    public Image propertyBuyImage;
    public Image CurrentTile_s; 


    [SerializeField] private GameObject purchasePropertyPanel;
    [SerializeField] private GameObject buyHousePanel;

    [SerializeField] private GameObject sellHousePanel;
    [SerializeField] private GameObject WarningPanel;
    [SerializeField] private GameObject PlayerPanel;
    [SerializeField] private GameObject PlayerPanelManager;

    public Player currentPlayer;

    public void passGo()
    {
        Debug.Log($"{currentPlayer.playerName} passed Go");
        logText.text = $"{currentPlayer.playerName} passed Go" + "\n\n" + logText.text;
        currentPlayer.addMoney(passGoMoney);
        currentPlayer.hasCompletedCircuit = true;
    }

    private void Start()
    {
        initializeGame();
    }



    public void rollDice()
    {
        rollButton.gameObject.SetActive(false);

        // Trigger animation on dice1 before rolling
        dice1.GetComponent<Animation>().Play("diceGo1");
        dice1.rollAndReturn(value1 =>
        {
            int dice1Value = value1;
            Debug.Log($"Dice 1 rolled: {dice1Value}");

            // Trigger animation on dice2 before rolling
            dice2.GetComponent<Animation>().Play("diceGo2");
            dice2.rollAndReturn(value2 =>
            {
                int dice2Value = value2;
                Debug.Log($"Dice 2 rolled: {dice2Value}");

                int totalDiceValue = dice1Value + dice2Value;
                Debug.Log($"Total Dice Value: {totalDiceValue}");
                logText.text = $"Total Dice Value: {totalDiceValue}" + "\n\n" + logText.text;
                if (dice1Value == dice2Value)
                {
                    doubleRolled = true;
                    doubleCount++;
                    if (doubleCount == 3)
                    {
                        GoToJail();
                    }
                }
                else
                {
                    doubleRolled = false;
                    doubleCount = 0;
                }
                movePlayer(totalDiceValue, currentPlayer);


                Debug.Log(currentPlayer.playerName + "is on" + currentPlayer.currentTile.name);
                if (currentPlayer.currentTile.IsProperty() == true)
                {
                    purchasePropertyUI(currentPlayer, currentPlayer.currentTile);
                }
                nextTurnButton.gameObject.SetActive(true);
            });
        });
    }
    public void housePanelToggle()
    {
        if (sellHousePanel.gameObject.activeSelf)
        {
            sellHousePanelToggle(); // Close sell house panel if it's open
        }
        buyHousePanel.gameObject.SetActive(!buyHousePanel.gameObject.activeSelf);
        foreach (Property property in currentPlayer.GetProperties())
        {
            property.ShowButtonCheck(currentPlayer);
        }
        TextMeshProUGUI buttonText = buyHouseButton.GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText.text == "Done")
        {
            foreach (Property property in currentPlayer.GetProperties())
            {
                property.HideButtonCheck(currentPlayer);
            }
        }
        if (buyHousePanel.gameObject.activeSelf)
        {
            buttonText.text = "Done";
        }
        else
        {
            buttonText.text = "Buy Houses";
        }
    }
    public void sellHousePanelToggle()
    {
        if (buyHousePanel.gameObject.activeSelf)
        {
            housePanelToggle(); // Close buy house panel if it's open
        }
        sellHousePanel.gameObject.SetActive(!sellHousePanel.gameObject.activeSelf);
        foreach (Property property in currentPlayer.GetProperties())
        {
            property.ShowSellButtonCheck(currentPlayer);
        }
        TextMeshProUGUI buttonText = sellHouseButton.GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText.text == "Done")
        {
            foreach (Property property in currentPlayer.GetProperties())
            {
                property.HideSellButtonCheck(currentPlayer);
            }
        }
        if (sellHousePanel.gameObject.activeSelf)
        {
            buttonText.text = "Done";
        }
        else
        {
            buttonText.text = "Sell Houses";
        }

    }

    private void MakePlayers()
    {
        print(GameData.Players);
        foreach (MenuPlayer p in GameData.Players)
        {
            GameObject newPlayer = Instantiate(playerPrefab);
            Player newPlayerScript = newPlayer.GetComponent<Player>();
            newPlayerScript.playerName = p.name;
            newPlayerScript.colour = p.colour;
            newPlayerScript.icon = p.icon;
            newPlayerScript.setIcon();
            players.Add(newPlayerScript);
            playerCount++;
        }
        Debug.Log($"Found and added {players.Count} players");
    }


    //THIS FUNCTION IS OLD - from when there where 5 player gameObjects already in the scene
    private void FindPlayers()
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject playerObject in playerObjects)
        {
            Player player = playerObject.GetComponent<Player>();
            if (player != null && players.Count < maxPlayers)
            {
                players.Add(player);
                playerCount++;
            }
        }
        Debug.Log($"Found and added {players.Count} players");
    }

    private void addDebugPlayers()
    {
        GameObject newPlayer = Instantiate(playerPrefab);
        Player newPlayerScript = newPlayer.GetComponent<Player>();
        newPlayerScript.playerName = "boot";
        newPlayerScript.colour = MenuEnums.Colours.Green;
        newPlayerScript.icon = MenuEnums.Icon.Boot;
        newPlayerScript.setIcon();
        players.Add(newPlayerScript);
        playerCount++;

        GameObject newPlayer2 = Instantiate(playerPrefab);
        Player newPlayerScript2 = newPlayer2.GetComponent<Player>();
        newPlayerScript.playerName = "cat";
        newPlayerScript.colour = MenuEnums.Colours.Red;
        newPlayerScript.icon = MenuEnums.Icon.Cat;
        newPlayerScript.setIcon();
        players.Add(newPlayerScript2);
        playerCount++;

        GameObject newPlayer3 = Instantiate(playerPrefab);
        Player newPlayerScript3 = newPlayer3.GetComponent<Player>();
        newPlayerScript.playerName = "ship";
        newPlayerScript.colour = MenuEnums.Colours.Purple;
        newPlayerScript.icon = MenuEnums.Icon.Ship;
        newPlayerScript.setIcon();
        players.Add(newPlayerScript3);
        playerCount++;
    }

    private void initializeGame()
    {
        nextTurnButton.gameObject.SetActive(false);
        Debug.Log("Initializing game...");
        logText.text = "Initializing game..." + "\n\n" + logText.text;
        logText.text = "Game ready!" + "\n\n" + logText.text;

        MakePlayers();

        if (playerCount == 0)
        {
            addDebugPlayers();
        }

        foreach (Player player in players)
        {
            player.addMoney(startingMoney);
            Debug.Log($"{player.playerName} has {player.money} starting money");
            player.setID(players.IndexOf(player));
            player.setCurrentTile(startTile);
            player.transform.position = startTile.transform.position;

            player.hasCompletedCircuit = true;
        }
       // PlayerPanelManager playerPanelManager = FindAnyObjectByType<PlayerPanelManager>();
       // playerPanelManager.InitializePlayerPanel(players);

        if (players.Count > 0)
        {
            currentPlayer = players[0];
            updateTurnText(currentPlayer);
            currentPlayer.gameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 0);
        }
        else
        {
            Debug.LogError("No players found in the scene!");
        }
    }



    private void purchasePropertyUI(Player player, Tile tile)
    {
        Property property = tile.GetComponent<Property>();
        if (!property.IsOwned() && player.money >= property.GetPrice() )

        {
            propertyBuyText.text = $"Would you like to purchase {property.GetName()} for {property.GetPrice()}?";
            SpriteRenderer spriteRenderer = tile.GetComponent<SpriteRenderer>();
            propertyBuyImage.sprite = spriteRenderer.sprite;
            purchasePropertyPanel.gameObject.SetActive(true);
            Debug.Log(currentPlayer.playerName + " is viewing property:" + currentPlayer.currentTile.name);
        }

    }
    public void OnpurchaseButtonClick()
    {
        Property property = currentPlayer.currentTile.GetComponent<Property>();
        purchaseProperty(currentPlayer, property);
    }
    public void OnPassButtonClick()
    {
        Property property = currentPlayer.currentTile.GetComponent<Property>();
        purchasePropertyPanel.gameObject.SetActive(false);
        
        if (property != null && !property.IsOwned())
        {
            bool hasEligiblePlayers = false;
            foreach (Player player in players)
            {
                if (player.hasCompletedCircuit)
                {
                    hasEligiblePlayers = true;
                    break;
                }
            }
            
            if (hasEligiblePlayers)
            {
                nextTurnButton.gameObject.SetActive(false);
                auctionSystem.StartAuction(property);
            }

            else
            {
                Debug.Log("No eligible players for auction");
                logText.text = "No eligible players for auction\n" + logText.text;
                nextTurnButton.gameObject.SetActive(true);
            }
        }
        else
        {
            nextTurnButton.gameObject.SetActive(true);
        }
    }
    private void purchaseProperty(Player player, Property property)
    {
        player.takeMoney(property.GetPrice());
        property.SetOwner(player);
        Debug.Log($"{player.playerName} purchased property: {property.GetName()}");
        logText.text = $"{player.playerName} purchased property: {property.GetName()}" + "\n\n" + logText.text;
        purchasePropertyPanel.gameObject.SetActive(false);
    }


    private void movePlayer(int diceValue, Player player)
    {
        Debug.Log($"{player.playerName} is moving.");

        for (int i = 0; i < diceValue; i++)
        {
            if (player.getCurrentTile() != null && player.getCurrentTile().GetNext() != null)
            {
                Tile nextTile = player.getCurrentTile().GetNext();
                player.setCurrentTile(nextTile);
                player.transform.position = nextTile.transform.position;
                //checkForPassGo(currentPlayer);
                Debug.Log($"{player.playerName} landed on tile: {nextTile.GetName()}");
            }
            else
            {
                Debug.Log($"{player.playerName} has reached the end of the board!");
                break;
            }
        }
        logText.text = $"{player.playerName} landed on tile: {player.getCurrentTile().GetName()}" + "\n\n" + logText.text;
        CheckForActionEvent(player);
        CheckForRent(player, diceValue);
        nextTurnButton.gameObject.SetActive(true);
    }
    public void CheckForRent(Player player, int diceValue)
    {
        if (player.getCurrentTile().GetComponent<Property>()!= null)
        {
            Property property = player.getCurrentTile().GetComponent<Property>();
            if (property.IsOwned() && property.GetOwner()!= player)
            {
                if (property.IsStation() == false && property.IsUtility() == false)
                {
                    int rent = property.GetRentLevels();
                    if (player.HasCompleteSet(property.GetGroup())&& property.GetHouses() == 0)
                    {rent *= 2;}
                    player.takeMoney(rent);
                    property.GetOwner().addMoney(rent);
                    Debug.Log($"{player.playerName} paid rent to {property.GetOwner().getName()} for {rent}");
                    logText.text = $"{player.playerName} has paid {rent} to {property.GetOwner().getName()}" + "\n\n" + logText.text;
                } else if (property.IsStation() == false && property.IsUtility() == true)
                {
                    int multiplier = property.GetRent(property.GetOwner().CountUtilities());
                    int rent = multiplier * diceValue;
                    player.takeMoney(rent);
                    property.GetOwner().addMoney(rent);
                    Debug.Log($"{player.playerName} paid rent to {property.GetOwner().getName()} for {rent}");
                    logText.text = $"{player.playerName} has paid {rent} to {property.GetOwner().getName()}" + "\n\n" + logText.text;
                }
                else if (property.IsStation() == true && property.IsUtility() == false)
                {
                    int rent = property.GetRent(property.GetOwner().CountStations());
                    player.takeMoney(rent);
                    property.GetOwner().addMoney(rent);
                    Debug.Log($"{player.playerName} paid rent to {property.GetOwner().getName()} for {rent}");
                    logText.text = $"{player.playerName} has paid {rent} to {property.GetOwner().getName()}" + "\n\n" + logText.text;

                }
                if (player.money < 0)
                {
                    logText.text = $"{player.playerName} is in debt!" + "\n\n" + logText.text;
                }
            }
        }
    }
    public void updateTile_s(Property property)
    {
        SpriteRenderer spriteRenderer = property.GetComponent<SpriteRenderer>();
        CurrentTile_s.sprite=spriteRenderer.sprite;
    }

    private void OnTileLanded()
    {
        Debug.Log($"{currentPlayer.playerName} has landed on a tile.");
    }

    public void nextTurn()
    {
        if (currentPlayer.money < 0)
        {
            WarningPanel.gameObject.SetActive(true);
            return;
        }
        if (!doubleRolled)
        {
            currentPlayer.gameObject.GetComponent<Renderer>().material.color = new Color(255, 255, 255);
            currentPlayerIndex++;
            if (currentPlayerIndex == playerCount)
            {
                currentPlayerIndex = 0;
            }
        }
        currentPlayer = players[currentPlayerIndex];
        nextTurnButton.gameObject.SetActive(false);
        rollButton.gameObject.SetActive(true);
        updateTurnText(currentPlayer);
        // Assuming currentPlayer is a reference to your Player class
        currentPlayer.gameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 0);
        // RGB values for black

    }
    public void OK()
    {
        WarningPanel.gameObject.SetActive(false);
    }

    private void updateTurnText(Player player)
    {
        string name = player.getName();
        currentPlayerText.text = $"Current Player: {name}";
    }

    public void CheckForActionEvent(Player player)
    {
        Tile currentTile = player.getCurrentTile();
        Debug.Log("Checking action space for tile " + currentTile.name);
        if (currentTile != null)
        {
            ActionSpace actionSpace = currentTile.GetComponent<ActionSpace>();
            if (actionSpace != null)
            {
                actionSpace.LandedOn(player);
                Debug.Log("action space " + actionSpace.name);
            }
        }

    }

    private void checkForPassGo(Player player)
    {
        Tile currentTile = player.getCurrentTile();

        if (currentTile != null)
        {
            ActionSpace actionSpace = currentTile.GetComponent<ActionSpace>();

            if (actionSpace != null && actionSpace.GetActionType() == ActionSpace.ActionType.Go)
            {
                actionSpace.LandedOn(player);
            }
        }
    }

    public void GoToJail()
    {
        if (currentPlayer != null)
        {
            // Find the Jail tile by tag
            GameObject jailTile = GameObject.FindGameObjectWithTag("Jail");

            if (jailTile != null)
            {
                // Move player to Jail tile
                currentPlayer.setCurrentTile(jailTile.GetComponent<Tile>());
                currentPlayer.transform.position = jailTile.transform.position;

                // Set player status to jailed
                currentPlayer.setInJail(true);

                Debug.Log($"{currentPlayer.playerName} has been sent to Jail!");
                logText.text = $"{currentPlayer.playerName} has been sent to Jail!" + "\n\n" + logText.text;
            }
        }
    }

}
