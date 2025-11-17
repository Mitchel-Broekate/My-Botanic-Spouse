using UnityEngine;

public class PlantManager : MonoBehaviour
{
    #region Vars
    [SerializeField] int _plantMaxHealth;
    [SerializeField] int _plantMaxAffection;
    [SerializeField] int _plantMaxSoilQuality;
    [SerializeField] int _plantMaxThirst;
    [SerializeField] int _plantMaxWarmth;

    [SerializeField] int plantHealth;
    [SerializeField] int plantAffection;
    [SerializeField] int plantSoilQuality;
    [SerializeField] int plantThirst;
    [SerializeField] int plantWarmth;
    #endregion

    /// <summary>
    /// Will update the plant stats (Decrease all stats over time)
    /// </summary>
    void Update()
    {

    }

    #region ChangeStat ints
    /// <summary>
    /// This int changes the health of the plant based on the given information (change types: "plus", "min")
    /// </summary>
    /// <param name="changeType"></param>
    /// <param name="healthchange"></param>
    /// <returns></returns>
    public int ChangePlantHealth(string changeType, int healthChange)
    {
        if (changeType == "plus")
        {
            plantHealth += healthChange;
            if (plantHealth > _plantMaxHealth)
            {
                plantHealth = _plantMaxHealth;
            }
        }
        else if (changeType == "min")
        {
            plantHealth -= healthChange;
            if (plantHealth <= 0)
            {
                //Execute plant death (min 1 plant, if plant count if 0 gameover)
            }
        }
        else
        {
            Debug.LogWarning("Not a valid changeType");
        }

        return plantHealth;
    }

    public int ChangePlantAffection(string changeType, int affectionChange)
    {
        if (changeType == "plus")
        {
            plantAffection += affectionChange;
            if (plantAffection > _plantMaxAffection)
            {
                plantAffection = _plantMaxAffection;
            }
        }
        else if (changeType == "min")
        {
            plantAffection -= affectionChange;
            if (plantAffection <= 0)
            {
                //Execute plant health damage over time
            }
        }
        else
        {
            Debug.LogWarning("Not a valid changeType");
        }

        return plantAffection;
    }

    public int ChangePlantSoilQuality(string changeType, int soilChange)
    {
        if (changeType == "plus")
        {
            plantSoilQuality += soilChange;
            if (plantSoilQuality > _plantMaxSoilQuality)
            {
                plantSoilQuality = _plantMaxSoilQuality;
            }
        }
        else if (changeType == "min")
        {
            plantSoilQuality -= soilChange;
            if (plantSoilQuality <= 0)
            {
                //Execute plant health damage over time
            }
        }
        else
        {
            Debug.LogWarning("Not a valid changeType");
        }

        return plantSoilQuality;
    }

    public int ChangePlantSoilThirst(string changeType, int thirstChange)
    {
        if (changeType == "plus")
        {
            plantThirst += thirstChange;
            if (plantThirst > _plantMaxThirst)
            {
                plantThirst = _plantMaxThirst;
            }
        }
        else if (changeType == "min")
        {
            plantThirst -= thirstChange;
            if (plantThirst <= 0)
            {
                //Execute plant health damage over time
            }
        }
        else
        {
            Debug.LogWarning("Not a valid changeType");
        }

        return plantThirst;
    }

    public int ChangePlantSoilWarmth(string changeType, int warmthChange)
    {
        if (changeType == "plus")
        {
            plantWarmth += warmthChange;
            if (plantWarmth > _plantMaxWarmth)
            {
                plantWarmth = _plantMaxWarmth;
            }
        }
        else if (changeType == "min")
        {
            plantWarmth -= warmthChange;
            if (plantWarmth <= 0)
            {
                //Execute plant health damage over time
            }
        }
        else
        {
            Debug.LogWarning("Not a valid changeType");
        }


        return plantWarmth;
    }
    #endregion
}