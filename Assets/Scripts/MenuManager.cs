using Unity.VisualScripting;
using UnityEngine;
using MenuEnums;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEditor.TerrainTools;
using System.Collections.Generic;
using System;
using Codice.Client.Common;
using UnityEngine.SceneManagement;



public class MenuManager : MonoBehaviour
{

    public CardLobby card1;
    public CardLobby card2;
    public CardLobby card3;
    public CardLobby card4;
    public CardLobby card5;
    private CardLobby[] cardArray;
    public GameObject iconSelect;
    public GameObject colourSelect;


    public GameObject mainScreen;
    public GameObject lobbyScreen;
    public GameObject newPlayerScreen;
    public GameObject changeTimeScreen;

    private List<MenuPlayer> menuPlayers = new List<MenuPlayer>();
    private List<Icon> availibleIcons;
    private List<Colours> availibileColours;


    public TMP_InputField playerNameInput;
    public TMP_InputField gameTimeInput;

    public TextMeshProUGUI gameModeText;

    public MenuPlayer tempPlayer;

    public Button[] iconButtons;
    public Button[] colourButtons;

    private Button tempSelectediconButton;
    private Button tempSelectedcolourButton;

    private int gamemode = 1;
    private String gameTimeTextTemp;
    private int gameTimeMins;

    void Start()
    {
        gamemode = 1;
        mainScreen.SetActive(true);
        lobbyScreen.SetActive(false);
        newPlayerScreen.SetActive(false);
        emptyPlayerList();

        cardArray = new CardLobby[] { card1, card2, card3, card4, card5 };
        drawCards();
        iconSelect.SetActive(false);
        colourSelect.SetActive(false);
        changeTimeScreen.SetActive(false);
        gameTimeMins = 30;
    }

    public void swtichToLobby()
    {
        mainScreen.SetActive(false);
        lobbyScreen.SetActive(true);
        newPlayerScreen.SetActive(false);
        emptyPlayerList();
        availibleIcons = new List<Icon>() { Icon.Boot, Icon.Cat, Icon.HatStand, Icon.Iron, Icon.Smartphone, Icon.Ship };
        availibileColours = new List<Colours> { Colours.Green, Colours.Blue, Colours.Red, Colours.Yellow, Colours.Purple, Colours.Cyan };

    }
    public void switchToMenu()
    {
        mainScreen.SetActive(true);
        lobbyScreen.SetActive(false);
        newPlayerScreen.SetActive(false);
        menuPlayers.Clear();
        drawCards();
        //make all the of the buttons in the new player window enabled
        foreach (Button b in iconButtons)
        {
            b.gameObject.SetActive(true);
        }
        foreach (Button b in colourButtons)
        {
            b.gameObject.SetActive(true);
        }
    }

    public void showNewPlayerScreen()
    {
        newPlayerScreen.SetActive(true);
        tempPlayer = new MenuPlayer("", Colours.Grey, Icon.Empty, false);
        playerNameInput.text = "";
        iconSelect.SetActive(false);
        colourSelect.SetActive(false);
    }

    public void showChangeTimeScreen()
    {
        changeTimeScreen.SetActive(true);
    }

    public void hideChangeTimeScreen()
    {


        if (int.TryParse(gameTimeTextTemp, out int parsedVal))
        {
            if (parsedVal > 0)
            {
                gameTimeMins = parsedVal;
                if (gamemode == 2)
                {
                    gameModeText.text = "Game mode: abridged \n Time limit: " + gameTimeMins.ToString() + " mins";
                }
            }

        }


        gameTimeInput.text = "";
        changeTimeScreen.SetActive(false);
    }

    public void newPlayerSelectIcon(string icon)
    {
        switch (icon)
        {
            case "boot":
                iconSelect.transform.SetParent(iconButtons[0].transform, false);
                iconSelect.transform.position = iconButtons[0].transform.position;
                tempPlayer.icon = Icon.Boot;
                tempSelectediconButton = iconButtons[0];
                availibleIcons.Remove(Icon.Boot);
                break;
            case "ship":
                iconSelect.transform.SetParent(iconButtons[1].transform, false);
                iconSelect.transform.position = iconButtons[1].transform.position;
                tempPlayer.icon = Icon.Ship;
                tempSelectediconButton = iconButtons[1];
                availibleIcons.Remove(Icon.Ship);
                break;
            case "cat":
                iconSelect.transform.SetParent(iconButtons[2].transform, false);
                iconSelect.transform.position = iconButtons[2].transform.position;
                tempPlayer.icon = Icon.Cat;
                tempSelectediconButton = iconButtons[2];
                availibleIcons.Remove(Icon.Cat);
                break;
            case "hat stand":
                iconSelect.transform.SetParent(iconButtons[3].transform, false);
                iconSelect.transform.position = iconButtons[3].transform.position;
                tempPlayer.icon = Icon.HatStand;
                tempSelectediconButton = iconButtons[3];
                availibleIcons.Remove(Icon.HatStand);
                break;
            case "smartphone":
                iconSelect.transform.SetParent(iconButtons[4].transform, false);
                iconSelect.transform.position = iconButtons[4].transform.position;
                tempPlayer.icon = Icon.Smartphone;
                tempSelectediconButton = iconButtons[4];
                availibleIcons.Remove(Icon.Smartphone);
                break;
            case "iron":
                iconSelect.transform.SetParent(iconButtons[5].transform, false);
                iconSelect.transform.position = iconButtons[5].transform.position;
                tempPlayer.icon = Icon.Iron;
                tempSelectediconButton = iconButtons[5];
                availibleIcons.Remove(Icon.Iron);
                break;



        }
        iconSelect.SetActive(true);
    }

    public void newPlayerSelectColour(String c)
    {
        switch (c)
        {
            case "red":
                colourSelect.transform.SetParent(colourButtons[0].transform, false);
                colourSelect.transform.position = colourButtons[0].transform.position;
                tempPlayer.colour = Colours.Red;
                tempSelectedcolourButton = colourButtons[0];
                availibileColours.Remove(Colours.Red);
                break;
            case "blue":
                colourSelect.transform.SetParent(colourButtons[1].transform, false);
                colourSelect.transform.position = colourButtons[1].transform.position;
                tempPlayer.colour = Colours.Blue;
                tempSelectedcolourButton = colourButtons[1];
                availibileColours.Remove(Colours.Blue);
                break;
            case "green":
                colourSelect.transform.SetParent(colourButtons[2].transform, false);
                colourSelect.transform.position = colourButtons[2].transform.position;
                tempPlayer.colour = Colours.Green;
                tempSelectedcolourButton = colourButtons[2];
                availibileColours.Remove(Colours.Green);
                break;
            case "purple":
                colourSelect.transform.SetParent(colourButtons[3].transform, false);
                colourSelect.transform.position = colourButtons[3].transform.position;
                tempPlayer.colour = Colours.Purple;
                tempSelectedcolourButton = colourButtons[3];
                availibileColours.Remove(Colours.Purple);
                break;
            case "yellow":
                colourSelect.transform.SetParent(colourButtons[4].transform, false);
                colourSelect.transform.position = colourButtons[4].transform.position;
                tempPlayer.colour = Colours.Yellow;
                tempSelectedcolourButton = colourButtons[4];
                availibileColours.Remove(Colours.Yellow);
                break;
            case "cyan":
                colourSelect.transform.SetParent(colourButtons[5].transform, false);
                colourSelect.transform.position = colourButtons[5].transform.position;
                tempPlayer.colour = Colours.Cyan;
                tempSelectedcolourButton = colourButtons[5];
                availibileColours.Remove(Colours.Cyan);
                break;
        }
        colourSelect.SetActive(true);

    }


    public void hideNewPlayerScreen()
    {
        newPlayerScreen.SetActive(false);
        drawCards();
    }

    public void updateTempPlayerText(string n)
    {
        tempPlayer.name = n;
    }

    public void updateTempTimeText(string t)
    {
        gameTimeTextTemp = t;
    }



    // Update is called once per frame
    void Update()
    {

    }

    private void emptyPlayerList()
    {
        menuPlayers.Clear();
    }

    private void drawCards()
    {
        //make all the cards plain
        for (int i = 0; i < cardArray.Length; i++)
        {
            cardArray[i].playerName = "";
            cardArray[i].icon = Icon.Empty;
            cardArray[i].cardColour = Colours.Grey;
            cardArray[i].refreshCard();
            Debug.Log("card empty");
        }
        Debug.Log("menu player count");
        Debug.Log(menuPlayers.Count);

        //then fill in the cards for which there is information
        for (int i = 0; i < menuPlayers.Count; i++)
        {
            Debug.Log("creating new card with stuff");
            Debug.Log(menuPlayers[i].name);
            cardArray[i].playerName = menuPlayers[i].name;
            cardArray[i].icon = menuPlayers[i].icon;
            cardArray[i].cardColour = menuPlayers[i].colour;
            cardArray[i].refreshCard();
        }
    }

    public void addAI()
    {
        //dont add the ai if there are already 5 players
        if (menuPlayers.Count >= 5)
        {
            Debug.Log("too many players");
            return;
        }

        tempPlayer = new MenuPlayer("AI", Colours.Grey, Icon.Robot, true);
        menuPlayers.Add(tempPlayer);
        drawCards();

    }
    public void addPlayer()
    {
        //dont add the player if there is already 5!!!
        if (menuPlayers.Count >= 5)
        {
            Debug.Log("too many players");
            return;
        }
        //dont add player if there is not a colour and icon selected and player has a name

        if (tempPlayer.name != "" & tempPlayer.icon != Icon.Empty & tempPlayer.colour != Colours.Grey & !tempPlayer.name.StartsWith("AI"))
        {
            menuPlayers.Add(tempPlayer);
            drawCards();
            hideNewPlayerScreen();
            Debug.Log("appended new player");
            Debug.Log(tempPlayer.name);
            if (tempSelectediconButton)
            {
                tempSelectediconButton.gameObject.SetActive(false);
                tempSelectediconButton = null;
            }
            if (tempSelectedcolourButton)
            {
                tempSelectedcolourButton.gameObject.SetActive(false);
                tempSelectedcolourButton = null;
            }
        }
    }

    public void removeLastPlayer()
    {
        if (menuPlayers.Count > 0)
        {
            MenuPlayer temp = menuPlayers[menuPlayers.Count - 1];
            menuPlayers.RemoveAt(menuPlayers.Count - 1);
            availibileColours.Add(temp.colour);
            availibleIcons.Add(temp.icon);
            switch (temp.colour)
            {
                case Colours.Red:
                    colourButtons[0].gameObject.SetActive(true);
                    break;
                case Colours.Blue:
                    colourButtons[1].gameObject.SetActive(true);
                    break;
                case Colours.Green:
                    colourButtons[2].gameObject.SetActive(true);
                    break;
                case Colours.Purple:
                    colourButtons[3].gameObject.SetActive(true);
                    break;
                case Colours.Yellow:
                    colourButtons[4].gameObject.SetActive(true);
                    break;
                case Colours.Cyan:
                    colourButtons[5].gameObject.SetActive(true);
                    break;

            }

            switch (temp.icon)
            {
                case Icon.Boot:
                    iconButtons[0].gameObject.SetActive(true);
                    break;
                case Icon.Ship:
                    iconButtons[1].gameObject.SetActive(true);
                    break;
                case Icon.Cat:
                    iconButtons[2].gameObject.SetActive(true);
                    break;
                case Icon.HatStand:
                    iconButtons[3].gameObject.SetActive(true);
                    break;
                case Icon.Smartphone:
                    iconButtons[4].gameObject.SetActive(true);
                    break;
                case Icon.Iron:
                    iconButtons[5].gameObject.SetActive(true);
                    break;

            }

            drawCards();

        }
    }

    public void startGame()
    {
        if (menuPlayers.Count > 1)
        {
            //give each AI player a unique name, colour, icon
            int counter = 1;
            foreach (MenuPlayer p in menuPlayers)
            {
                if (p.isAI)
                {
                    p.name = "AI" + counter.ToString();
                    p.icon = availibleIcons[0];
                    availibleIcons.RemoveAt(0);
                    p.colour = availibileColours[0];
                    availibileColours.RemoveAt(0);
                    counter += 1;
                }
            }
            drawCards();
            GameData.Players = menuPlayers;
            GameData.gameMode = gamemode;
            GameData.gameTime = gameTimeMins;
            SceneManager.LoadScene("GameScene");
        }
    }

    public void switchGameMode()
    {

        if (gamemode == 1)
        {
            gamemode = 2;
            gameModeText.text = "Game mode: abridged \n Time limit: " + gameTimeMins.ToString() + " mins";
        }
        else
        {
            gamemode = 1;
            gameModeText.text = "Game mode: original";
        }
        Debug.Log("game mode is " + gamemode);
    }
}
