using System;
using System.Collections;
using UnityEngine;

public class WaterCan : MonoBehaviour
{
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
    [SerializeField] int mpAmount;
    [SerializeField] float mpCooldownTime;

    [Header("Pour State")]
    [SerializeField] bool _isPouring = false;

    GameManager gameManager;
    bool canAddMP = true;

    /// <summary>
    /// Links the Game Manager
    /// </summary>
    void Start()
    {
        //Add the Game Manager object here
        gameManager = GameObject.Find("GameManager(PlayerManager)").GetComponent<GameManager>();
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
            // You could start a particle system here
        }

        DrainWater();

        PlantManager plantScript = plant.transform.parent.GetComponent<PlantManager>();
        if (plantScript != null)
        {
            plantScript.ChangePlantStats("Thirst", -_pourRate * Time.deltaTime);
            //start coroutine (add money cooldown)
            if(canAddMP)
            {
               gameManager.PlayerMotivationPoints += mpAmount;
               StartCoroutine(AddMPCooldown(mpCooldownTime)); 
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

    IEnumerator AddMPCooldown(float duration)
    {
        canAddMP = false;
        yield return new WaitForSeconds(duration);
        canAddMP = true;
    }
}
