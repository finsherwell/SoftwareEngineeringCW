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

    private List<MenuPlayer> menuPlayers = new List<MenuPlayer>();
    public TMP_InputField playerNameInput;

    public MenuPlayer tempPlayer;

    public Button[] iconButtons;
    public Button[] colourButtons;

    private Button tempSelectediconButton;
    private Button tempSelectedcolourButton;

    void Start()
    {
        mainScreen.SetActive(true);
        lobbyScreen.SetActive(false);
        newPlayerScreen.SetActive(false);
        emptyPlayerList();

        cardArray = new CardLobby[] { card1, card2, card3, card4, card5 };
        drawCards();
        iconSelect.SetActive(false);
        colourSelect.SetActive(false);


    }

    public void swtichToLobby()
    {
        mainScreen.SetActive(false);
        lobbyScreen.SetActive(true);
        newPlayerScreen.SetActive(false);
        emptyPlayerList();
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

    public void newPlayerSelectIcon(string icon)
    {
        switch (icon)
        {
            case "boot":
                iconSelect.transform.SetParent(iconButtons[0].transform, false);
                iconSelect.transform.position = iconButtons[0].transform.position;
                tempPlayer.icon = Icon.Boot;
                tempSelectediconButton = iconButtons[0];
                break;
            case "ship":
                iconSelect.transform.SetParent(iconButtons[1].transform, false);
                iconSelect.transform.position = iconButtons[1].transform.position;
                tempPlayer.icon = Icon.Ship;
                tempSelectediconButton = iconButtons[1];
                break;
            case "cat":
                iconSelect.transform.SetParent(iconButtons[2].transform, false);
                iconSelect.transform.position = iconButtons[2].transform.position;
                tempPlayer.icon = Icon.Cat;
                tempSelectediconButton = iconButtons[2];
                break;
            case "hat stand":
                iconSelect.transform.SetParent(iconButtons[3].transform, false);
                iconSelect.transform.position = iconButtons[3].transform.position;
                tempPlayer.icon = Icon.HatStand;
                tempSelectediconButton = iconButtons[3];
                break;
            case "smartphone":
                iconSelect.transform.SetParent(iconButtons[4].transform, false);
                iconSelect.transform.position = iconButtons[4].transform.position;
                tempPlayer.icon = Icon.Smartphone;
                tempSelectediconButton = iconButtons[4];
                break;
            case "iron":
                iconSelect.transform.SetParent(iconButtons[5].transform, false);
                iconSelect.transform.position = iconButtons[5].transform.position;
                tempPlayer.icon = Icon.Iron;
                tempSelectediconButton = iconButtons[5];
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
                break;
            case "blue":
                colourSelect.transform.SetParent(colourButtons[1].transform, false);
                colourSelect.transform.position = colourButtons[1].transform.position;
                tempPlayer.colour = Colours.Blue;
                tempSelectedcolourButton = colourButtons[1];
                break;
            case "green":
                colourSelect.transform.SetParent(colourButtons[2].transform, false);
                colourSelect.transform.position = colourButtons[2].transform.position;
                tempPlayer.colour = Colours.Green;
                tempSelectedcolourButton = colourButtons[2];
                break;
            case "purple":
                colourSelect.transform.SetParent(colourButtons[3].transform, false);
                colourSelect.transform.position = colourButtons[3].transform.position;
                tempPlayer.colour = Colours.Purple;
                tempSelectedcolourButton = colourButtons[3];
                break;
            case "yellow":
                colourSelect.transform.SetParent(colourButtons[4].transform, false);
                colourSelect.transform.position = colourButtons[4].transform.position;
                tempPlayer.colour = Colours.Yellow;
                tempSelectedcolourButton = colourButtons[4];
                break;
            case "cyan":
                colourSelect.transform.SetParent(colourButtons[5].transform, false);
                colourSelect.transform.position = colourButtons[5].transform.position;
                tempPlayer.colour = Colours.Cyan;
                tempSelectedcolourButton = colourButtons[5];
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
        menuPlayers.RemoveAt(menuPlayers.Count - 1);
        drawCards();
    }

    public void startGame()
    {
        //give each AI player a unique name
        int counter = 1;
        foreach (MenuPlayer p in menuPlayers)
        {
            if (p.isAI)
            {
                p.name = "AI" + counter.ToString();
                counter += 1;
            }
        }
        drawCards();
    }
}
