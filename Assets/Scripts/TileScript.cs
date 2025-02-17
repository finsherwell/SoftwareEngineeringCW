// using UnityEngine;

// public class TileScript : MonoBehaviour
// {
//     private Player[] playersOn;
//     public Player[] players;
//     private string tileName;
//     private string ID;
//     private bool isProperty;

//     void Start()
//     {
//         playersOn = new Player[10];
//     }

//     void Update()
//     {
//     }

//     public string GetName()
//     {
//         return this.tileName;
//     }

//     public TileScript GetNext()
//     {
//         return null;
//     }

//     public string GetID()
//     {
//         return this.ID;
//     }

//     public bool IsProperty()
//     {
//         return this.isProperty;
//     }

//     public bool LandedOn()
//     {
//         return playersOn != null && playersOn.Length > 0;
//     }

//     public void SetName(string name)
//     {
//         this.tileName = name;
//     }

//     public void SetID(string id)
//     {
//         this.ID = id;
//     }

//     public void SetIsProperty(bool isProperty)
//     {
//         this.isProperty = isProperty;
//     }

//     public void GetLandedOn()
//     {
//         for (int i = 0; i < players.Length; i++)
//         {
//             if (players[i].positionID == ID)
//             {
//                 playersOn[i] = players[i];
//             }
//         }
//     }
// }
