using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantManager : MonoBehaviour
{
    #region Vars
    [Header("Stat Decrease Rate")]
    [SerializeField] float _decreaseRateAffection;
    [SerializeField] float _decreaseRateSoilQuality;
    [SerializeField] float _decreaseRateThirst;
    [SerializeField] float _changeRateWarmth;

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

    [Header("Stat States")]
    [SerializeField] bool _heaterState;
    [SerializeField] bool _allowGivingAffection = false;

    [Header("Stat Bars")]
    [SerializeField] GameObject statBarParent;
    [SerializeField] List<Slider> statBars = new();
    [SerializeField] float statBarUpdateDelayTime;
    Coroutine _decreaseStats;
    Coroutine _updateStatBars;
    #endregion

    /// <summary>
    /// This function will set the stat starting stat values
    /// </summary>
    void Start()
    {
        _plantHealth = _plantMaxHealth;
        _plantAffection = _plantMaxAffection;
        _plantSoilQuality = _plantMaxSoilQuality;
        _plantThirst = _plantMaxThirst;
        _plantWarmth = (_plantMaxWarmth + _plantMinWarmth) / 2; 
    }

    /// <summary>
    /// At the start of the level this function will activate the plant stat decrease (Enable/Disable in the GameManager)
    /// </summary>
    public void SetPlantState(bool gameState)
    {
        //if the game has started, change all stats at their rate; If it ends stop the stat change
        if (gameState)
        {
            foreach (Slider statBar in statBars)
            {
                if (statBar.name == "Warmth")
                {
                    statBar.minValue = GetPlantstat("Min", statBar.name.ToString());
                }
                statBar.maxValue = GetPlantstat("Max", statBar.name.ToString());
            }

            statBarParent.SetActive(true);
            _decreaseStats = StartCoroutine(DecreaseStats());
            _updateStatBars = StartCoroutine(StatBarUpdate(statBarUpdateDelayTime));
        }
        else
        {
            if (_decreaseStats == null && _updateStatBars == null) return;
            StopCoroutine(_decreaseStats);
            StopCoroutine(_updateStatBars);
            statBarParent.SetActive(false);
        }
    }

    /// <summary>
    /// Decreases the plant stats over time (Enables the gameplay)
    /// </summary>
    /// <returns></returns>
    IEnumerator DecreaseStats()
    {
        while (_plantHealth > 0)
        {
            float dt = Time.deltaTime;

            ChangePlantStats("Affection", _decreaseRateAffection * dt);
            ChangePlantStats("SoilQuality", _decreaseRateSoilQuality * dt);
            ChangePlantStats("Thirst", _decreaseRateThirst * dt);
            ChangePlantStats("Warmth", _changeRateWarmth * dt);

            if(_plantAffection < (_plantMaxAffection / 10) * 7 )
            {
                _allowGivingAffection = true;
            }
            else
            {
                _allowGivingAffection = false;
            }

            yield return null;
        }
    }
    IEnumerator StatBarUpdate(float delayTime)
    {
        while (_plantHealth > 0)
        {
            foreach(Slider statBar in statBars)
            {
                statBar.value = GetPlantstat("Current", statBar.name.ToString());
            }
            
            yield return new WaitForSeconds(delayTime);
        }

        yield return null;
    }

    #region Change Stat Vars
    /// <summary>
    /// This float changes the Health of the plant based on the given information (change amounts: minus = plus amount, no added operators = minus amount. changeTypes: Health, Affection, SoilQuality, Thirst)
    /// </summary>
    /// <param name="changeType"></param>
    /// <param name="healthchange"></param>
    /// <returns></returns>
    public float ChangePlantStats(string changeType, float changeAmount)
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
                if (_plantAffection <= 0)
                {
                    _plantAffection = 0;

                    ChangePlantStats("Health", 2f);
                }
                else
                {
                    _plantAffection -= changeAmount;
                }
                break;
            case "SoilQuality":
                if (_plantSoilQuality <= 0)
                {
                    _plantSoilQuality = 0;

                    ChangePlantStats("Health", 2f);
                }
                else
                {
                    _plantSoilQuality -= changeAmount;
                }
                break;
            case "Thirst":
                if (_plantThirst <= 0)
                {
                    _plantThirst = 0;

                    ChangePlantStats("Health", 2f);
                }
                else
                {
                    _plantThirst -= changeAmount;
                }
                break;
            case "Warmth":
                if (_heaterState)
                {
                    if (_plantWarmth >= _plantMaxWarmth)
                    {
                        _plantWarmth = _plantMaxWarmth;
                        ChangePlantStats("Health", 2f);
                    }
                    else
                    {
                        _plantWarmth += changeAmount;
                    }
                }
                else
                {
                    if (_plantWarmth <= _plantMinWarmth)
                    {
                        _plantWarmth = _plantMinWarmth;
                        ChangePlantStats("Health", 2f);
                    }
                    else
                    {
                        _plantWarmth -= changeAmount;
                    }
                }
                break;
            default:
                Debug.LogWarning(changeType + " Is not a valid option");
            break;
        }
        return _plantHealth;
    }
    
    public bool HeaterState(bool newState)
    {
        _heaterState = newState;
        return _heaterState;
    }

    public bool AllowGivingAffection
    {
        get{return _allowGivingAffection;}
    }
    #endregion

    #region Link Stats Slider
    /// <summary>
    /// This function gets the stats and states from the plants. (Valid states: Current, Max, Min(Depending on the stat). Valid stats: Health, Affection, SoilQuality, Thirst, Warmth)
    /// </summary>
    /// <param name="getState"></param>
    /// <returns></returns>
    public float GetPlantstat(string getState, string stat)
    {
        if (stat == "Health")
        {
            if (getState == "Current")
            {
                return _plantHealth;
            }
            else if (getState == "Max")
            {
                return _plantMaxHealth;
            }
            else
            {
                Debug.LogWarning("Not a valid state for stat: " + stat);
                return 0;
            } 
        }
        else if (stat == "Affection")
        {
            if (getState == "Current")
            {
                return _plantAffection;
            }
            else if (getState == "Max")
            {
                return _plantMaxAffection;
            }
            else
            {
                Debug.LogWarning("Not a valid state for stat: " + stat);
                return 0;
            }   
        }
        else if (stat == "Soil Quality")
        {
            if (getState == "Current")
            {
                return _plantSoilQuality;
            }
            else if (getState == "Max")
            {
                return _plantMaxSoilQuality;
            }
            else
            {
                Debug.LogWarning("Not a valid state for stat: " + stat);
                return 0;
            }   
        }
        else if (stat == "Thirst")
        {
            if (getState == "Current")
            {
                return _plantThirst;
            }
            else if (getState == "Max")
            {
                return _plantMaxThirst;
            }
            else
            {
                Debug.LogWarning("Not a valid state for stat: " + stat);
                return 0;
            }   
        }
        else if (stat == "Warmth")
        {
            if (getState == "Current")
            {
                return _plantWarmth;
            }
            else if (getState == "Max")
            {
                return _plantMaxWarmth;
            }
            else if (getState == "Min")
            {
                return _plantMinWarmth;
            }
            else
            {
                Debug.LogWarning("Not a valid state for stat: " + stat);
                return 0;
            }   
        }
        else
        {
            Debug.LogWarning("Not a valid state for stat: " + stat);
            return 0;
        }
    }
    #endregion
}