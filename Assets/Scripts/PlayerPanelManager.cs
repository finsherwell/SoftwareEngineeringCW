using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanelManager : MonoBehaviour
{
    [SerializeField] public List<TextMeshProUGUI> playerNameText;
    [SerializeField] public List<GameObject> playercards;

    public void InitializePlayerPanel(List<Player> players)
    {
        Image cardImage;
        foreach (var player in players)
        {
            cardImage = playercards[player.getID()].GetComponent<Image>(); 
            cardImage.color = player.colour;
            playerNameText[player.getID()].text = player.playerName;
        }
        for (int i = players.Count; i < playercards.Count; i++)
        {
            playercards[i].SetActive(false);
        }
    }





    // Update is called once per frame
    void Update()
    {

    }
}

