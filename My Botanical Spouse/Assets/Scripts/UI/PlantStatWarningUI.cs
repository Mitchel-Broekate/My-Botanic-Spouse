using System.Collections.Generic;
using UnityEngine;

public class PlantStatWarningUI : MonoBehaviour
{
    [Header("Warning Thresholds")]
    [SerializeField] private float _healthWarningMin = 50f;
    [SerializeField] private float _affectionWarningMin = 50f;
    [SerializeField] private float _soilWarningMin = 50f;
    [SerializeField] private float _thirstWarningMin = 50f;

    [SerializeField] private float _warmthColdMin = 25f;
    [SerializeField] private float _warmthHotMax = 35f;

    [Header("References")]
    [SerializeField] private PlantManager _plantManager;

    [Header("UI Spawn Parents")]
    [SerializeField] private Transform _healthParent;
    [SerializeField] private Transform _affectionParent;
    [SerializeField] private Transform _soilParent;
    [SerializeField] private Transform _thirstParent;
    [SerializeField] private Transform _warmthParent;





    [Header("Stat Warning Prefabs (< 50)")]
    [SerializeField] private List<GameObject> _healthWarnings;
    [SerializeField] private List<GameObject> _affectionWarnings;
    [SerializeField] private List<GameObject> _soilWarnings;
    [SerializeField] private List<GameObject> _thirstWarnings;

    [Header("Warmth Warning Prefabs")]
    [SerializeField] private List<GameObject> _coldWarnings; // < 25
    [SerializeField] private List<GameObject> _hotWarnings;  // > 35

    private GameObject _healthInstance;
    private GameObject _affectionInstance;
    private GameObject _soilInstance;
    private GameObject _thirstInstance;
    private GameObject _warmthInstance;

    void Update()
    {
        CheckStandardStat("Health", _healthWarningMin, _healthWarnings, ref _healthInstance, _healthParent);
        CheckStandardStat("Affection", _affectionWarningMin, _affectionWarnings, ref _affectionInstance, _affectionParent);
        CheckStandardStat("SoilQuality", _soilWarningMin, _soilWarnings, ref _soilInstance, _soilParent);
        CheckStandardStat("Thirst", _thirstWarningMin, _thirstWarnings, ref _thirstInstance, _thirstParent);

        CheckWarmth();
    }



    #region Standard Stats (< 50)
    void CheckStandardStat(
    string statName,
    float threshold,
    List<GameObject> prefabs,
    ref GameObject instance,
    Transform parent)
    {
        float value = _plantManager.GetPlantstat("Current", statName);

        if (value < threshold)
        {
            if (instance == null)
            {
                instance = SpawnRandom(prefabs, parent);
            }
        }
        else
        {
            RemoveInstance(ref instance);
        }
    }

    #endregion

    #region Warmth Logic
    void CheckWarmth()
    {
        float warmth = _plantManager.GetPlantstat("Current", "Warmth");

        if (warmth < _warmthColdMin)
        {
            if (_warmthInstance == null)
                _warmthInstance = SpawnRandom(_coldWarnings, _warmthParent);
        }
        else if (warmth > _warmthHotMax)
        {
            if (_warmthInstance == null)
                _warmthInstance = SpawnRandom(_hotWarnings, _warmthParent);
        }
        else
        {
            RemoveInstance(ref _warmthInstance);
        }
    }


    #endregion

    #region Helpers
    GameObject SpawnRandom(List<GameObject> prefabs, Transform parent)
    {
        if (prefabs == null || prefabs.Count == 0 || parent == null)
            return null;

        int index = Random.Range(0, prefabs.Count);
        return Instantiate(prefabs[index], parent);
    }


    void RemoveInstance(ref GameObject instance)
    {
        if (instance != null)
        {
            Destroy(instance);
            instance = null;
        }
    }
    #endregion
}
