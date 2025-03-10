/*
using UnityEngine;

public class ActionSpace : MonoBehaviour
{
    // Enum to store the different types of actions that you can land on
    public enum ActionType {
        Go,
        GoToJail,
        Jail,
        FreeParking,
        IncomeTax,
        SuperTax,
        PotLuck,
        OpportunityKnocks
    }

    [SerializeField] private ActionType actionType;

    
    //Returns the action type.
    
    public ActionType GetActionType()
    {
        return actionType;
    }

    
    //Handles the action type for the space that you land on.
    
    public void LandedOn(Player player)
    {
        switch (actionType)
        {
            case ActionType.Go:
            // Implements game engine go method
                Engine.passGo(player);
                break;
            
            case ActionType.GoToJail:
            // Checks if user has GOOJ, otherwise jails them
                if(player.hasGOOJ == false){GoToJail(player);}
                break;

            case ActionType.Jail:
            // Nothing, allows you to pass if not in jail
                if(player.inJail == true){EscapeJail(player);}
                break;

            case ActionType.FreeParking:
            // Nothing, collect fines
                CollectFines(player);
                break;

            case ActionType.IncomeTax:
            // Simple mathematical deduction
            // If they can't pay, they get put into bankrupcy
                player.takeMoney(20);
                break;

            case ActionType.SuperTax:
            // Simple mathematical deduction
            // If they can't pay, they get put into bankrupcy
                player.takeMoney(100);
                break;

            case ActionType.PotLuck:
            // Use Action Card Class
                break;

            case ActionType.OpportunityKnocks:
            // Use Action Card Class
                break;

        }
    }

    public void GoToJail(Player player){
       // player.setCurrentTile(*jails tile ID*)
       print("send player to jail");
    }

    public void EscapeJail(Player player){
        //dice rolls no longer move the player and instead they must roll a double to escape 
    }

    public void CollectFines(Player player){
        //display message to player about how much they have gained
        player.addMoney(Engine.CollectFines());
    }

    public void setActionType(ActionType type){
        actionType = type;
    }
}
*/
