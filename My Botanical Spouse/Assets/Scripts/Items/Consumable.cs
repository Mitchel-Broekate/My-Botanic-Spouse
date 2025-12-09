using UnityEngine;

public class Consumable : MonoBehaviour
{
    #region Vars
    string _statEffect;
    float _effectAmount;
    GameManager gameManager;
    ItemStats itemStats;
    #endregion

    /// <summary>
    /// Gets the ItemStat and GameManager script
    /// </summary>
    void Start()
    {
        itemStats = GetComponent<ItemStats>();
        gameManager = GameObject.Find("GameManager(PlayerManager)").GetComponent<GameManager>();
        
        if(gameManager == null)
        {
            Debug.LogWarning("Game Manager not found");
        }
    }

    /// <summary>
    /// Checks if the consumable touched the plant to execute the given effects
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.GetMask("Plant"))
        {
            _statEffect = itemStats.StatEffect;
            _effectAmount = itemStats.EffectAmount;

            if (_statEffect == null || _effectAmount == 0) 
            {
                Debug.LogWarning("effects not given");
                return;
            }

            other.gameObject.GetComponent<PlantManager>().ChangePlantStats(_statEffect, _effectAmount);
            gameManager.PlayerMotivationPoints += itemStats.GetMotivationPoints;
            //Create effects
            Destroy(gameObject);
        }
    }


}
