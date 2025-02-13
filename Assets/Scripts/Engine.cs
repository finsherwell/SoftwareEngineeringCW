using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Engine : MonoBehaviour
{
    [SerializeField] private int maxPlayers = 5;
    [SerializeField] public List<Player> players;
    [SerializeField] private int startingMoney = 1500;
    [SerializeField] private int passGoMoney = 200;
    [SerializeField] private BoardManager boardmanager;
    [SerializeField] private Dice dice;
    
    private int currentPlayerIndex = 0;
    private bool gameOver = false;


    void Start()
    {
        if (players.count < 1 && players.count > maxPlayers)
        {
            initializeGame(List<Player> players);
        }
        else
        {
            debug.LogError("Invalid number of players");
        }
    }

    private void initializeGame(List<Player> players)
    {
        foreach (var player in players)
        {
            player.AddMoney(startingMoney);
            Debug.Log($"{player.PlayerName} has {player.money} starting money");
        }
    }   
    private void passGo(Player player)
    {
        Debug.Log($"{Player.PlayerName} passed Go");
        Player.Addmoney(passGoMoney);
    }
    private void nextTurn()
    {
        Player currentPlayer = players[currentPlayerIndex];
    }

    private void RollAndMove(int diceValue, Player player)
    {

    }

    private void transactProperty(Player player, Property property)
    {

    }

    private void transactHouses(Player player, Property property, int houses)
    {
        
    }
}