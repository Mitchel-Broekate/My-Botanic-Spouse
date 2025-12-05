using System;
using UnityEngine;

public class WaterCan : MonoBehaviour
{
    [SerializeField] float _waterAmount = 100;
    [SerializeField] float pourThreshold = 26;
    [SerializeField] Transform _waterPoint;
    [SerializeField] bool _isPouring;

    void Update()
    {
        Pour();
    }

    void Pour()
    {
        bool currentPourState = CheckPourAngle() < pourThreshold;
        Debug.Log(CheckPourAngle());

        if (_isPouring != currentPourState)
        {
            _isPouring = currentPourState;

            if (_isPouring && _waterAmount > 0)
            {
                DoPour();
            }
            else if (!_isPouring)
            {
                StopPour();
            }
            else
            {
                Debug.Log("Water emtpy");
            }

        }
    }

    float CheckPourAngle()
    {
        return _waterPoint.forward.y * Mathf.Rad2Deg;
    }

    void DoPour()
    {
        //pour water
        _waterAmount -= 1 * Time.deltaTime;

        //check if plant is underneath
        if (!Physics.Raycast(_waterPoint.position, _waterPoint.forward, out RaycastHit hit, 1000, LayerMask.GetMask("Plant")))
        {
            Debug.Log("Not pouring on plant");
            return;
        }
        else
        {
            PlantManager plantManager = hit.transform.parent.GetComponent<PlantManager>();
            plantManager.BeingWatered(true);

            //give plant water
            plantManager.ChangePlantStats("Thirst", 1 * Time.deltaTime);

            Debug.Log("Pouring on plant");
        }

    }

    void StopPour()
    {
        //stop pour effect
    }
}
