using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantStatWarningUI : MonoBehaviour
{
    [Header("Thresholds")]
    [SerializeField] float _standardMin = 50f;
    [SerializeField] float _coldMin = 25f;
    [SerializeField] float _hotMax = 35f;

    [Header("References")]
    [SerializeField] PlantManager _plantManager;

    [Header("Parents")]
    [SerializeField] Transform _healthParent;
    [SerializeField] Transform _affectionParent;
    [SerializeField] Transform _soilParent;
    [SerializeField] Transform _thirstParent;
    [SerializeField] Transform _warmthParent;

    [Header("Prefabs")]
    [SerializeField] List<GameObject> _healthWarnings;
    [SerializeField] List<GameObject> _affectionWarnings;
    [SerializeField] List<GameObject> _soilWarnings;
    [SerializeField] List<GameObject> _thirstWarnings;
    [SerializeField] List<GameObject> _coldWarnings;
    [SerializeField] List<GameObject> _hotWarnings;

    [Header("Audio")]
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _warningSpawnSound;

    Dictionary<string, GameObject> _instances = new();

    void OnEnable()
    {
        PlantManager.OnPlantSpawned += RegisterPlant;
    }

    void OnDisable()
    {
        PlantManager.OnPlantSpawned -= RegisterPlant;

        if (_plantManager != null)
            _plantManager.OnStatChanged -= OnStatChanged;
    }

    void RegisterPlant(PlantManager manager)
    {
        Debug.Log("[WarningUI] PlantManager registered");

        _plantManager = manager;
        _plantManager.OnStatChanged += OnStatChanged;

        StartCoroutine(DelayedSync());
    }

    IEnumerator DelayedSync()
    {
        yield return new WaitUntil(() => _plantManager.IsInitialized);
        _plantManager.ForceStatSync();
    }

    void OnStatChanged(string stat, float value)
    {
        switch (stat)
        {
            case "Health":
                HandleStandard(stat, value, _healthWarnings, _healthParent);
                break;
            case "Affection":
                HandleStandard(stat, value, _affectionWarnings, _affectionParent);
                break;
            case "SoilQuality":
                HandleStandard(stat, value, _soilWarnings, _soilParent);
                break;
            case "Thirst":
                HandleStandard(stat, value, _thirstWarnings, _thirstParent);
                break;
            case "Warmth":
                HandleWarmth(value);
                break;
        }
    }

    void HandleStandard(string key, float value, List<GameObject> prefabs, Transform parent)
    {
        if (value < _standardMin)
            SpawnIfMissing(key, prefabs, parent);
        else
            Remove(key);
    }

    void HandleWarmth(float value)
    {
        if (value < _coldMin)
            Replace("Warmth", _coldWarnings, _warmthParent);
        else if (value > _hotMax)
            Replace("Warmth", _hotWarnings, _warmthParent);
        else
            Remove("Warmth");
    }

    void SpawnIfMissing(string key, List<GameObject> prefabs, Transform parent)
    {
        if (_instances.ContainsKey(key)) return;

        _instances[key] = Instantiate(
            prefabs[Random.Range(0, prefabs.Count)],
            parent
        );

        PlayWarningSound();
    }

    void Replace(string key, List<GameObject> prefabs, Transform parent)
    {
        Remove(key);
        SpawnIfMissing(key, prefabs, parent);
    }

    void Remove(string key)
    {
        if (!_instances.TryGetValue(key, out var go)) return;

        Destroy(go);
        _instances.Remove(key);
    }

    void PlayWarningSound()
    {
        if (_audioSource != null && _warningSpawnSound != null)
            _audioSource.PlayOneShot(_warningSpawnSound);
    }
}
