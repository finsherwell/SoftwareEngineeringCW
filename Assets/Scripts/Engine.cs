using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Engine : MonoBehaviour
{
    [SerializeField] public List<Player> players;
    [SerializeField] private int startingMoney = 1500;
    [SerializeField] private int passGoMoney = 200;
    [SerializeField] private int maxPlayers = 5;
    [SerializeField] private int currentPlayerIndex = 0;
    [SerializeField] private TextMeshProUGUI currentPlayerText;
    private bool gameOver = false;
    [SerializeField] private int playerCount = 0; 
    [SerializeField] public Dice dice1;
    [SerializeField] public Dice dice2;
    [SerializeField] private Button rollButton;
    [SerializeField] private Button nextTurnButton;
    [SerializeField] private Tile startTile;

    public Player currentPlayer;

    public void passGo(Player player)
    {
        Debug.Log($"{player.playerName} passed Go");
        player.addMoney(passGoMoney);
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
                movePlayer(totalDiceValue, currentPlayer);
            });
        });
    }


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

    private void initializeGame()
    {
        nextTurnButton.gameObject.SetActive(false);
        Debug.Log("Initializing game...");
        FindPlayers();        
        foreach (Player player in players)
        {
            player.addMoney(startingMoney);
            Debug.Log($"{player.playerName} has {player.money} starting money");
            player.setID(players.IndexOf(player));
            player.setCurrentTile(startTile);
            player.transform.position = startTile.transform.position;
        }

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
                Debug.Log($"{player.playerName} landed on tile: {nextTile.GetName()}");
            }
            else
            {
                Debug.Log($"{player.playerName} has reached the end of the board!");
                break;
            }
        }
        CheckForActionEvent(player);
        nextTurnButton.gameObject.SetActive(true);
    }

    private void OnTileLanded()
    {
        Debug.Log($"{currentPlayer.playerName} has landed on a tile.");
    }

    public void nextTurn()
    {
        currentPlayer.gameObject.GetComponent<Renderer>().material.color = new Color(255, 255, 255);
        currentPlayerIndex++;
        if (currentPlayerIndex == playerCount)
        {
            currentPlayerIndex = 0;
        }
        currentPlayer = players[currentPlayerIndex];
        nextTurnButton.gameObject.SetActive(false);
        rollButton.gameObject.SetActive(true);
        updateTurnText(currentPlayer);
        // Assuming currentPlayer is a reference to your Player class
        currentPlayer.gameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 0);
        // RGB values for black

    }

    private void updateTurnText(Player player)
    {
        string name = player.getName();
        currentPlayerText.text = $"Current Player: {name}";
    }
    private void CheckForActionEvent(Player player)
    {
        Tile currentTile = player.getCurrentTile();
        if (currentTile != null)
        {
            ActionSpace actionSpace = currentTile.GetComponent<ActionSpace>(); // Get ActionSpace component
            if (actionSpace != null)
            {
                actionSpace.LandedOn(player); // Trigger the action event
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
            }
        }
    }

}
