using UnityEngine;

public class Tile : MonoBehaviour
{
    private Player[] playersOn;
    public Player[] players;
    [SerializeField] public string tileName;
    [SerializeField] private int ID;
    [SerializeField] protected bool isProperty;
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

    public bool IsProperty()
    {
        return this.isProperty;
    }

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

    public void SetIsProperty(bool isProperty)
    {
        this.isProperty = isProperty;
    }

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
