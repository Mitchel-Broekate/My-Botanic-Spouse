using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Vars
    [Header("Player Info")]
    [SerializeField] int motivationPoints;

    [Header("Plant Info")]
    [SerializeField]GameObject plantParent;
    List<PlantManager> plantManagers = new();
    
    bool currentGameState;
    #endregion

    void Start()
    {
        //TEMP gets all the PlantManagers in the scene
        GetPlantManagers();
        
        //sets the game state to active at the start
        ChangeGameState();
    }

    #region Player functions
    /// <summary>
    /// Getter/Setter for the MP currency
    /// </summary>
    public int PlayerMotivationPoints
    {
        get
        {
            return motivationPoints;
        }
        set
        {
            motivationPoints = value;
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

        foreach(PlantManager manager in plantManagers)
        {
            manager.SetPlantState(currentGameState);
        }

        Debug.Log("Current Game State: " + currentGameState);
    }

    void GetPlantManagers()
    {
        for(int i = 0; plantParent.transform.childCount > i; i++)
        {
            if(plantManagers.Contains(plantParent.transform.GetChild(i).GetComponent<PlantManager>())) return;

            plantManagers.Add(plantParent.transform.GetChild(i).GetComponent<PlantManager>());
        }
    }
    
    //Level stuff
    #endregion
}
