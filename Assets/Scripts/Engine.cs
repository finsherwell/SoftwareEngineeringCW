 using UnityEngine;
using System.Collections.Generic;
using Unity.Properties;
public class Engine : MonoBehaviour
 {
    [SerializeField] public List<Player> players;

    //[SerializeField] public List<Property> properties;
    [SerializeField] private int startingMoney = 1500;
    [SerializeField] private int passGoMoney = 200;
    [SerializeField] private int maxPlayers = 5;
    [SerializeField] private BoardManager boardmanager;
    [SerializeField] private Dice dice;

    [SerializeField] private int currentPlayerIndex = 0;
     private bool gameOver = false;


     private void Start()
     {
         initializeGame();
     }

     private void initializeGame()
     {
         int i = 0;
         foreach (var player in players)
         {
             player.addMoney(startingMoney);
             Debug.Log($"{player.playerName} has {player.money} starting money");
             player.setID(i);
             i++;
             currentPlayerIndex = 0;
         }
     }   

     public void passGo(Player player)
     {
         Debug.Log($"{player.playerName} passed Go");
         player.addMoney(passGoMoney);
     }
     public void nextTurn()
     {
        currentPlayerIndex += 1;
         if (currentPlayerIndex >= players.Count)
         {
             currentPlayerIndex = 0;
         }
     }
 }
/*
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
 */