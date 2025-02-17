using JetBrains.Annotations;
using UnityEditor;
 using UnityEngine;
 using UnityEngine.Tilemaps;

 public class Player : MonoBehaviour
 {
    public int money; 

    [SerializeField] public string playerName;
    [SerializeField] private int playerID;
    //Property properties[];
    Tile currentTile;
    bool inJail = false;
    void Start()
    {
         
    }
    
    // Property getProperty(void) { return properties; }

     public int getID(int playerID){return this.playerID;  }
     public void setID(int playerID){this.playerID = playerID;  }

     public void addMoney(int amount) { this.money += amount; }

     void takeMoney(int amount) { this.money -= amount; }
    
 }
