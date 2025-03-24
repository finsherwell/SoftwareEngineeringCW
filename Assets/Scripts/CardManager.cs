using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class Action
{
    // Enum that stores all the possible actions, which will be used to process actions.
    public enum Actions {
        pay_player,
        pay_bank,
        move,
        opportunity_knocks,
        put_free_parking,
        jail,
        receive_player,
        avoid_jail,
        pay_bank_per_house,
        pay_bank_per_hotel,
        move_back
    }
    
    // Type of action, default is pay_player.
    public Actions action = Actions.pay_player;
    
    // If the action requires an amount, it is defined here, otherwise it is 0 by default.
    public int amount = 0;

    // If the action has a property it needs to move to, then it will be stored here, default is null.
    public string property = null;

    // If the action requires a number of spaces to move back, it will be stored here.
    public int spaces = 0;
}

[System.Serializable]
public class ActionCard
{
    // Stores the number of the card, which is what it is indexed by in the card-data.json file.
    // Will be used when displaying the card on the screen to the user.
    public int cardNum { get; set; }

    // Stores the description of the action.
    public string description { get; set; }

    // Stores the first action, which will always exist.
    public Action action1 { get; set; }

    // Stores the second action, which is only necessary when there is more than one action, it is null by default.
    public Action action2 { get; set; }

    // Stores whether there is a choice between actions, in scenarios where there are 2 actions.
    // False by default as most actions do not have a choice.
    public bool choice { get; set; }

    // Stores the type of the card, Pot Luck or Opportunity Knocks.
    public string type { get; set; }
}

public class CardManager : MonoBehaviour
{
    [SerializeField] public TextAsset cardData;
    [SerializeField] private List<ActionCard> potLuckCards;
    [SerializeField] private List<ActionCard> opportunityKnocksCards;
    [SerializeField] private System.Random random;
    [SerializeField] public Engine gameEngine;
    [SerializeField] public BoardManager board;


    public void Awake()
    {
        Debug.LogError("Card manager awakening...");
        random = new System.Random();
        ParseCardData();
        LogAllCards();
    }
    
    private void ParseCardData()
    {
        cardData = Resources.Load<TextAsset>("card-data");

        if (cardData == null)
        {
            Debug.LogError("Card data is missing!");
            return;
        }

        potLuckCards = new List<ActionCard>();
        opportunityKnocksCards = new List<ActionCard>();

        try
        {
            var jsonObject = JObject.Parse(cardData.text);
            
            foreach (var prop in jsonObject.Properties())
            {
                int cardNum = int.Parse(prop.Name);
                JObject cardInfo = (JObject)prop.Value;
                
                ActionCard card = new ActionCard
                {
                    cardNum = cardNum,
                    description = cardInfo["Description"].ToString(),
                    choice = (bool)cardInfo["Choice"],
                    type = cardInfo["Type"].ToString()
                };
                
                // Create the first action
                card.action1 = new Action();
                
                // Handle the Action property which can be complex
                JToken actionToken = cardInfo["Action"];
                
                if (card.choice || (actionToken.Type == JTokenType.Object && actionToken["Action1"] != null))
                {
                    // Card has a choice or multiple actions
                    JToken action1Token = actionToken["Action1"];
                    JToken action2Token = actionToken["Action2"];
                    
                    // Parse first action
                    string actionType1 = action1Token["Type"].ToString();
                    card.action1.action = (Action.Actions)Enum.Parse(typeof(Action.Actions), actionType1, true);
                    
                    if (action1Token["Amount"] != null)
                        card.action1.amount = (int)action1Token["Amount"];
                    
                    if (action1Token["Property"] != null)
                        card.action1.property = action1Token["Property"].ToString();
                    
                    if (action1Token["Spaces"] != null)
                        card.action1.spaces = (int)action1Token["Spaces"];
                    
                    // Parse second action if it exists
                    if (action2Token != null)
                    {
                        card.action2 = new Action();
                        string actionType2 = action2Token["Type"].ToString();
                        card.action2.action = (Action.Actions)Enum.Parse(typeof(Action.Actions), actionType2, true);
                        
                        if (action2Token["Amount"] != null)
                            card.action2.amount = (int)action2Token["Amount"];
                        
                        if (action2Token["Property"] != null)
                            card.action2.property = action2Token["Property"].ToString();
                        
                        if (action2Token["Spaces"] != null)
                            card.action2.spaces = (int)action2Token["Spaces"];
                    }
                }
                else
                {
                    // Card has a single action
                    string actionType = actionToken["Type"].ToString();
                    card.action1.action = (Action.Actions)Enum.Parse(typeof(Action.Actions), actionType, true);
                    
                    if (actionToken["Amount"] != null)
                        card.action1.amount = (int)actionToken["Amount"];
                    
                    if (actionToken["Property"] != null)
                        card.action1.property = actionToken["Property"].ToString();
                    
                    if (actionToken["Spaces"] != null)
                        card.action1.spaces = (int)actionToken["Spaces"];
                }
                
                // Add the card to the appropriate deck
                if (card.type == "Pot Luck")
                    potLuckCards.Add(card);
                else if (card.type == "Opportunity Knocks")
                    opportunityKnocksCards.Add(card);
            }
            
            // Shuffle the decks initially
            Shuffle();
            Debug.Log($"Successfully loaded {potLuckCards.Count} Pot Luck cards and {opportunityKnocksCards.Count} Opportunity Knocks cards");
        }
        catch (Exception e)
        {
            Debug.LogError($"Error parsing card data: {e.Message}");
        }
    }

    public ActionCard DrawOpportunityKnocks()
    {
        if (opportunityKnocksCards.Count == 0)
        {
            Debug.LogWarning("No Opportunity Knocks cards available.");
            return null;
        }

        // Draw the top card
        ActionCard drawnCard = opportunityKnocksCards[0];

        // Remove it from the top and add it to the bottom
        opportunityKnocksCards.RemoveAt(0);
        opportunityKnocksCards.Add(drawnCard);

        // Log what has been drawn
        Debug.Log($"Drew Opportunity Knocks card: {drawnCard.description}");

        // Execute the card's action(s)
        ExecuteCardActions(drawnCard);

        // Return the drawn card
        return drawnCard;
    }

    public ActionCard DrawPotLuck()
    {
        if (potLuckCards.Count == 0)
        {
            Debug.LogWarning("No Pot Luck cards available.");
            return null;
        }

        // Draw the top card
        ActionCard drawnCard = potLuckCards[0];

        // Remove it from the top and add it to the bottom
        potLuckCards.RemoveAt(0);
        potLuckCards.Add(drawnCard);

        // Log what has been drawn
        Debug.Log($"Drew Pot Luck card: {drawnCard.description}");

        // Execute the card's action(s)
        ExecuteCardActions(drawnCard);

        // Return the drawn card
        return drawnCard;
    }

    private void ExecuteCardActions(ActionCard card)
    {
        if (card.choice)
        {
            // If a card has a choice, the player will be required to choose between the two
            Debug.Log($"Card requires a choice between two actions: {card.description}");
            // After player selects an action, we call ExecuteAction on either action1 or action2
        }
        else
        {
            // Execute the first action
            ExecuteAction(card.action1);
            // If there's a second action, execute it
            if (card.action2 != null)
            {
                ExecuteAction(card.action2);
            }
        }
    }

    private void ExecuteAction(Action action)
    {
        switch (action.action)
        {
            case Action.Actions.pay_player:
                Debug.Log($"Pay player {action.amount}");
                gameEngine.currentPlayer.addMoney(action.amount);
                break;
                
            case Action.Actions.pay_bank:
                Debug.Log($"Player pays bank {action.amount}");
                gameEngine.currentPlayer.takeMoney(action.amount);
                break;
                
            case Action.Actions.move:
                Debug.Log($"Move player to {action.property}");
                
                Tile targetTile = null;

                foreach (Tile tile in board.tiles)
                {
                    if (tile.GetName() == action.property)
                    {
                        targetTile = tile;
                        break;
                    }
                }
                
                if (targetTile != null)
                {
                    Tile currentTile = gameEngine.currentPlayer.getCurrentTile();
                    int currentTileIndex = board.tiles.IndexOf(currentTile);
                    int targetTileIndex = board.tiles.IndexOf(targetTile);

                    if (targetTileIndex < currentTileIndex)
                    {
                        gameEngine.passGo();
                    }

                    gameEngine.currentPlayer.setCurrentTile(targetTile);

                    gameEngine.CheckForActionEvent(gameEngine.currentPlayer);
                }
                else
                {
                Debug.LogError($"Could not find property with name: {action.property}");
                }
                break;
            
                
            case Action.Actions.opportunity_knocks:
                Debug.Log("Draw an Opportunity Knocks card");
                DrawOpportunityKnocks();
                break;
                
            case Action.Actions.put_free_parking:
                Debug.Log($"Put {action.amount} in free parking");
                
                gameEngine.parkingFines += action.amount;
                gameEngine.currentPlayer.takeMoney(action.amount);
                break;
                
            case Action.Actions.jail:
                Debug.Log("Send player to jail");
                if (gameEngine.currentPlayer.hasGOOJ)
                {
                    Debug.Log("Player used Get Out of Jail Free card");
                    gameEngine.currentPlayer.hasGOOJ = false;
                }
                else
                {
                    gameEngine.GoToJail();
                }
                break;
                
            case Action.Actions.receive_player:
                Debug.Log($"Receive {action.amount} from each player");
                foreach (Player player in gameEngine.players)
                {
                    if (player != gameEngine.currentPlayer)
                    {
                        player.takeMoney(action.amount);
                        gameEngine.currentPlayer.addMoney(action.amount);
                    }
                }
                break;
                
            case Action.Actions.avoid_jail:
                Debug.Log("Player gets a Get Out of Jail Free card");
                gameEngine.currentPlayer.hasGOOJ = true;
                break;
                
            case Action.Actions.pay_bank_per_house:
                Debug.Log($"Pay {action.amount} per house");
                int houseCount = 0;
                
                // Count all houses owned by the player
                foreach (Property property in gameEngine.currentPlayer.GetProperties())
                {
                    if (property.GetHouseCount() > 0 && property.GetHouseCount() < 5)
                    {
                        houseCount += property.GetHouseCount();
                    }
                }
                
                int totalPayment = houseCount * action.amount;
                Debug.Log($"Player pays {totalPayment} for {houseCount} houses");
                gameEngine.currentPlayer.takeMoney(totalPayment);
                break;
                
            case Action.Actions.pay_bank_per_hotel:
                Debug.Log($"Pay {action.amount} per hotel");
                int hotelCount = 0;
                
                // Count all hotels owned by the player (assuming 5 houses = 1 hotel)
                foreach (Property property in gameEngine.currentPlayer.GetProperties())
                {
                    if (property.GetHouseCount() == 5)
                    {
                        hotelCount++;
                    }
                }
                
                int totalHotelPayment = hotelCount * action.amount;
                Debug.Log($"Player pays {totalHotelPayment} for {hotelCount} hotels");
                gameEngine.currentPlayer.takeMoney(totalHotelPayment);
                break;
                
            case Action.Actions.move_back:
                Debug.Log($"Move back {action.spaces} spaces");
            
                // Find current tile index
                Tile playerTile = gameEngine.currentPlayer.getCurrentTile();
                int playerTileIndex = board.tiles.IndexOf(playerTile);
                
                // Calculate target index
                int targetIndex = (playerTileIndex - action.spaces + board.TotalTiles) % board.TotalTiles;
                Tile backwardsTile = board.tiles[targetIndex];
                
                // Move the player to the target tile
                gameEngine.currentPlayer.setCurrentTile(backwardsTile);
                gameEngine.currentPlayer.transform.position = backwardsTile.transform.position;
                
                // Check for action on the new tile
                gameEngine.CheckForActionEvent(gameEngine.currentPlayer);
                break;
                
            default:
                Debug.LogWarning($"Unknown action type: {action.action}");
                break;
        }
    }

    public void ShuffleDeck(List<ActionCard> deck)
    {
        int n = deck.Count;

        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            ActionCard value = deck[k];
            deck[k] = deck[n];
            deck[n] = value;
        }
    }

    public void Shuffle()
    {
        ShuffleDeck(potLuckCards);
        ShuffleDeck(opportunityKnocksCards);
        Debug.Log("Shuffled both card decks");
    }

    public void LogAllCards()
    {
        // Shows all of the cards in both decks and logs them
        Debug.Log("--- Pot Luck Cards ---");
        foreach (var card in potLuckCards)
        {
            Debug.Log($"#{card.cardNum}: {card.description}");
        }

        Debug.Log("--- Opportunity Knocks Cards ---");
        foreach (var card in opportunityKnocksCards)
        {
            Debug.Log($"#{card.cardNum}: {card.description}");
        }
    }
}