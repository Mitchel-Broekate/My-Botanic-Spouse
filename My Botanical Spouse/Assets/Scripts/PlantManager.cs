using System.Collections;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    #region Vars
    [Header("Stat Decrease Rate")]
    [SerializeField] float _decreaseRateAffection;
    [SerializeField] float _decreaseRateSoilQuality;
    [SerializeField] float _decreaseRateThirst;
    [SerializeField] float _decreaseRateWarmth;

    [Header("Stat Max Config")]
    [SerializeField] float _plantMaxHealth;
    [SerializeField] float _plantMaxAffection;
    [SerializeField] float _plantMaxSoilQuality;
    [SerializeField] float _plantMaxThirst;
    [SerializeField] float _plantMaxWarmth;
    [SerializeField] float _plantMinWarmth;

    [Header("Current Stat Values")]
    [SerializeField] float _plantHealth;
    [SerializeField] float _plantAffection;
    [SerializeField] float _plantSoilQuality;
    [SerializeField] float _plantThirst;
    [SerializeField] float _plantWarmth;
    [SerializeField] bool _heaterState;

    Coroutine _decreaseStats;
    #endregion

    /// <summary>
    /// Will update the plant stats (Decrease all stats over time)
    /// </summary>
    public void SetPlantState(bool gameState)
    {
        //if the game has started, change all stats at their rate; If it ends stop the stat change
        if (gameState)
        {
            _decreaseStats = StartCoroutine(DecreaseStats());
        }
        else
        {
            if (_decreaseStats == null) return;
            StopCoroutine(_decreaseStats);
        }
    }

    IEnumerator DecreaseStats()
    {
        while (_plantHealth > 0)
        {
            float dt = Time.deltaTime;

            ChangePlant("Affection", _decreaseRateAffection * dt);
            ChangePlant("SoilQuality", _decreaseRateSoilQuality * dt);
            ChangePlant("Thirst", _decreaseRateThirst * dt);
            ChangePlantWarmth(_decreaseRateWarmth * dt);

            yield return null;
        }
    }

    #region Change Stat floats
    /// <summary>
    /// This float changes the Health of the plant based on the given information (changeTypes: Health, Affection, SoilQuality, Thirst)
    /// </summary>
    /// <param name="changeType"></param>
    /// <param name="healthchange"></param>
    /// <returns></returns>
    public float ChangePlant(string changeType, float changeAmount)
    {
        switch (changeType)
        {
            case "Health":
                if (_plantHealth <= 0)
                {
                    _plantHealth = 0;
                    //kill plant void

                    Debug.Log("You killed the plant");
                }
                else
                {
                    _plantHealth -= changeAmount;
                }
                break;
            case "Affection":
                if (_plantAffection < 0)
                {
                    _plantAffection = 0;

                    ChangePlant("Health", 7.5f);
                }
                else
                {
                    _plantAffection -= changeAmount;
                }
                break;
            case "SoilQuality":
                if (_plantSoilQuality < 0)
                {
                    _plantSoilQuality = 0;

                    ChangePlant("Health", 7.5f);
                }
                else
                {
                    _plantSoilQuality -= changeAmount;
                }
                break;
            case "Thirst":
                if (_plantThirst < 0)
                {
                    _plantThirst = 0;

                    ChangePlant("Health", 7.5f);
                }
                else
                {
                    _plantThirst -= changeAmount;
                }
                break;
            default:
                Debug.LogWarning(changeType + " Is not a valid option");
                break;

        }

        return _plantHealth;
    }

    /// <summary>
    /// This float changes the Warmth of the plant based on the given information (change types: "plus", "min")
    /// </summary>
    /// <param name="changeType"></param>
    /// <param name="warmthChange"></param>
    /// <returns></returns>
    float ChangePlantWarmth(float changeAmount)
    {
        if (_heaterState)
        {
            _plantWarmth += changeAmount;
            _plantWarmth = Mathf.Clamp(_plantWarmth, _plantMaxWarmth, _plantMinWarmth);
            if (_plantWarmth == _plantMaxWarmth)
            {
                //Execute plant health damage over time
                ChangePlant("Health", 7.5f);
            }
        }
        else
        {
            _plantWarmth -= changeAmount;
            _plantWarmth = Mathf.Clamp(_plantWarmth, _plantMaxWarmth, _plantMinWarmth);
            if (_plantWarmth == _plantMaxWarmth)
            {
                //Execute plant health damage over time
                ChangePlant("Health", 7.5f);
            }
        }

        return _plantWarmth;
    }
    
    public bool _HeaterState(bool newState)
    {
        _heaterState = newState;
        return _heaterState;
    }
    #endregion
}