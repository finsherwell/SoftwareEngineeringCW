using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanelManager : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> playerNameText;
    [SerializeField] private List<GameObject> playercards;
    [SerializeField] private List<Image> playerSprites;
    private List<Player> players;
    [SerializeField] GameObject pointer;

    public void InitializePlayerPanel(List<Player> players)
    {
        this.players = players;
        Debug.Log($"[PlayerPanelManager] InitializePlayerPanel called with {players.Count} players.");

        for (int i = 0; i < players.Count; i++)
        {
            Player player = players[i];
            int id = player.getID();

            Debug.Log($"[PlayerPanelManager] Setting up player ID {id}: {player.playerName}");

            if (id < 0 || id >= playercards.Count || id >= playerNameText.Count || id >= playerSprites.Count)
            {
                Debug.LogWarning($"[PlayerPanelManager] Player ID {id} is out of range of UI lists.");
                continue;
            }

            playerNameText[id].text = (player.playerName+"\n£"+player.money);

            Image cardImage = playercards[id].GetComponent<Image>();
            if (cardImage != null)
            {
                cardImage.color = GetColorFromEnum(player.colour);
            }
            else
            {
                Debug.LogWarning($"[PlayerPanelManager] No Image component found on card at index {id}.");
            }

            SpriteRenderer sr = player.GetComponent<SpriteRenderer>();
            if (sr != null && sr.sprite != null)
            {
                playerSprites[id].sprite = sr.sprite;
                Debug.Log($"[PlayerPanelManager] Assigned sprite for player {player.playerName} to UI card.");
            }
            else
            {
                playerSprites[id].sprite = null;
                Debug.LogWarning($"[PlayerPanelManager] No SpriteRenderer or sprite found on player {player.playerName}.");
            }

            playercards[id].SetActive(true);
        }

        for (int i = players.Count; i < playercards.Count; i++)
        {
            playercards[i].SetActive(false);
            playerNameText[i].text = "";
            playerSprites[i].sprite = null;
            Debug.Log($"[PlayerPanelManager] Hiding unused player card at index {i}.");
        }
    }

    public void removePlayer(int id, int p_count)
    {
        Debug.Log($"[PlayerPanelManager] Removing player visuals starting at index {id}");

        for (int i = id; i < playerNameText.Count - 1; i++)
        {
            // Shift name up
            playerNameText[i].text = playerNameText[i + 1].text;

            // Shift sprite up
            playerSprites[i].sprite = playerSprites[i + 1].sprite;

            // Shift card color up
            Image cardImageCurrent = playercards[i].GetComponent<Image>();
            Image cardImageNext = playercards[i + 1].GetComponent<Image>();

            if (cardImageCurrent != null && cardImageNext != null)
            {
                cardImageCurrent.color = cardImageNext.color;
            }

            // Make sure this card is active in case it was hidden earlier
            playercards[i].SetActive(true);
        }
        for (int i =p_count; i < 4; i++)
        {
            playercards[i].SetActive(false);
        }
    }
    public void updateArrow(int id)
    {
        // Get the current X and Z positions of the pointer (keeping X unchanged)
        float pointerXPosition = pointer.transform.position.x;
        float pointerZPosition = pointer.transform.position.z;

        // Get the Y position of the player card
        float playerCardYPosition = playercards[id].transform.position.y;

        // Create the new position with the current X and Z positions, and the updated Y position
        Vector3 newPosition = new Vector3(pointerXPosition, playerCardYPosition, pointerZPosition);

        // Update the pointer's position
        pointer.transform.position = newPosition;
    }

    private void Update()
    {
        if (players == null || players.Count == 0)
        { 
            return; // Exit early if no players
        }

        for (int i = 0; i < players.Count; i++)
        {
            Player player = players[i];
            if (player == null)
            {
                Debug.LogWarning($"Player at index {i} is null!");
                continue; // Skip if player is null
            }

            // Ensure the UI lists are not out of bounds
            if (i >= playerNameText.Count || i >= playerSprites.Count)
            {
                Debug.LogWarning($"Index {i} is out of bounds for player UI lists.");
                continue;
            }

            // Update player name and money
            playerNameText[i].text = player.playerName + "\n£" + player.money;
        }
    }

    public Color getPlayerColour(int id)
    {
        Player player = players[id];
        Debug.Log("color for player " + id);
        return GetColorFromEnum(player.colour);
    }

    private Color GetColorFromEnum(MenuEnums.Colours colour)
    {
        Color playerColor;
        switch (colour)
        {
            case MenuEnums.Colours.Green:
                ColorUtility.TryParseHtmlString("#33FF57", out playerColor); break;
            case MenuEnums.Colours.Yellow:
                ColorUtility.TryParseHtmlString("#FFD700", out playerColor); break;
            case MenuEnums.Colours.Blue:
                ColorUtility.TryParseHtmlString("#3357FF", out playerColor); break;
            case MenuEnums.Colours.Purple:
                ColorUtility.TryParseHtmlString("#8A2BE2", out playerColor); break;
            case MenuEnums.Colours.Red:
                ColorUtility.TryParseHtmlString("#FF5733", out playerColor); break;
            case MenuEnums.Colours.Cyan:
                ColorUtility.TryParseHtmlString("#00FFFF", out playerColor); break;
            default:
                Debug.LogWarning("[PlayerPanelManager] Unknown color enum, defaulting to white.");
                playerColor = Color.white; break;
        }
        return playerColor;
    }
}
