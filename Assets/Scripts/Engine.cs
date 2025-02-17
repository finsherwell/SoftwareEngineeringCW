 using UnityEngine;
using System.Collections.Generic;
 public class Engine : MonoBehaviour
 {
    [SerializeField] public List<Player> players;
    [SerializeField] private int startingMoney = 1500;
    [SerializeField] private int passGoMoney = 200;
    [SerializeField] private int maxPlayers = 5;
    [SerializeField] private BoardManager boardmanager;
    [SerializeField] private Dice dice;

     private int currentPlayerIndex = 0;
     private bool gameOver = false;


     private void Start()
     {
         initializeGame();
     }

     private void initializeGame()
     {
         foreach (var player in players)
         {
             player.AddMoney(startingMoney);
             Debug.Log($"{player.PLayerName} has {player.money} starting money");
         }
     }   
     private void passGo(Player player)
     {
         Debug.Log($"{player.PlayerName} passed Go")
         player.Addmoney(passGoMoney);
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