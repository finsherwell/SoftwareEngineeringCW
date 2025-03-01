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

    [SerializeField] private BoardManager boardmanager;
    [SerializeField] private Dice dice;

    [SerializeField] private int currentPlayerIndex = 0;
    [SerializeField] private TextMeshProUGUI currentPlayerText;
    private bool gameOver = false;
    [SerializeField] private int playerCount = 0; 
    [SerializeField] public Dice dice1;
    [SerializeField] public Dice dice2;
    [SerializeField] private Button rollButton;
    [SerializeField] private Button nextTurnButton;

    public Player currentPlayer;

    
     public void passGo(Player player) //when player lands on Go, add money to their account
     {
         Debug.Log($"{player.playerName} passed Go");
         player.addMoney(passGoMoney);
     }

    private void Start()
    {
        initializeGame();
    }

    private void Update()
    {

    }
    public void rollDice()
    {
        rollButton.gameObject.SetActive(false);
        // Start the dice rolls and use callbacks to handle the results once they finish
        dice1.rollAndReturn(value1 =>
        {
            int dice1Value = value1;
            Debug.Log($"Dice 1 rolled: {dice1Value}");

            dice2.rollAndReturn(value2 =>
            {
                int dice2Value = value2;
                Debug.Log($"Dice 2 rolled: {dice2Value}");

                // Calculate total dice value
                int totalDiceValue = dice1Value + dice2Value;
                Debug.Log($"Total Dice Value: {totalDiceValue}");
                movePlayer(totalDiceValue, currentPlayer);
                // Move the player based on the total dice value
                //movePlayer(totalDiceValue, currentPlayer);
                Debug.Log($"Player has moved {totalDiceValue} positions.");
            });
        });
        
        nextTurnButton.gameObject.SetActive(true);
    }
    private void FindPlayers()
    {
        GameObject [] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject playerObject in playerObjects)
        {
            Player player = playerObject.GetComponent<Player>();
            if (player!= null && players.Count < maxPlayers)
            {
                players.Add(player);
                playerCount++;
            }
        }
        Debug.Log($"found and added {players.Count} players");
    }


    private void initializeGame()
    {
        boardmanager.FindTiles();
        nextTurnButton.gameObject.SetActive(false);
        Debug.Log("Initializing game...1");
        Tile startTile = boardmanager.GetTile(0);
        FindPlayers();
        Debug.Log("Initializing game...2");
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
        }
        else
        {
            Debug.LogError("No players found in the scene!");
        }
    }

 

    private void RollAndMove(int diceValue, Player player)
    {
        nextTurnButton.gameObject.SetActive(false);
        rollButton.gameObject.SetActive(true);
    }

    private void movePlayer(int diceValue, Player player)
    {
        Debug.Log($"{player.playerName} is moving.");

        // Move the player through the tiles based on the dice roll
        for (int i = 0; i < diceValue; i++)
        {
            if (player.getCurrentTile() != null && player.getCurrentTile().GetNext() != null)
            {
                // Move the player to the next tile
                Tile nextTile = player.getCurrentTile().GetNext();
                player.setCurrentTile(nextTile);
                // Move the player's GameObject to the new tile position
                player.transform.position = nextTile.transform.position; // This updates the player's position in the world

                Debug.Log($"{player.playerName} landed on tile: {nextTile.GetName()}");
            }
            else
            {
                Debug.Log($"{player.playerName} has reached the end of the board!");
                break;  // Stop if there are no more tiles to move to
            }
        }
        Invoke("OnTileLanded", 0.5f);
    }

    private void OnTileLanded()
    {
        Debug.Log($"{currentPlayer.playerName} has landed on a tile.");
    }

    public void nextTurn() // Increment current player index and wrap when maxxed
    {
        currentPlayerIndex++;
        if (currentPlayerIndex == playerCount)
        {
            currentPlayerIndex = 0;
        }
        currentPlayer=players[currentPlayerIndex];
        nextTurnButton.gameObject.SetActive(false);
        rollButton.gameObject.SetActive(true);
        updateTurnText(currentPlayer);

    }
    private void updateTurnText(Player player)
    {
        string name = player.getName();
        currentPlayerText.text = $"Current Player: {name}";
    }
}
