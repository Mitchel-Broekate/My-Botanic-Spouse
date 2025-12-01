using UnityEngine;

public class TEMPGameManagerActivate : MonoBehaviour
{
    [SerializeField] PlantManager plantManager;
    bool currentGameState;

    public void ChangeGameState()
    {
        currentGameState = !currentGameState;

        plantManager.SetPlantState(currentGameState);

        Debug.Log("Current Game State: " + currentGameState);
    }
}
