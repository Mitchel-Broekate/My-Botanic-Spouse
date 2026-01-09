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
    [SerializeField] float _currentTime;
    [SerializeField] bool _timerActive;
    [SerializeField] int _currentLevel = 0;
    [SerializeField] List<Transform> _plantSpawns = new();

    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject loseScreen;

    bool currentGameState = false;
    #endregion

    void Start()
    {
        //Spawns the plants in the game
        StartLevel();
    }

    void Update()
    {
        if(_timerActive)
        {
            LevelTimer();
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
    public void ChangeGameState(bool gameState)
    {
        currentGameState = gameState;
        Debug.Log("Current Game State: " + currentGameState);

        foreach(PlantManager manager in _plantManagers)
        {
            if (gameState)
            {
                manager.Init();
            }
            
            manager.SetPlantState(currentGameState);
        }
    }

    /// <summary>
    /// Gets all active managers for the plants
    /// </summary>
    void GetPlantManagers()
    {
        for(int i = 0; _plantParent.transform.childCount > i; i++)
        {
            if(!_plantManagers.Contains(_plantParent.transform.GetChild(i).GetComponent<PlantManager>()))
            {
                _plantManagers.Add(_plantParent.transform.GetChild(i).GetComponent<PlantManager>());
            }
        }
    }
    
    public void StartLevel()
    {
            //instantiate plant at spawn pos in list equal to int i
            GameObject spawnedPlant = Instantiate(plantPrefab, _plantSpawns[_currentLevel].transform.position, _plantSpawns[_currentLevel].transform.rotation);
            spawnedPlant.transform.parent = _plantParent.transform;

            //gets all active plant's managers
            GetPlantManagers();

            Debug.Log("Start Game");
            //sets the game state to active at the start
            ChangeGameState(true);

            //start timer
            _timerActive = true;
            _currentTime = _levelTime;
            _currentLevel++;
    }

    void LevelTimer()
    {
        _currentTime -= Time.deltaTime;

        if(_currentTime <= 0)
        {
            //win condition LEVEL (activate UI next level)

            Debug.Log("Level won");

            _timerActive = false;

            ChangeGameState(false);

            AcitvateWinScreen();
        }
    }
    #endregion


    public void AcitvateWinScreen()
    {
        winScreen.SetActive(true);
    }

    public void AcitvateLoseScreen()
    {
        loseScreen.SetActive(true);
    }
}