using UnityEngine;

public class Tile : MonoBehaviour
{
    protected Player[] playersOn;
    public Player[] players;
//    [SerializeField] private string tileName; removed for inheritance 
    [SerializeField] private int ID;
  //  [SerializeField] private bool isProperty;  removed for inheritance
    public Tile nextTile;

    void Start()
    {
        playersOn = new Player[10];
    }

    void Update()
    {
        
    }

    public string GetName()
    {
        return this.tileName;
    }

    public Tile GetNext()
    {
        return nextTile;
    }

    public int GetID()
    {
        return this.ID;
    }

   /* public bool IsProperty()
    {
        return this.isProperty;
    }
    
    removed for inheritance
*/

    public bool LandedOn()
    {
        return playersOn != null && playersOn.Length > 0;
    }

    public void SetName(string name)
    {
        this.tileName = name;
    }

    public void SetID(int id)
    {
        this.ID = id;
    }
/*
    public void SetIsProperty(bool isProperty)
    {
        this.isProperty = isProperty;
    }
    removed for inheritance
*/
     public void GetLandedOn()
     {
         for (int i = 0; i < players.Length; i++)
         {
            if (players[i].getPositionID() == ID)
             {
                 playersOn[i] = players[i];
             }
        }
     }
}
