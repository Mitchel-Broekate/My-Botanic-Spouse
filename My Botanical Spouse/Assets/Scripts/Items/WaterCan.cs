using System.Collections;
using UnityEngine;

public class WaterCan : MonoBehaviour
{
    #region Vars
    [Header("Tilt Settings")]
    [SerializeField] Transform _pourPoint;
    [SerializeField] float _pourAngleThreshold = 45f;

    [Header("Water Settings")]
    [SerializeField] float _maxWater = 100f;
    [SerializeField] float _currentWater = 100f;
    [SerializeField] float _pourRate = 10f;

    [Header("Plant Conditions")]
    [SerializeField] float _pourCheckDistance = 0.5f;
    [SerializeField] LayerMask _plantLayer;

    [Header("Player Conditions")]
    [SerializeField] float _mpCooldownTime;

    [Header("Pour State")]
    [SerializeField] bool _isPouring = false;

    GameManager _gameManager;
    ItemStats _itemStats;
    bool _canAddMP = true;
    #endregion

    /// <summary>
    /// Links the Game Manager
    /// </summary>
    void Start()
    {
        //Add the Game Manager object here
        _gameManager = GameObject.Find("GameManager(PlayerManager)").GetComponent<GameManager>();
        _itemStats = GetComponent<ItemStats>();
    }

    /// <summary>
    /// Constantly checks if the watering can can pour or not
    /// </summary>
    void Update()
    {
        CheckPourCondition();
    }

    /// <summary>
    /// Checks if the watering can is tilted and activates/deactivates the pour
    /// </summary>
    void CheckPourCondition()
    {
        float tiltAngle = Vector3.Angle(transform.up, Vector3.up);

        bool tiltedEnough = tiltAngle > _pourAngleThreshold;
        bool hasWater = _currentWater > 0;

        bool plantBelow = Physics.Raycast(_pourPoint.position, Vector3.down, out RaycastHit hit, _pourCheckDistance, _plantLayer);

        if (tiltedEnough && hasWater && plantBelow)
        {
            StartPouring(hit.collider);
        }
        else
        {
            StopPouring();
        }
    }

    /// <summary>
    /// Applies the pour conditions to the plant and watering can stats
    /// </summary>
    /// <param name="plant"></param>
    void StartPouring(Collider plant)
    {
        if (!_isPouring)
        {
            _isPouring = true;
            // start particles
        }

        DrainWater();

        PlantManager plantScript = plant.transform.parent.GetComponent<PlantManager>();
        if (plantScript != null)
        {
            plantScript.ChangePlantStats("Thirst", -_pourRate * Time.deltaTime);

            if(_canAddMP)
            {
               _gameManager.PlayerMotivationPoints += _itemStats.GetMotivationPoints;
               StartCoroutine(AddMPCooldown(_mpCooldownTime)); 
            }
        }
    }

    /// <summary>
    /// Stops the pouring visuals
    /// </summary>
    void StopPouring()
    {
        if (_isPouring)
        {
            _isPouring = false;
            // Stop particles
        }
    }

    /// <summary>
    /// Drains the watering can's water so it runs out after a while
    /// </summary>
    void DrainWater()
    {
        _currentWater -= _pourRate * Time.deltaTime;
        _currentWater = Mathf.Clamp(_currentWater, 0, _maxWater);
    }
    
    /// <summary>
    /// Disables the AddMP Function for a given duration
    /// </summary>
    /// <param name="duration"></param>
    /// <returns></returns>
    IEnumerator AddMPCooldown(float duration)
    {
        _canAddMP = false;
        yield return new WaitForSeconds(duration);
        _canAddMP = true;
    }
}
