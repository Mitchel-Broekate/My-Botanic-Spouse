using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Vars
    [SerializeField] List<PlantManager> plantManagers = new();
    [SerializeField] int motivationPoints;
    bool currentGameState;
    #endregion

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
    
    //Level stuff
    #endregion
}
