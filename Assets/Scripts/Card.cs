using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Card : MonoBehaviour
{
    [SerializeField] public int cardNum;
    [SerializeField] public string cardType;
    [SerializeField] public bool choice;
    [SerializeField] public string description;
    [SerializeField] public Action action;

    /*
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
    */

    public Card (int cardNum, string cardType, bool choice, string description, Action action)
    {
        this.cardNum = cardNum;
        this.cardType = cardType;
        this.choice = choice;
        this.description = description;
        this.action = action;
    }


}