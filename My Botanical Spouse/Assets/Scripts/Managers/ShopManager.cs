using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    #region Vars
    [SerializeField] GameManager gameManager;
    [SerializeField] Transform shopItemSpawnpoint;
    [SerializeField] List<GameObject> itemObjects = new();
    GameObject itemToBuy;
    int currentMP;
    int itemCost;
    #endregion

    #region Buying process
    void CheckCanBuyItem()
    {
        //get player currency
        currentMP = gameManager.PlayerMotivationPoints;
        //get item value
        itemCost = itemToBuy.GetComponent<ItemStats>().GetItemCost;
        //compare and see if player can buy the item
        //buy item void or give message that player can't buy the item
        if(itemCost <= currentMP)
        {
            BuyItem();    
        }
        else
        {
            //give that message
            Debug.Log("Player doesn't have enough MP");
        }
    }
    
    void BuyItem()
    {
        //remove item value amount from player currency
        gameManager.PlayerMotivationPoints -= itemCost;
        //get item to spawn
        Instantiate(itemToBuy, shopItemSpawnpoint.position, quaternion.identity);
        //spawn item at spawnpoint

    }
    #endregion

    /// <summary>
    /// This function connects to a button to buy an item (Valid items: Watering Can, Water Cooler, Water Cooler Upgrade, Soil, Soil Upgrade, Medicine, Medicine Upgrade)
    /// </summary>
    /// <param name="item"></param>
    public void BuyItem(string item)
    {
        switch(item)
        {
            case"Watering Can":
                itemToBuy = itemObjects.Find(obj => obj.name == "WateringCan");
                //check if can buy
                CheckCanBuyItem();
                //spawn object
                //give object stat and effect amount
                Debug.Log("bought Item");
                break;
            case"Water Cooler":
                itemToBuy = itemObjects.Find(obj => obj.name == "WaterCooler");

                CheckCanBuyItem();
                break;
            case"Water Cooler Upgrade":
                itemToBuy = itemObjects.Find(obj => obj.name == "WaterCoolerUpgrade");

                CheckCanBuyItem();
                break;
            case"Soil":
                itemToBuy = itemObjects.Find(obj => obj.name == "Soil");

                CheckCanBuyItem();
                break;
            case"Soil Upgrade":
                itemToBuy = itemObjects.Find(obj => obj.name == "SoilUpgrade");

                CheckCanBuyItem();
                break;
            case"Medicine":
                itemToBuy = itemObjects.Find(obj => obj.name == "Medicine");

                CheckCanBuyItem();
                break;
            case"Medicine Upgrade":
                itemToBuy = itemObjects.Find(obj => obj.name == "MedicineUpgrade");

                CheckCanBuyItem();
                break;
            default:
                Debug.LogWarning("Not a valid Item name");
                break;
        }
    }
}
