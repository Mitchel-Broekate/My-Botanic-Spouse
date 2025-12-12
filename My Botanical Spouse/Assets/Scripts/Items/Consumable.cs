using UnityEngine;

public class Consumable : MonoBehaviour
{
    #region Vars
    [SerializeField] LayerMask _layerMask;
    string _statEffect;
    float _effectAmount;
    GameManager _gameManager;
    ItemStats _itemStats;
    #endregion

    /// <summary>
    /// Gets the ItemStat and GameManager script
    /// </summary>
    void Start()
    {
        _itemStats = GetComponent<ItemStats>();
        _gameManager = GameObject.Find("GameManager(PlayerManager)").GetComponent<GameManager>();
        
        if(_gameManager == null)
        {
            Debug.LogWarning("Game Manager not found");
        }

        _layerMask = LayerMask.GetMask("Plant");
    }

    /// <summary>
    /// Checks if the consumable touched the plant to execute the given effects
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == _layerMask)
        {

            Debug.Log("Collided with plant");

            _statEffect = _itemStats.StatEffect;
            _effectAmount = _itemStats.EffectAmount;

            if (_statEffect == null || _effectAmount == 0) 
            {
                Debug.LogWarning("Item effects not given");
                return;
            }

            collision.transform.parent.GetComponent<PlantManager>().ChangePlantStats(_statEffect, _effectAmount);
            _gameManager.PlayerMotivationPoints += _itemStats.GetMotivationPoints;

            Debug.Log("Gave effects");

            //Create effects
            Destroy(gameObject, 0.3f);
        }
    }


}
