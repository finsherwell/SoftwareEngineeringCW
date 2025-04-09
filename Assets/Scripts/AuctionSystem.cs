using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class AuctionSystem : MonoBehaviour
{
    [SerializeField] private GameObject auctionPanel;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI propertyText;
    [SerializeField] private TextMeshProUGUI currentBidText;
    [SerializeField] private TextMeshProUGUI currentBidderText;
    [SerializeField] private Button placeBidButton;
    [SerializeField] private Button passBidButton;
    [SerializeField] private TMP_InputField inputBidField;
    [SerializeField] private Engine gameEngine;

    private Property currentProperty;
    private int currentBid = 0;
    private Player currentBidder = null;
    private int currentBidderIndex = 0;
    private List<Player> eligiblePlayers = new List<Player>();
    private List<Player> activeBidders = new List<Player>();

    private void Awake()
    {
        placeBidButton.onClick.AddListener(PlaceBid);
        passBidButton.onClick.AddListener(PassBid);
    }

    public void StartAuction(Property property)
    {
        currentProperty = property;
        currentBid = 0;
        currentBidder = null;

        eligiblePlayers.Clear();
        activeBidders.Clear();

        foreach (Player player in gameEngine.players)
        {
            if (player.hasCompletedCircuit)
            {
                eligiblePlayers.Add(player);
                activeBidders.Add(player);
            }
        }

        if (eligiblePlayers.Count == 0)
        {
            Debug.Log("No eligible players for auction");
            EndAuction();
            return;
        }

        propertyText.text = property.GetName();
        currentBidText.text = "Current Bid: £0";

        int startPlayerIndex = (gameEngine.players.IndexOf(gameEngine.currentPlayer) + 1) % gameEngine.players.Count;
        currentBidderIndex = FindNextEligiblePlayerIndex(startPlayerIndex - 1);
        
        if (currentBidderIndex == -1)
        {
            EndAuction();
            return;
        }
        
        UpdatePlayerTurn();
        
        inputBidField.text = "";
        auctionPanel.SetActive(true);
        gameEngine.logText.text = $"Auction started for {property.GetName()}\n\n" + gameEngine.logText.text;
    }

    private void UpdatePlayerTurn()
    {
        Player currentTurnPlayer = gameEngine.players[currentBidderIndex];
        currentBidderText.text = $"{currentTurnPlayer.playerName}'s turn to bid";
    }

    private int FindNextEligiblePlayerIndex(int startIndex)
    {
        int index = startIndex;
        for (int i = 0; i < gameEngine.players.Count; i++)
        {
            index = (index + 1) % gameEngine.players.Count;
            Player player = gameEngine.players[index];
            if (activeBidders.Contains(player))
            {
                return index;
            }
        }
        return -1;
    }

    public void PlaceBid()
    {
        if (!int.TryParse(inputBidField.text, out int bidAmount))
        {
            currentBidderText.text = "Please enter a valid number";
            return;
        }
        
        Player bidder = gameEngine.players[currentBidderIndex];
        
        if (bidAmount <= currentBid)
        {
            currentBidderText.text = $"Bid must be higher than ${currentBid}";
            return;
        }
        
        if (bidAmount > bidder.money)
        {
            currentBidderText.text = $"Not enough money. You have ${bidder.money}";
            return;
        }
        
        currentBid = bidAmount;
        currentBidder = bidder;
        currentBidText.text = $"Current Bid: ${currentBid}";
        
        gameEngine.logText.text = $"{bidder.playerName} bid ${bidAmount} for {currentProperty.GetName()}\n\n" + gameEngine.logText.text;
        
        AdvanceToNextBidder();
    }
    
    public void PassBid()
    {
        Player currentPlayer = gameEngine.players[currentBidderIndex];
        gameEngine.logText.text = $"{currentPlayer.playerName} passed on bidding\n\n" + gameEngine.logText.text;
        
        activeBidders.Remove(currentPlayer);
        
        if (activeBidders.Count == 1)
        {
            currentBidder = activeBidders[0];
            if (currentBid == 0)
            {
                currentBid = 1;
            }
            CompleteAuction();
            return;
        }
        
        if (activeBidders.Count == 0)
        {
            EndAuction();
            return;
        }
        
        AdvanceToNextBidder();
    }
    
    private void AdvanceToNextBidder()
    {
        int nextIndex = FindNextEligiblePlayerIndex(currentBidderIndex);
        
        if (nextIndex == -1 || activeBidders.Count <= 1)
        {
            CompleteAuction();
            return;
        }
        
        currentBidderIndex = nextIndex;
        UpdatePlayerTurn();
        
        inputBidField.text = "";
    }
    
    private void CompleteAuction()
    {
        if (currentBidder != null && currentBid > 0)
        {
            currentBidder.takeMoney(currentBid);
            currentProperty.SetOwner(currentBidder);
            
            gameEngine.logText.text = $"Auction complete: {currentBidder.playerName} purchased {currentProperty.GetName()} for ${currentBid}\n" + gameEngine.logText.text;
            Debug.Log($"Auction complete: {currentBidder.playerName} purchased {currentProperty.GetName()} for ${currentBid}");
        }
        else
        {
            gameEngine.logText.text = $"Auction complete: No one purchased {currentProperty.GetName()}\n\n" + gameEngine.logText.text;
            Debug.Log($"Auction complete: No one purchased {currentProperty.GetName()}");
        }
        
        EndAuction();
    }
    
    private void EndAuction()
    {
        auctionPanel.SetActive(false);
        gameEngine.nextTurnButton.gameObject.SetActive(true);
    }
}