using UnityEngine;

public class Property : MonoBehaviour
{
    [SerializeField] private string propertyName;
    [SerializeField] private string group; // Color set (e.g., "Brown", "Blue")
    [SerializeField] private int price;
    [SerializeField] private int houseCost;
    [SerializeField] private int[] rentLevels; // Rent for different levels (0-4 houses + hotel)
    
    private bool isOwned = false;
    private int houses = 0;
    private Player owner = null;

    public string GetPropertyName()
    {
        return propertyName;
    }

    public string GetGroup()
    {
        return group;
    }

    public int GetPrice()
    {
        return price;
    }

    public int GetHouseCost()
    {
        return houseCost;
    }

    public int GetRent()
    {
        return rentLevels[houses];
    }

    public bool IsOwned()
    {
        return isOwned;
    }

    public Player GetOwner()
    {
        return owner;
    }

    public void PurchaseProperty(Player buyer)
    {
        if (!isOwned && buyer.money >= price)
        {
            buyer.money -= price;
            owner = buyer;
            isOwned = true;
        }
    }

    public void UpgradeProperty()
    {
        if (isOwned && owner.money >= houseCost && houses < 5) // Max: 4 houses + hotel
        {
            owner.money -= houseCost;
            houses++;
        }
    }

    public void PayRent(Player tenant)
    {
        if (isOwned && tenant != owner)
        {
            int rent = GetRent();
            tenant.money -= rent;
            owner.money += rent;
        }
    }
}
