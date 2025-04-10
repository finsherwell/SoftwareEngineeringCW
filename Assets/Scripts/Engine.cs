using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;

public class Engine : MonoBehaviour
{
    [SerializeField] public List<Player> players;
    public GameObject playerPrefab;
    [SerializeField] public CardManager cardManager;
    [SerializeField] public int parkingFines = 0;
    [SerializeField] private int startingMoney = 1500;
    [SerializeField] private int passGoMoney = 200;
    [SerializeField] private int maxPlayers = 5;
    [SerializeField] private int currentPlayerIndex = 0;
    [SerializeField] private TextMeshProUGUI currentPlayerText;
    [SerializeField] private TextMeshProUGUI propertyBuyText;
    [SerializeField] public TextMeshProUGUI logText;
    [SerializeField] public TextMeshProUGUI JailDescriptionText;
    [SerializeField] public TextMeshProUGUI viewText;
    private bool gameOver = false;
    [SerializeField] private int playerCount = 0;
    [SerializeField] public Dice dice1;
    [SerializeField] public Dice dice2;
    [SerializeField] private Button rollButton;
    [SerializeField] public Button nextTurnButton;
    [SerializeField] private Button buyHouseButton;
    [SerializeField] private Button sellHouseButton;
    [SerializeField] private Button WarningOKbutton;
    [SerializeField] private Button viewOwnedButton;
    [SerializeField] private Button doneViewButton;
    [SerializeField] private Button mortgagebutton;
    [SerializeField] private Button unmortgageButton;
    [SerializeField] private Button viewAllButton;
    [SerializeField] private Button doneViewAllButton;
    [SerializeField] private Tile startTile;
    [SerializeField] private AuctionSystem auctionSystem;
    private bool doubleRolled = false;
    private int doubleCount;
    public Image propertyBuyImage;
    public Image CurrentTile_s;
    public int gameMode = 1; //1 = no timer, 2 = timer.
    public int gameTime;
    public bool abridgedTimerEnded;
    public Player abridgedGameEndedOn = null; //if game is in abridged mode, this will store the index of the player whose turn it was when timer went off. This can then be used to see when game should end.



    [SerializeField] private GameObject purchasePropertyPanel;
    [SerializeField] private GameObject buyHousePanel;

    [SerializeField] private GameObject sellHousePanel;
    [SerializeField] private GameObject WarningPanel;
    [SerializeField] private GameObject PlayerPanel;
    [SerializeField] private GameObject JailPanel;
    [SerializeField] private Property selectedProperty;
    [SerializeField] private GameObject BankruptPanel;
    [SerializeField] private PlayerPanelManager PPM;

    [SerializeField] private float playerSpacing = 2f;
    [SerializeField] private float positionOffsetY = 1f;

    [SerializeField] private GameObject auctionPanel;
    [SerializeField] private GameObject gameEndedPanel;

    [SerializeField] private TextMeshProUGUI winningPlayerText;

    public Dictionary<Tile, List<Player>> playersOnTiles = new Dictionary<Tile, List<Player>>();

    public Player currentPlayer;

    private float abdrigedGameTimeLeft = 0;
    [SerializeField] private TextMeshProUGUI abridgedTimeLeftText;

    private enum TileOrientation
    {
        Bottom,
        Left,
        Top,
        Right,
        Corner
    }

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

    private void InitPlayersOnTiles()
    {
        playersOnTiles.Clear();

        foreach (Player player in players)
        {
            Tile startTile = player.getCurrentTile();
            if (!playersOnTiles.ContainsKey(startTile))
            {
                playersOnTiles.Add(startTile, new List<Player>());
            }
            playersOnTiles[startTile].Add(player);
        }

        foreach (Tile tile in playersOnTiles.Keys)
        {
            PosPlayerOnTile(tile);
        }
    }

    public void PosPlayerOnTile(Tile tile)
    {
        if (!playersOnTiles.ContainsKey(tile) || playersOnTiles[tile].Count == 0)
            return;

        List<Player> playersOnTile = playersOnTiles[tile];
        int playerCount = playersOnTile.Count;

        // Center position of the tile
        Vector3 tileCenter = tile.transform.position;

        // Determine tile orientation based on tile ID or name
        TileOrientation orientation = GetTileOrientationFromId(tile);
        bool isCorner = tile.name.Contains("corner");

        // Adjust spacing for corner tiles
        float currentSpacing = isCorner ? playerSpacing * 1.1f : playerSpacing;
        float currentOffsetY = isCorner ? positionOffsetY * 1.1f : positionOffsetY;

        if (playerCount == 1)
        {
            Vector3 orientationOffset = GetOrientationOffset(orientation);
            Vector3 adjustedCenter = tileCenter + orientationOffset;
            playersOnTile[0].transform.position = adjustedCenter;
        }
        else
        {
            // Calculate positions based on orientation
            Vector3[] positions = CalculatePositions(playerCount, tileCenter, currentSpacing, currentOffsetY, orientation);

            for (int i = 0; i < playerCount; i++)
            {
                playersOnTile[i].transform.position = positions[i];
            }
        }
    }

    private TileOrientation GetTileOrientationFromId(Tile tile)
    {
        int tileId = tile.ID;

        if (tile.name.Contains("corner"))
            return TileOrientation.Corner;

        if (tileId >= 1 && tileId <= 9)
            return TileOrientation.Bottom;

        if (tileId >= 11 && tileId <= 19)
            return TileOrientation.Left;

        if (tileId >= 21 && tileId <= 29)
            return TileOrientation.Top;

        if (tileId >= 31 && tileId <= 39)
            return TileOrientation.Right;

        // Default case
        return TileOrientation.Bottom;
    }

    private Vector3[] CalculatePositions(int playerCount, Vector3 tileCenter, float spacing, float offsetY, TileOrientation orientation)
    {
        Vector3[] positions = new Vector3[playerCount];

        // Add offset to move icons away from the text based on orientation
        Vector3 orientationOffset = GetOrientationOffset(orientation);
        Vector3 adjustedCenter = tileCenter + orientationOffset;

        switch (playerCount)
        {
            case 1:
                positions[0] = adjustedCenter;
                break;
            case 2:
                positions[0] = adjustedCenter + new Vector3(-spacing / 2, 0, 0);
                positions[1] = adjustedCenter + new Vector3(spacing / 2, 0, 0);
                break;
            case 3:
                positions[0] = adjustedCenter + new Vector3(0, offsetY, 0);
                positions[1] = adjustedCenter + new Vector3(-spacing / 2, -offsetY, 0);
                positions[2] = adjustedCenter + new Vector3(spacing / 2, -offsetY, 0);
                break;
            case 4:
                positions[0] = adjustedCenter + new Vector3(-spacing / 2, offsetY, 0);
                positions[1] = adjustedCenter + new Vector3(spacing / 2, offsetY, 0);
                positions[2] = adjustedCenter + new Vector3(-spacing / 2, -offsetY, 0);
                positions[3] = adjustedCenter + new Vector3(spacing / 2, -offsetY, 0);
                break;
            case 5:
                positions[0] = adjustedCenter;
                positions[1] = adjustedCenter + new Vector3(-spacing / 2, offsetY, 0);
                positions[2] = adjustedCenter + new Vector3(spacing / 2, offsetY, 0);
                positions[3] = adjustedCenter + new Vector3(-spacing / 2, -offsetY, 0);
                positions[4] = adjustedCenter + new Vector3(spacing / 2, -offsetY, 0);
                break;
            default:
                for (int i = 0; i < playerCount; i++)
                {
                    positions[i] = adjustedCenter;
                }
                break;
        }

        // Adjust positions based on orientation
        for (int i = 0; i < positions.Length; i++)
        {
            Vector3 relativePos = positions[i] - adjustedCenter;

            switch (orientation)
            {
                case TileOrientation.Left:
                    // Rotate 90 degrees counter-clockwise
                    positions[i] = adjustedCenter + new Vector3(relativePos.y, -relativePos.x, relativePos.z);
                    break;
                case TileOrientation.Top:
                    // Rotate 180 degrees
                    positions[i] = adjustedCenter + new Vector3(-relativePos.x, -relativePos.y, relativePos.z);
                    break;
                case TileOrientation.Right:
                    // Rotate 270 degrees counter-clockwise
                    positions[i] = adjustedCenter + new Vector3(-relativePos.y, relativePos.x, relativePos.z);
                    break;
                case TileOrientation.Corner:
                    break;
            }
        }

        return positions;
    }

    private Vector3 GetOrientationOffset(TileOrientation orientation)
    {
        float offsetAmount = 1f;

        switch (orientation)
        {
            case TileOrientation.Bottom:
                // For bottom tiles, move icons lower (away from text at top)
                return new Vector3(0, -offsetAmount, 0);

            case TileOrientation.Left:
                // For left tiles, move icons more left (away from text at right)
                return new Vector3(-offsetAmount - 0.5f, 0, 0);

            case TileOrientation.Top:
                // For top tiles, move icons higher (away from text at bottom)
                return new Vector3(0, offsetAmount, 0);

            case TileOrientation.Right:
                // For right tiles, move icons more right (away from text at left)
                return new Vector3(offsetAmount + 0.5f, 0, 0);

            case TileOrientation.Corner:
                // For corner tiles, move icons more down
                return new Vector3(0, -offsetAmount * 1.7f, 0);

            default:
                return Vector3.zero;
        }
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
                Property property = currentPlayer.currentTile.GetComponent<Property>();
                if (currentPlayer.currentTile.IsProperty() == true && !property.IsOwned())
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
            newPlayerScript.setAIMode(p.isAI);
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
        newPlayerScript2.playerName = "cat";
        newPlayerScript2.colour = MenuEnums.Colours.Red;
        newPlayerScript2.icon = MenuEnums.Icon.Cat;
        newPlayerScript2.setIcon();
        players.Add(newPlayerScript2);
        playerCount++;

        GameObject newPlayer3 = Instantiate(playerPrefab);
        Player newPlayerScript3 = newPlayer3.GetComponent<Player>();
        newPlayerScript3.playerName = "ship";
        newPlayerScript3.colour = MenuEnums.Colours.Purple;
        newPlayerScript3.icon = MenuEnums.Icon.Ship;
        newPlayerScript3.setIcon();
        players.Add(newPlayerScript3);
        playerCount++;
        Debug.Log("player count is " + playerCount);
    }

    private void initializeGame()
    {

        nextTurnButton.gameObject.SetActive(false);
        gameEndedPanel.SetActive(false);


        Debug.Log("Initializing game...");
        logText.text = "Initializing game..." + "\n\n" + logText.text;
        logText.text = "Game ready!" + "\n\n" + logText.text;


        auctionPanel.SetActive(false);

        MakePlayers();

        //if the game scene is ran directly (for debugging) add some players and set the game mode. Otherwise, get the game mode info from game data (players added already)
        if (playerCount == 0)
        {
            addDebugPlayers();
            gameMode = 1;
            gameTime = 123456789; //dont think this really needs to be here
            abridgedTimeLeftText.enabled = false;
        }
        else
        {
            //get the game mode from game data
            abridgedTimerEnded = false;
            gameMode = GameData.gameMode;
            Debug.Log("game mode is " + gameMode);
            if (gameMode == 2)
            {
                gameTime = GameData.gameTime;
                Debug.Log("game time is " + gameTime);
                Invoke("gameTimeGoneOff", gameTime * 60);   //multiplied by 60 so it is in mins and not secs
                abdrigedGameTimeLeft = gameTime * 60;
            }
            //if the game mode is abridged, show the text that displays how much time is left
            if (gameMode == 1)
            {
                abridgedTimeLeftText.enabled = false;
            }
            else
            {
                abridgedTimeLeftText.enabled = true;
            }
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

        InitPlayersOnTiles();

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
        PPM.InitializePlayerPanel(players);
        Debug.Log("PPM called with players: " + players.Count);
    }

    void Update()
    {
        if (abdrigedGameTimeLeft > 0 && gameMode == 2)
        {
            abdrigedGameTimeLeft -= Time.deltaTime;
            int minsLeft = Mathf.FloorToInt(abdrigedGameTimeLeft / 60);
            abridgedTimeLeftText.text = "minutes left: " + minsLeft.ToString();
            Debug.Log("set game time to " + minsLeft.ToString());
        }
        if (selectedProperty != null)
        {
            bool showMortgageButton = selectedProperty.GetOwner() == currentPlayer &&
            selectedProperty.GetHouseCount() == 0 &&
            !selectedProperty.GetMortgaged();

            bool showUnmortgageButton = selectedProperty.GetOwner() == currentPlayer &&
            selectedProperty.GetMortgaged() &&
            currentPlayer.money >= selectedProperty.GetPrice();

            mortgagebutton.gameObject.SetActive(showMortgageButton);
            unmortgageButton.gameObject.SetActive(showUnmortgageButton);
        }
        else
        {
            // If no property is selected, hide both buttons
            mortgagebutton.gameObject.SetActive(false);
            unmortgageButton.gameObject.SetActive(false);
        }
    }
    public void onMortgageButtonClick()
    {
        Property property = selectedProperty;
        if (property.GetMortgaged() == false)
        {
            property.SetMortgaged(true);
            currentPlayer.money += property.GetPrice() / 2;
            print($"{currentPlayer.playerName} mortgaged {property.GetName()} for {property.GetPrice() / 2}");
            logText.text = $"{currentPlayer.playerName} mortgaged {property.GetName()} for {property.GetPrice() / 2}" + "\n\n" + logText.text;
        }
    }
    public void onUnmortgageButtonClick()
    {
        Property property = selectedProperty;
        if (property.GetMortgaged() == true)
        {
            property.SetMortgaged(false);
            currentPlayer.money -= property.GetPrice();
            print($"{currentPlayer.playerName} unmortgaged {property.GetName()} for {property.GetPrice() / 2}");
            logText.text = $"{currentPlayer.playerName} unmortgaged {property.GetName()} for {property.GetPrice() / 2}" + "\n\n" + logText.text;
        }
    }

    public void viewAllProperties()
    {
        viewAllButton.gameObject.SetActive(false);
        doneViewAllButton.gameObject.SetActive(true);

        foreach (Player player in players)
        {
            Color playerColor = PPM.GetColorFromEnum(player.colour);

            foreach (Property property in player.ownedproperties)
            {
                SpriteRenderer propertyRenderer = property.GetComponent<SpriteRenderer>();
                if (propertyRenderer != null)
                {
                    propertyRenderer.color = playerColor;
                }
            }
        }
    }


    public void finishedViewAllProperties()
    {
        viewAllButton.gameObject.SetActive(true);
        doneViewAllButton.gameObject.SetActive(false);

        foreach (Player player in players)
        {
            foreach (Property property in player.ownedproperties)
            {
                SpriteRenderer propertyRenderer = property.GetComponent<SpriteRenderer>();
                if (propertyRenderer != null)
                {
                    propertyRenderer.color = Color.white;
                }
            }
        }
    }

    private void purchasePropertyUI(Player player, Tile tile)
    {
        Property property = tile.GetComponent<Property>();
        if (!property.IsOwned() && player.money >= property.GetPrice() && player.hasCompletedCircuit == true)

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
                logText.text = "No eligible players for auction\n\n" + logText.text;
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

        // Remove player from current tile's player list
        Tile currentTile = player.getCurrentTile();
        if (playersOnTiles.ContainsKey(currentTile))
        {
            playersOnTiles[currentTile].Remove(player);
            // Reposition remaining players on this tile
            PosPlayerOnTile(currentTile);
        }

        for (int i = 0; i < diceValue; i++)
        {
            if (player.getCurrentTile() != null && player.getCurrentTile().GetNext() != null)
            {
                Tile nextTile = player.getCurrentTile().GetNext();
                player.setCurrentTile(nextTile);

                checkForPassGo(currentPlayer);
                Debug.Log($"{player.playerName} moved to tile: {nextTile.GetName()}");
            }
            else
            {
                Debug.Log($"{player.playerName} has reached the end of the board!");
                break;
            }
        }

        // Add player to the new tile's player list
        Tile newTile = player.getCurrentTile();
        if (!playersOnTiles.ContainsKey(newTile))
        {
            playersOnTiles.Add(newTile, new List<Player>());
        }
        playersOnTiles[newTile].Add(player);

        // Position all players on this new tile
        PosPlayerOnTile(newTile);

        logText.text = $"{player.playerName} landed on tile: {player.getCurrentTile().GetName()}" + "\n\n" + logText.text;
        CheckForActionEvent(player);
        CheckForRent(player, diceValue);
        nextTurnButton.gameObject.SetActive(true);
    }

    public void CheckForRent(Player player, int diceValue)
    {
        if (player.getCurrentTile().GetComponent<Property>() != null)
        {
            Property property = player.getCurrentTile().GetComponent<Property>();
            if (property.IsOwned() && property.GetOwner() != player && property.GetMortgaged() == false)
            {
                if (property.IsStation() == false && property.IsUtility() == false)
                {
                    int rent = property.GetRentLevels();
                    if (player.HasCompleteSet(property.GetGroup()) && property.GetHouses() == 0)
                    { rent *= 2; }
                    player.takeMoney(rent);
                    property.GetOwner().addMoney(rent);
                    Debug.Log($"{player.playerName} paid rent to {property.GetOwner().getName()} for {rent}");
                    logText.text = $"{player.playerName} has paid {rent} to {property.GetOwner().getName()}" + "\n\n" + logText.text;
                }
                else if (property.IsStation() == false && property.IsUtility() == true)
                {
                    int i = property.GetOwner().CountUtilities() - 1;
                    int multiplier = property.GetRent(i);
                    int rent = multiplier * diceValue;
                    player.takeMoney(rent);
                    property.GetOwner().addMoney(rent);
                    Debug.Log($"{player.playerName} paid rent to {property.GetOwner().getName()} for {rent}");
                    logText.text = $"{player.playerName} has paid {rent} to {property.GetOwner().getName()}" + "\n\n" + logText.text;
                }
                else if (property.IsStation() == true && property.IsUtility() == false)
                {
                    int rent = property.GetRent(property.GetOwner().CountStations() - 1);
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
        Sprite rent_s = property.getSprite();
        CurrentTile_s.sprite = rent_s;
        selectedProperty = property;
    }

    private void OnTileLanded()
    {
        Debug.Log($"{currentPlayer.playerName} has landed on a tile.");
    }

    public void hideWarningPanel()
    {
        WarningPanel.SetActive(false);
    }

    public void nextTurn()
    {
        finishedView();
        if (currentPlayer.money < 0)
        {
            WarningPanel.gameObject.SetActive(true);
            return;
        }
        if (!doubleRolled || currentPlayer.GetJailTime() > 0)
        {
            currentPlayer.gameObject.GetComponent<Renderer>().material.color = new Color(255, 255, 255);
            currentPlayerIndex++;
            if (currentPlayerIndex == playerCount)
            {
                currentPlayerIndex = 0;
            }
        }
        currentPlayer = players[currentPlayerIndex];

        if (currentPlayer == null)
        {
            currentPlayerIndex++;
        }
        currentPlayer = players[currentPlayerIndex];
        //maybe
        if (abridgedTimerEnded == true && currentPlayer == abridgedGameEndedOn)
        {
            endGame(maxAssetPlayer());
        }
        nextTurnButton.gameObject.SetActive(false);
        rollButton.gameObject.SetActive(true);
        updateTurnText(currentPlayer);
        PPM.updateArrow(currentPlayerIndex);
        currentPlayer.gameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 0);

        if (currentPlayer.GetJailTime() > 0)
        {
            doubleRolled = false;
            JailPanel.gameObject.SetActive(true);
            JailDescriptionText.text = $"Turns until freedom: " + currentPlayer.GetJailTime() + " turns";
        }
    }


    public void PayBail()
    {
        currentPlayer.takeMoney(50);
        currentPlayer.setInJail(0);
        JailPanel.gameObject.SetActive(false);
        logText.text = $"{currentPlayer.playerName} has left Jail!" + "\n\n" + logText.text;
        nextTurn();
    }
    public void JailNextTurn()
    {
        currentPlayer.setInJail(currentPlayer.GetJailTime() - 1);
        JailPanel.gameObject.SetActive(false);
        nextTurn();

    }
    public void Bankrupt()
    {
        // Hide the warning panel when the player goes bankrupt
        WarningPanel.gameObject.SetActive(false);

        // Loop through owned properties in reverse order to avoid modifying the collection during iteration
        for (int i = currentPlayer.ownedproperties.Count - 1; i >= 0; i--)
        {
            var property = currentPlayer.ownedproperties[i];
            currentPlayer.removeProperty(property);
        }

        // Set the player's money to zero
        currentPlayer.money = 0;

        // If currentPlayer is a GameObject, destroy it
        Destroy(currentPlayer.gameObject);

        // Hide the bankrupt panel after processing
        RemovePlayerFromGame(currentPlayer);
        Debug.Log(currentPlayer.getName() + " is broke. players left (hopefully): " + players.Count.ToString());
        //if there is only player left, they have won!! end the game.
        if (playerCount == 1)
        {
            endGame(players[0]);
        }



        // Proceed to the next turn
        nextTurn();
        BankruptPanel.gameObject.SetActive(false);
        PPM.removePlayer(currentPlayerIndex, players.Count);


    }



    private void updateTurnText(Player player)
    {
        string name = player.getName();
        currentPlayerText.text = $"Current Player: {name}";
        viewText.text = $"View {name} properties";
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
                currentPlayer.setInJail(3);

                Debug.Log($"{currentPlayer.playerName} has been sent to Jail!");
                logText.text = $"{currentPlayer.playerName} has been sent to Jail!" + "\n\n" + logText.text;
            }
        }
    }

    public void RemovePlayerFromGame(Player player)
    {
        // Remove from current tile
        Tile currentTile = player.getCurrentTile();
        if (playersOnTiles.ContainsKey(currentTile))
        {
            playersOnTiles[currentTile].Remove(player);
            PosPlayerOnTile(currentTile);
        }

        players.Remove(player);
        playerCount--;

        currentPlayerIndex++;
        if (currentPlayerIndex >= playerCount)
        {
            currentPlayerIndex = 0;
        }
    }
    public void viewOwned()
    {
        viewOwnedButton.gameObject.SetActive(false);
        doneViewButton.gameObject.SetActive(true);
        Color color = PPM.getPlayerColour(currentPlayerIndex);
        for (int i = currentPlayer.ownedproperties.Count - 1; i >= 0; i--)
        {
            var property = currentPlayer.ownedproperties[i];

            // Check if the property has an Image component (for UI elements)
            SpriteRenderer propertyRenderer = property.GetComponent<SpriteRenderer>();
            if (propertyRenderer != null)
            {
                propertyRenderer.color = color;  // Set the color of the UI image
            }
            else
            {
                return;
            }
        }
    }
    public void finishedView()
    {
        viewOwnedButton.gameObject.SetActive(true);
        doneViewButton.gameObject.SetActive(false);
        Color color = Color.white;
        for (int i = currentPlayer.ownedproperties.Count - 1; i >= 0; i--)
        {
            var property = currentPlayer.ownedproperties[i];

            // Check if the property has an Image component (for UI elements)
            SpriteRenderer propertyRenderer = property.GetComponent<SpriteRenderer>();
            if (propertyRenderer != null)
            {
                propertyRenderer.color = color;  // Set the color of the UI image
            }
            else
            {
                return;
            }
        }
    }

    public void exitToMenu()
    {
        SceneManager.LoadScene("MainMenu2");
    }


    //returns the player who has the highest value of property + house + cash combined
    public Player maxAssetPlayer()
    {
        Player richest = players[0];
        foreach (var p in players)
        {
            if (p.totalAssetValue > richest.totalAssetValue)
            {
                richest = p;
            }
        }
        return richest;
    }

    public void gameTimeGoneOff()
    {
        abridgedTimerEnded = true;
        abridgedGameEndedOn = currentPlayer;
        Debug.Log("abdriged game timer went off, player " + currentPlayer.getName());
    }

    public void endGame(Player winner)
    {
        //Player winner = maxAssetPlayer();
        winningPlayerText.text = winner.getName();
        gameEndedPanel.SetActive(true);
        Debug.Log("winner is " + winner.getName());
    }

    public void backToMenu()
    {
        SceneManager.LoadScene("MainMenu2");
    }

}
