using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WaterTank : MonoBehaviour
{
    #region Vars
    [Header("Frame Detection")]
    [SerializeField] LayerMask _frameLayer;
    
    [Header("Timer Conditions")]
    float timerDuration;
    bool _isTimerActive;

    ItemStats itemStats;
    XRGrabInteractable _xRGrabInteractable;

    void Start()
    {
        itemStats = GetComponent<ItemStats>();
        _xRGrabInteractable = GetComponent<XRGrabInteractable>();
    }

    void Update()
    {
        if(_isTimerActive)
        {
            DurabilityTimer();
        }
    }


    #endregion
    //place tank on frame

    /// <summary>
    /// Checks if the tank collides with the tank frame and places the frame there (makes it uninteractable)
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == _frameLayer)
        {
            _xRGrabInteractable.enabled = false;

            //change pos, rot, and parent
            transform.parent = collision.transform;
            transform.position = collision.transform.position;
            transform.rotation = collision.transform.rotation;

            //activate timer + conditions
            _isTimerActive = true;
        }
    }  

    /// <summary>
    /// Executes the timer for the tank durability 
    /// </summary>
    void DurabilityTimer()
    {
        timerDuration -= Time.deltaTime;
        if(timerDuration <= 0)
        {
            //do particle
            //destroy object
        }
        else
        {
            WaterTankConditions();
        }
    }

    void WaterTankConditions()
    {
        
    }
    //add mp per ...
    //stop decreasing water for plant
}
