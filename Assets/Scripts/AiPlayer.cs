//using System.Collections.Generic;
//using UnityEngine;
//using System.Linq;

//public class AiPlayer : Player
//{
//    private Player aiPlayer;
//    private string state;
//    private float currentPrice;
//    [SerializeField] public Engine gameEngine;

//    void Start()
//    {
//        aiPlayer = new Player();
//        gameEngine = FindObjectOfType<Engine>();
//    }

//    public void takeTurn()
//    {
//        switch (state)
//        {
//            case "jail":
//                if (aiPlayer.getMoney() > 200)
//                {
//                    pressGOJ();
//                }
//                else
//                {
//                    pressRollDice();
//                }
//                break;

//            case "auction":
//                string propset = "someGroup"; // Replace with actual group name/logic
//                if (ownedproperties.Any(p => p.GetGroup() == propset))
//                {
//                    if (money > 2.5f * currentPrice)
//                    {
//                        bid();
//                    }
//                }
//                break;

//            default:
//                pressRollDice();

//                Property property = aiPlayer.getCurrentTile().GetComponent<Property>();
//                if (property != null)
//                {
//                    if (property.GetOwner() == null)
//                    {
//                        propset = "someGroup"; // Replace with property.set or similar logic
//                        if (property.GetGroup() == propset)
//                        {
//                            if (money > 2.5f * property.GetPrice())
//                            {
//                                buyproperty();
//                            }
//                        }
//                        else if (money > 2.5f)
//                        {
//                            buyproperty();
//                        }
//                    }
//                    else
//                    {
//                        payrent();
//                    }
//                }

//                if (aiPlayer.getCurrentTile().GetComponent<ActionSpace>() != null)
//                {
//                    break;
//                }

//                foreach (var prop in ownedproperties)
//                {
//                    if (money > 2.0f * prop.GetHouseCost())
//                    {
//                        prop.UpgradeProperty();
//                    }
//                }
//                break;
//        }
//    }

//    // Empty method stubs
//    private void pressGOJ() {
//        gameEngine.PayBail();
//    }

//    private void pressRollDice() {
//        gameEngine.rollDice();
//    }

//    private void bid() {
//        return;
//    }

//    private void buyproperty() {
//        gameEngine.purchasePropertyAI();
//    }

//    private void payrent() {
//        return;
//        //engine handles this automatically 
//    }
//}
