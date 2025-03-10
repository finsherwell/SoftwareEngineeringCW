using UnityEngine;

public class ActionSpace : MonoBehaviour
{
    public Engine gameEngine;
    private int finesTotal; 

    public enum ActionType
    {
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

    public ActionType GetActionType()
    {
        return actionType;
    }

    public void LandedOn(Player player)
    {
        switch (actionType)
        {
            case ActionType.Go:
                gameEngine.passGo(player);
                break;

            case ActionType.GoToJail:
                gameEngine.GoToJail();
                break;

            case ActionType.Jail:
                // Nothing happens unless player is sent to jail
                break;

            case ActionType.FreeParking:
                CollectFines(player);
                break;

            case ActionType.IncomeTax:
                player.takeMoney(100);
                addtoFinesTotal(100);
                break;

            case ActionType.SuperTax:
                player.takeMoney(200);
                addtoFinesTotal(200);
                break;

            case ActionType.PotLuck:
                // Draw Pot Luck card
                break;

            case ActionType.OpportunityKnocks:
                // Draw Opportunity Knocks card
                break;
        }
    }

    public void GoToJail(Player player)
    {
        // player.setCurrentTile(/*jail tile ID*/);
        print("Send player to jail");
    }

    public void EscapeJail(Player player)
    {
        // Implement logic for escaping jail
    }

    public void CollectFines(Player player)
    {
        // Display message about fines collected
        player.addMoney(gameEngine.CollectFines());
    }

    public void SetActionType(ActionType type)
    {
        actionType = type;
    }
    public void addtoFinesTotal(int fine)
    {
        finesTotal = +fine; 
    }
    public int getFinesTotal() {
        return finesTotal; 
    }
}
