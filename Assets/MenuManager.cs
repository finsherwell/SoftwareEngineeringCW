using Unity.VisualScripting;
using UnityEngine;
using MenuEnums;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEditor.TerrainTools;
using System.Collections.Generic;



public class MenuManager : MonoBehaviour
{


    public GameObject mainScreen;
    public GameObject lobbyScreen;
    public GameObject newPlayerScreen;

    private List<MenuPlayer> menuPlayers = new List<MenuPlayer>();
    public TMP_InputField playerNameInput;

    public MenuPlayer tempPlayer;
    public GameObject card1;

    void Start()
    {
        mainScreen.SetActive(true);
        lobbyScreen.SetActive(false);
        newPlayerScreen.SetActive(false);
    }

    public void swtichToLobby()
    {
        mainScreen.SetActive(false);
        lobbyScreen.SetActive(true);
        newPlayerScreen.SetActive(false);
    }
    public void switchToMenu()
    {
        mainScreen.SetActive(true);
        lobbyScreen.SetActive(false);
        newPlayerScreen.SetActive(false);
    }

    public void showNewPlayerScreen()
    {
        newPlayerScreen.SetActive(true);
        tempPlayer = new MenuPlayer("", Colours.Red, Icon.Ship);
        playerNameInput.text = "";
    }

    public void hideNewPlayerScreen()
    {
        newPlayerScreen.SetActive(false);
    }

    public void updateTempPlayerText(string n)
    {
        tempPlayer.name = n;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addPlayer()
    {
        menuPlayers.Append(tempPlayer);
    }

}
