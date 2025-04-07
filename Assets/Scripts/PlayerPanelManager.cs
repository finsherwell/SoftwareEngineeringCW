//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;
//using UnityEngine.UI;

//public class PlayerPanelManager : MonoBehaviour
//{
//    [SerializeField] public List<TextMeshProUGUI> playerNameText;
//    [SerializeField] public List<GameObject> playercards;

    public void InitializePlayerPanel(List<Player> players)
    {
        Image cardImage;
        foreach (var player in players)
        {
            cardImage = playercards[player.getID()].GetComponent<Image>();

            switch (player.colour)
            {

                case MenuEnums.Colours.Green:
                    if (ColorUtility.TryParseHtmlString("#33FF57", out var c)) cardImage.color = c;
                    break;
                case MenuEnums.Colours.Yellow:
                    if (ColorUtility.TryParseHtmlString("#FFD700", out var d)) cardImage.color = d;
                    break;
                case MenuEnums.Colours.Blue:
                    if (ColorUtility.TryParseHtmlString("#3357FF", out var e)) cardImage.color = e;
                    break;
                case MenuEnums.Colours.Purple:
                    if (ColorUtility.TryParseHtmlString("#8A2BE2", out var f)) cardImage.color = f;
                    break;
                case MenuEnums.Colours.Red:
                    if (ColorUtility.TryParseHtmlString("#FF5733", out var g)) cardImage.color = g;
                    break;
                case MenuEnums.Colours.Cyan:
                    if (ColorUtility.TryParseHtmlString("#00FFFF", out var h)) cardImage.color = h;
                    break;

            }

            playerNameText[player.getID()].text = player.playerName;
        }
        for (int i = players.Count; i < playercards.Count; i++)
        {
            playercards[i].SetActive(false);
        }
    }





//    // Update is called once per frame
//    void Update()
//    {

//    }
//}

