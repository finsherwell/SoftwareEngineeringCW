using UnityEngine;

public class Property : MonoBehaviour
{
    public Tile tile;
    private int setID;
    private int price;
    private int rent;
    private bool isOwned;

    // public bool isProperty()
    // {
    //     return tile.isProperty();
    // }
    public int getSet()
    {
        return this.setID;
    }
    public int getPrice()
    {
        return this.price;
    }
    public int getRent()
    {
        return this.rent;
    }
    // private bool isOwned()
    // {
    //     return this.isOwned;
    // }
    // public player OwnedBy()
    // {
    //     if (isOwned())
    //     {
    //         //return player that owns property 
    //     }
    // }
}
