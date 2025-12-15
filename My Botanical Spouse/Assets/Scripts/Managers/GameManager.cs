using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Vars
    [Header("Player Info")]
    [SerializeField] int _motivationPoints;

    [Header("Plant Info")]
    [SerializeField]GameObject plantPrefab;
    [SerializeField]GameObject _plantParent;
    List<PlantManager> _plantManagers = new();

    [Header("Level Info")]
    [SerializeField] float _levelTime;
    float _currentTime;
    [SerializeField] bool _timerActive;
    [SerializeField] int _currentLevel = 0;
    [SerializeField] List<Transform> _plantSpawns = new();

    bool currentGameState;
    #endregion

    void Start()
    {
        //Spawns the plants in the game
        StartNextLevel();
    }

    void Update()
    {
        if(_timerActive)
        {
            StartLevelTimer();
        }
    }

    #region Player functions
    /// <summary>
    /// Getter/Setter for the MP currency
    /// </summary>
    public int PlayerMotivationPoints
    {
        get
        {
            return _motivationPoints;
        }
        set
        {
            _motivationPoints = value;
        }
    }

    #endregion

    #region Game functions
    /// <summary>
    /// Changes the active state of the game (active/inactive)
    /// </summary>
    public void ChangeGameState()
    {
        currentGameState = !currentGameState;

        foreach(PlantManager manager in _plantManagers)
        {
            manager.SetPlantState(currentGameState);
        }

        Debug.Log("Current Game State: " + currentGameState);
    }

    /// <summary>
    /// Gets all active managers for the plants
    /// </summary>
    void GetPlantManagers()
    {
        for(int i = 0; _plantParent.transform.childCount > i; i++)
        {
            if(_plantManagers.Contains(_plantParent.transform.GetChild(i).GetComponent<PlantManager>())) return;

            _plantManagers.Add(_plantParent.transform.GetChild(i).GetComponent<PlantManager>());
        }
    }
    
    public void StartNextLevel()
    {
        if(_currentLevel < 4)
        {
            //instantiate plant at spawn pos in list equal to int i
            GameObject spawnedPlant = Instantiate(plantPrefab, _plantSpawns[_currentLevel].transform.position, _plantSpawns[_currentLevel].transform.rotation);
            spawnedPlant.transform.parent = _plantParent.transform;

            //gets all active plant's managers
            GetPlantManagers();

            //sets the game state to active at the start
            ChangeGameState();

            //start timer
            _timerActive = true;
            _currentTime = _levelTime;

            //deactivate level UI if active
        }
        else
        {
            //win condition ENDGAME
            _timerActive = false;

            //activate win UI
        }

        _currentLevel += 1;
    }

    void StartLevelTimer()
    {
        _currentTime -= Time.deltaTime;

        if(_currentTime <= 0)
        {
            //win condition LEVEL (activate UI next level)

            _timerActive = false;

            ChangeGameState();
        }
    }
    #endregion
}