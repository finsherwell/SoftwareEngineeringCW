using System.Data.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Property : Tile
{
    [SerializeField] private string propertyName; // Name of the property (e.g. "The Old Creek")
    [SerializeField] private string group; // Color set (e.g., "Brown", "Blue")
    [SerializeField] private int price; // Original cost to buy the property
    [SerializeField] private int houseCost; // 
    [SerializeField] private int[] rentLevels; // Rent for different levels (0-4 houses + hotel)
    [SerializeField] private Button button; // Button to or upgrade property
    [SerializeField] private Button sellbutton; // button to sell house / property
    [SerializeField] private bool isOwned = false; // Will be true if the property is owned
    [SerializeField] private bool isMortgaged = false; // Will be true if the property is mortgaged
    [SerializeField] private int houses = 0; // Updates if you upgrade houses
    [SerializeField] private Player owner = null; // Assigned to the player that owns the property
    [SerializeField] private Sprite rent_s; // Assigned to the player that owns the property

    [SerializeField] public GameObject housePrefab;
    [SerializeField] public GameObject hotelPrefab;

    private List<GameObject> houseObjects = new List<GameObject>();
    private  float houseSpacing = 1.15f; // Space between houses
    private float houseCenterOffset = 2.95f; // Houses offset off centre
    private GameObject hotelObject;


    private void Awake()
    {

        propertyName = tileName;

    }
    /*
    Returns the name of the property.
    */
    public string GetPropertyName()
    {
        return propertyName;
    }
    public void setOwned(bool value)
    {
        isOwned = value;
    }
    private void Start()
    {
        UpdatebuyButtonText();
        UpdateSellButtonText();
        SetupButtonListener();
        SetupSellButtonListener();
    }
    private void SetupButtonListener()
    {
        if (button != null)
        {
            button.onClick.AddListener(OnbuyButtonClick);
        }
    }
    private void SetupSellButtonListener()
    {
        if (sellbutton != null)
        {
            sellbutton.onClick.AddListener(OnsellButtonClick);
        }
    }

    private void OnbuyButtonClick()
    {
        Debug.Log($"Buying house on:  {propertyName}");
        if (isOwned && owner.money >= houseCost && houses <= 5) // Max: 4 houses + hotel
        {
            UpgradeProperty();
            UpdatebuyButtonText();
            UpdateSellButtonText();
        }
    }
    private void OnsellButtonClick()
    {

        DowngradeProperty();
        UpdatebuyButtonText();
        UpdateSellButtonText();
    }
    public void UpgradeProperty() // allows player to buy houses for their property
    {
        owner.takeMoney(houseCost);
        owner.houseBought(houseCost);
        houses++;
        UpdateHouseDisplay();
    }

    public void DowngradeProperty() // allows player to sell houses for their property
    {
        if (houses > 0)
        {
            owner.addMoney(houseCost);
            owner.houseSold(houseCost);
            houses--;
            UpdateHouseDisplay();
        }
    }

    private void UpdateHouseDisplay()
    {
        ClearHouses();
        
        // Handle hotel (5 houses)
        if (houses == 5)
        {
            AddHotel();
            return;
        }
        
        // Add houses
        for (int i = 0; i < houses; i++)
        {
            AddHouse(i);
        }
    }

    private void ClearHouses()
    {
        foreach (GameObject house in houseObjects)
        {
            Destroy(house);
        }
        houseObjects.Clear();
        
        if (hotelObject != null)
        {
            Destroy(hotelObject);
            hotelObject = null;
        }
    }

    private void AddHouse(int houseIndex)
    {
        GameObject house = Instantiate(housePrefab, transform, false);
        house.transform.localScale = new Vector3(1f, 1f, 1f);
        house.transform.localPosition = new Vector3(0, 0, 0);

        houseObjects.Add(house);
        
        Engine.TileOrientation orientation = GetTileOrientationFromGroup();
        PositionHouse(house, houseIndex, orientation);
    }

    private void AddHotel()
    {
        hotelObject = Instantiate(hotelPrefab, transform, false);
        hotelObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f); 
        hotelObject.transform.localPosition = new Vector3(0, 0, 0);

        SpriteRenderer propertyRenderer = hotelObject.GetComponent<SpriteRenderer>();
        if (propertyRenderer != null)
        {
            propertyRenderer.color = Color.red;
        }
        
        Engine.TileOrientation orientation = GetTileOrientationFromGroup();
        PositionHotel(hotelObject, orientation);
    }

    private void PositionHouse(GameObject house, int houseIndex, Engine.TileOrientation orientation)
    {
        house.transform.localPosition = new Vector3(0, 0, 0);
        float totalWidth = (houses - 1) * houseSpacing;
        float startX = -totalWidth / 2f;
        float positionX = startX + (houseIndex * houseSpacing);
        
        Vector3 position = Vector3.zero;
        Quaternion rotation = Quaternion.identity;
        
        switch (orientation)
        {
            case Engine.TileOrientation.Bottom:
                position = new Vector3(positionX, houseCenterOffset, 0);
                rotation = Quaternion.identity;
                break;
                
            case Engine.TileOrientation.Left:
                position = new Vector3(houseCenterOffset, positionX, 0);
                rotation = Quaternion.Euler(0, 90, 0);
                break;
                
            case Engine.TileOrientation.Top:
                position = new Vector3(positionX, -houseCenterOffset, 0);
                rotation = Quaternion.Euler(0, 180, 0);
                break;
                
            case Engine.TileOrientation.Right:
                position = new Vector3(-houseCenterOffset, positionX, 0);
                rotation = Quaternion.Euler(0, 270, 0);
                break;
        }
        
        house.transform.localPosition = position;
        house.transform.localRotation = rotation;
    }

    private void PositionHotel(GameObject hotel, Engine.TileOrientation orientation)
    {
        hotel.transform.localPosition = new Vector3(0, 0, 0);
        Vector3 position = Vector3.zero;
        Quaternion rotation = Quaternion.identity;
        
        switch (orientation)
        {
            case Engine.TileOrientation.Bottom:
                position = new Vector3(0, houseCenterOffset, 0);
                rotation = Quaternion.identity;
                break;
                
            case Engine.TileOrientation.Left:
                position = new Vector3(houseCenterOffset, 0, 0);
                rotation = Quaternion.Euler(0, 90, 0);
                break;
                
            case Engine.TileOrientation.Top:
                position = new Vector3(0, -houseCenterOffset, 0);
                rotation = Quaternion.Euler(0, 180, 0);
                break;
                
            case Engine.TileOrientation.Right:
                position = new Vector3(-houseCenterOffset, 0, 0);
                rotation = Quaternion.Euler(0, 270, 0);
                break;
        }
        
        hotel.transform.localPosition = position;
        hotel.transform.localRotation = rotation;
    }

    private Engine.TileOrientation GetTileOrientationFromGroup()
    {
        if (group == "Brown" || group == "Lblue")
            return Engine.TileOrientation.Bottom;

        if (group == "Purple" || group == "Orange")
            return Engine.TileOrientation.Left;

        else if (group == "Red" || group == "Yellow")
            return Engine.TileOrientation.Top;

        else if (group == "Green" || group == "Dblue")
            return Engine.TileOrientation.Right;

        return Engine.TileOrientation.Bottom;
    }

    private void UpdateSellButtonText()
    {
        if (sellbutton == null)
        {
            Debug.LogWarning("Sell button is not assigned.");
            return;
        }

        var textComponent = sellbutton.GetComponentInChildren<TextMeshProUGUI>();

        if (textComponent == null)
        {
            Debug.LogWarning("No TextMeshProUGUI found in sell button.");
            return;
        }

        textComponent.text = $"Sell House for ${houseCost}";
    }

    private void UpdatebuyButtonText()
    {
        if (button != null && button.GetComponentInChildren<TextMeshProUGUI>() != null)
        {
            if (houses < 5)
            {
                button.GetComponentInChildren<TextMeshProUGUI>().text = $"Purchase for ${houseCost}";
            }
            else
            {
                button.GetComponentInChildren<TextMeshProUGUI>().text = "Fully Upgraded";
            }
        }
    }
    public int getHouseCost()
    {
        return houseCost;
    }
    public Sprite getSprite()
    {
        return rent_s;
    }

    public bool GetMortgaged()
    {
        return isMortgaged;
    }
    public void SetMortgaged(bool value)
    {
        isMortgaged = value;
    }


    public int GetPrice()
    {
        return price;
    }

    public int GetHouseCount()
    {
        return houses;
    }

    public string GetGroup()
    {
        return group;
    }

    public bool HasHotel()
    {
        return houses == 5;
    }
    public int GetRentLevels()
    {
        return rentLevels[houses];
    }
    public int GetRent(int i)
    {
        return rentLevels[i];
    }
    public bool IsStation()
    {
        if (group == "Station")
        { return true; }
        else { return false; }
    }
    public bool IsUtility()
    {
        if (group == "Utility")
        { return true; }
        else { return false; }
    }


    /*
//    Returns the cost of the house.
//    */
    //    public int GetHouseCost()
    //    {
    //        return houseCost;
    //    }

    //    /*
    //    Returns the rent of the property.
    //    */
    //    public int GetRent()
    //    {
    //        return rentLevels[houses];
    //    }

    /*
     Returns whether the property is owned or not.
     */
    public bool IsOwned()
    {
        return isOwned;
    }

    /*
    Returns the owner of the property.
    */
    public Player GetOwner()
    {
        return owner;
    }

    public void SetOwner(Player newOwner)
    {
        owner = newOwner;//sets the owner on tile
        newOwner.addProperty(this);//adds the property to the player's properties list 
        isOwned = true;//sets the owned flag to true
    }

    public void ShowButtonCheck(Player player)//logic for showing which buttons to be shows on properties that match the criteria for buying houses
    {
        if (player == owner && player.HasCompleteSet(this.group) && player.money >= houseCost)//checking if player has the full set as well as enough money to buy houses
        {
            button.gameObject.SetActive(true);
        }
        else
        {
            button.gameObject.SetActive(false);
        }
    }
    public void ShowSellButtonCheck(Player player)
    {
        if (sellbutton != null && player == owner)//checking for properties with houses and corresponding ownership
        {
            sellbutton.gameObject.SetActive(true);
        }
    }

    public void HideButtonCheck(Player player)
    {
        if (player == owner)
        {
            button.gameObject.SetActive(false);
        }
    }
    public void HideSellButtonCheck(Player currentPlayer)
    {
        if (sellbutton != null)
        {
            sellbutton.gameObject.SetActive(false);
        }
    }
    public void sellHouse()
    {
        if (!isOwned)
        {
            return;
        }
        else
        {
            owner = null;
        }
    }


    /*
    Allows a user to buy the property as long as it isn't owned and they have the funds to buy it.
    */
    public void PurchaseProperty(Player buyer)
    {
        if (!isOwned && buyer.money >= price)
        {
            buyer.takeMoney(price);
            owner = buyer;
            isOwned = true;
        }
    }
    public int GetHouses()
    {
        return houses;
    }







}
//    /*
//    Handles rent payment between the tenant and the owner of the property.
//    */
//    public void PayRent(Player tenant)
//    {
//        if (isOwned && tenant != owner)
//        {
//            int rent = GetRent();
//            tenant.takeMoney(rent);
//            owner.addMoney(rent);
//        }
//    }

//}
