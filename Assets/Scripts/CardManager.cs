using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System;

[System.Serializable]
public class ActionData
{
    public string actionDescription;
    public Action action1;
    public Action action2 = null;
    public bool actionChoice;
    public string type;
}


[System.Serializable]
public class ActionCard
{
    public int cardNum;
    public string cardType;
    public bool choice;
    public string description;
    public Action[] actionChoices;

    public enum Action {
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
                    // If the card is Pot Luck
                    if (type == "Pot Luck")
                    {

                    }
                    // If the card is Opportunity Knocks
                    else
                    {
                        
                    }
                }
            }
        }
    }

    private ActionData.Action ParseAction()
    {
        /*
        return new ActionData.Action
        {
            
        }
        */
    }

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
}