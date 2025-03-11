using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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
}

[System.Serializable]
public class ActionData
{
    // Stores the description of the action.
    public string actionDescription;

    // Stores the first action, which will always exist.
    public Action action1;

    // Stores the second action, which is only necessary when there is more than one action, it is null by default.
    public Action action2 = null;

    // Stores whether there is a choice between actions, in scenarios where there are 2 actions.
    // False by default as most actions do not have a choice.
    public bool actionChoice = false;

    // Stores the type of the card, Pot Luck or Opportunity Knocks.
    public string type;
}


[System.Serializable]
public class ActionCard
{
    // Stores the number of the card, which is what it is indexed by in the card-data.json file.
    // Will be used when displaying the card on the screen to the user.
    public int cardNum;

    // Stores the action data of the card.
    public ActionData actionData;
}

public class CardManager : MonoBehaviour
{
    public TextAsset cardData;
    private List<ActionCard> potLuckCards;
    private List<ActionCard> opportunityKnocksCards;
    private void Start()
    {
        if (cardData != null)
        {
            var cardDict = JsonConvert.DeserializeObject<Dictionary<string, ActionData>>(cardData.text);
            potLuckCards = new List<ActionCard>();
            opportunityKnocksCards = new List<ActionCard>();

            foreach (var entry in cardDict)
            {
                ActionData card = entry.Value;
                bool hasChoice = card.actionChoice;
                string type = card.type;

                ActionCard newCard;

                if (hasChoice)
                {
                    
                }
            }
        }
    }

    /*
    private ActionData.Action ParseAction()
    {
        return new ActionData.Action
        {
            
        }
    }
    */

    /*
    public ActionCard DrawOpportunityKnocks()
    {
        // draw card, read it, return to bottom
        //return opportunityKnocksCards.
    }

    public ActionCard DrawPotLuck()
    {
        // draw card, read it and store
        // return to bottom when used
        //return potLuckCards.
    }

    public void Shuffle()
    {
        var rnd = new Random();
        var rnd2 = new Random();
        potLuckCards.OrderBy(item => rnd.Next());
        opportunityKnocksCards.OrderBy(item => rnd2.Next());
    }
    */
}