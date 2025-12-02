using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Vars
    [SerializeField] PlantManager plantManager;
    [SerializeField] int motivationPoints;
    bool currentGameState;
    #endregion

    public void ChangeGameState()
    {
        currentGameState = !currentGameState;

        plantManager.SetPlantState(currentGameState);

        Debug.Log("Current Game State: " + currentGameState);
    }

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
}
