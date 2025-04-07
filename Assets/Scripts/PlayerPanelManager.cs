using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanelManager : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> playerNameText;   // List to hold player names
    [SerializeField] private List<GameObject> playercards;           // List of player card GameObjects (each one has an Image component)
    [SerializeField] private List<Image> playerSprites;              // List of Image components to hold the player's sprite

    public void InitializePlayerPanel(List<Player> players)
    {
        Debug.Log($"[PlayerPanelManager] InitializePlayerPanel called with {players.Count} players.");

        // Loop through each player in the list
        for (int i = 0; i < players.Count; i++)
        {
            Player player = players[i];
            int id = player.getID();

            Debug.Log($"[PlayerPanelManager] Setting up player ID {id}: {player.playerName}");

            // Safety check
            if (id < 0 || id >= playercards.Count || id >= playerNameText.Count || id >= playerSprites.Count)
            {
                Debug.LogWarning($"[PlayerPanelManager] Player ID {id} is out of range of UI lists.");
                continue;
            }

            // Set player name
            playerNameText[id].text = player.playerName;

            // Set player card color based on the player's colour
            Image cardImage = playercards[id].GetComponent<Image>();
            if (cardImage != null)
            {
                Color playerColor;
                switch (player.colour)
                {
                    case MenuEnums.Colours.Green:
                        ColorUtility.TryParseHtmlString("#33FF57", out playerColor);
                        break;
                    case MenuEnums.Colours.Yellow:
                        ColorUtility.TryParseHtmlString("#FFD700", out playerColor);
                        break;
                    case MenuEnums.Colours.Blue:
                        ColorUtility.TryParseHtmlString("#3357FF", out playerColor);
                        break;
                    case MenuEnums.Colours.Purple:
                        ColorUtility.TryParseHtmlString("#8A2BE2", out playerColor);
                        break;
                    case MenuEnums.Colours.Red:
                        ColorUtility.TryParseHtmlString("#FF5733", out playerColor);
                        break;
                    case MenuEnums.Colours.Cyan:
                        ColorUtility.TryParseHtmlString("#00FFFF", out playerColor);
                        break;
                    default:
                        Debug.LogWarning($"[PlayerPanelManager] Unknown player color for player {player.playerName}. Using white.");
                        playerColor = Color.white;
                        break;
                }

                // Apply the determined color to the card's Image component
                cardImage.color = playerColor;
            }
            else
            {
                Debug.LogWarning($"[PlayerPanelManager] No Image component found on card at index {id}.");
            }

            // Set player sprite
            SpriteRenderer sr = player.GetComponent<SpriteRenderer>();
            if (sr != null && sr.sprite != null)
            {
                Image playerCardImage = playerSprites[id];
                if (playerCardImage != null)
                {
                    playerCardImage.sprite = sr.sprite;
                    Debug.Log($"[PlayerPanelManager] Assigned sprite for player {player.playerName} to UI card.");
                }
                else
                {
                    Debug.LogWarning($"[PlayerPanelManager] No Image component found in playerSprites list for player {player.playerName}.");
                }
            }
            else
            {
                Debug.LogWarning($"[PlayerPanelManager] No SpriteRenderer or sprite found on player {player.playerName}.");
            }
        }

        // Disable unused cards if there are fewer players than cards
        for (int i = players.Count; i < playercards.Count; i++)
        {
            if (playercards[i] != null)
            {
                playercards[i].SetActive(false);
                Debug.Log($"[PlayerPanelManager] Hiding unused player card at index {i}.");
            }
        }
    }
}
//