 using UnityEditor;
 using UnityEngine;
 using UnityEngine.Tilemaps;

 public class Player : MonoBehaviour
 {
     // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         int money = 1500;
         int playerID;
         Property protperties[];
         Tile currentTile = passGo;
         bool inJail = false;

     }

      Update is called once per frame
     void Update()
     {

     }

     Property getProperty(void) { return properties; }

     public void addMoney(int amount) { this.money += amount; }

     void takeMoney(int amount) { this.money -= amount; }
 }
