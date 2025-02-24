 using UnityEngine;
using System.Collections.Generic;
using Unity.Properties;
using TMPro;
public class Engine : MonoBehaviour
 {
    [SerializeField] public List<Player> players;

    //[SerializeField] public List<Property> properties;
    [SerializeField] private int startingMoney = 1500;
    [SerializeField] private int passGoMoney = 200;
    [SerializeField] private int maxPlayers = 5;
    [SerializeField] private BoardManager boardmanager;
    [SerializeField] private Dice dice;

    [SerializeField] private GameObject currentPlayerTextObject; 
    private TextMeshProUGUI currentPlayerText; 

    [SerializeField] private int currentPlayerIndex = 0;
     private bool gameOver = false;


     private void Start()
     {
         initializeGame();
     }

     private void initializeGame() //create players, initialize money, and set starting position
     {
         int i = 0;
         foreach (var player in players)
         {
             player.addMoney(startingMoney);
             Debug.Log($"{player.playerName} has {player.money} starting money");
             player.setID(i);
             i++;
         }
     }   

     public void passGo(Player player) //when player lands on Go, add money to their account
     {
         Debug.Log($"{player.playerName} passed Go");
         player.addMoney(passGoMoney);
     }
     private void nextTurn()//increment current player index and wrap when maxxed
     {
        currentPlayerIndex++;
        if (currentPlayerIndex == maxPlayers)
        {
             currentPlayerIndex = 0;
        }
        currentPlayerText.text = $"Current Player: {players[currentPlayerIndex].playerName}";
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