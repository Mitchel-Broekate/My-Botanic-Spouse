using UnityEngine;
using UnityEngine.InputSystem;

public class SetHeaterState : MonoBehaviour
{
    [SerializeField] PlantManager plantManager;
    [SerializeField] Material heaterMat;
    bool _currentHeaterState;

    public void ChangeHeaterState()
    {
        //If clicked changes the Heater state
        _currentHeaterState = !_currentHeaterState;
        plantManager.HeaterState(_currentHeaterState);
        
        Debug.Log("Current Heater State: " + _currentHeaterState);
    }
}
