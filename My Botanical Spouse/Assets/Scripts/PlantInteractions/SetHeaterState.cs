using UnityEngine;
public class SetHeaterState : MonoBehaviour
{
    [SerializeField] PlantManager plantManager;
    bool _currentHeaterState;

    /// <summary>
    /// This function changes the on/off state of the heater when called 
    /// </summary>
    public void ChangeHeaterState()
    {
        _currentHeaterState = !_currentHeaterState;
        plantManager.HeaterState(_currentHeaterState);
        
        Debug.Log("Current Heater State: " + _currentHeaterState);
    }
}
