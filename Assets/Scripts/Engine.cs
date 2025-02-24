using UnityEngine;
using System.Collections.Generic;

public class Engine : MonoBehaviour
{
    [SerializeField] public List<Player> players;
    //[SerializeField] public List<Property> properties;
    [SerializeField] private int startingMoney = 1500;
    [SerializeField] private int passGoMoney = 200;
    [SerializeField] private int maxPlayers = 5;
    [SerializeField] public Dice dice1;
    [SerializeField] public Dice dice2;
    [SerializeField] private Tile currentTile;
    public Player currentPlayer;
    private int currentPlayerIndex = 0;
    private bool gameOver = false;

    private void Start()
    {
        initializeGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            dice1.rollButtonPressed();
            dice2.rollButtonPressed();
            int dice1Value = dice1.getValue();
            int dice2Value = dice2.getValue();
            int totalDiceValue = dice1Value + dice2Value;
            RollAndMove(totalDiceValue, currentPlayer);
            Debug.Log($"Player has moved {totalDiceValue} positions.");
        }
    }

    private void initializeGame()
    {
        foreach (var player in players)
        {
            player.addMoney(startingMoney);
            Debug.Log($"{player.playerName} has {player.money} starting money");

            // Set the starting tile for each player, assuming the first tile (ID = 0) is the starting point
        }
    }

    public void passGo(Player player)
    {
        Debug.Log($"{player.playerName} passed Go");
        player.addMoney(passGoMoney);
    }

    private void RollAndMove(int diceValue, Player player)
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
                player.transform.position = nextTile.transform.position;  // This updates the player's position in the world

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
}


     //private void transactProperty(Player player, Property property)
     //{

     //}

     //private void transactHouses(Player player, Property property, int houses)
     //{

     //}