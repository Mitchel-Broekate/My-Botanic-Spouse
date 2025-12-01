using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    #region Vars
    [SerializeField] List<ScriptableObject> itemScriptableObjects = new();
    [SerializeField] int _itemLevel;
    #endregion

    //Check if player has moent void (do remove dough or give message "you broke")
    //Remove player money void
    //Button function to spawn corresponding item



    public int changeItemLevel
    {
        set
        {
            _itemLevel = value;
        }
    }
    /// <summary>
    /// This function connects to a button to buy an item (Valid items: Watering Can, Soil. Medicine)
    /// </summary>
    /// <param name="item"></param>
    public void BuyItem(string item)
    {
        ScriptableObject itemSO;

        switch(item)
        {
            case"Watering Can":
                itemSO = itemScriptableObjects.Find(obj => obj.name == "WateringCan" + _itemLevel.ToString());
                //check if can buy
                //spawn object
                //give object stat and effect amount
                Debug.Log("bought Item");
                break;
            case"Soil":
                itemSO = itemScriptableObjects.Find(obj => obj.name == "Soil" + _itemLevel.ToString());
                break;
            case"Medicine":
                itemSO = itemScriptableObjects.Find(obj => obj.name == "Medicine" + _itemLevel.ToString());
                break;
            default:
                Debug.LogWarning("");
                break;
        }
    }

}
