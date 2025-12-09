using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WaterTank : MonoBehaviour
{
    #region Vars
    [Header("Frame Detection")]
    [SerializeField] string _frameTag;
    GameObject waterTank;
    
    [Header("Timer Conditions")]
    [Tooltip("Time in seconds")]
    [SerializeField] float timerDuration;
    bool _isTimerActive;

    ItemStats itemStats;
    GameManager gameManager;
    PlantManager plantManager;
    XRGrabInteractable _xRGrabInteractable;

    void Start()
    {
        itemStats = GetComponent<ItemStats>();
        _xRGrabInteractable = GetComponent<XRGrabInteractable>();
        gameManager = GameObject.Find("GameManager(PlayerManager)").GetComponent<GameManager>();
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
        Debug.Log("Collided");

        if(collision.gameObject.CompareTag(_frameTag))
        {
            Debug.Log("Layer = frame");

            waterTank = collision.gameObject;

            _xRGrabInteractable.enabled = false;

            //change pos, rot, and parent
            transform.parent = waterTank.transform;
            transform.position = waterTank.transform.position;
            transform.rotation = waterTank.transform.rotation;

            plantManager = waterTank.transform.parent.GetComponent<PlantManager>();
            
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
            //start thirst drain again
            plantManager.BeingWatered(false);
            //do particle
            //destroy object
        }
        else
        {
            WaterTankConditions();
        }
    }

    //add mp per ...
    //stop decreasing water for plant

    /// <summary>
    /// Stops the thirst drain and gives mp overtime
    /// </summary>
    void WaterTankConditions()
    {
        plantManager.BeingWatered(true);

        float amountToAdd =  itemStats.GetMotivationPoints * Time.deltaTime;
        if(amountToAdd >= 1)
        {
            int whole = Mathf.FloorToInt(amountToAdd);
            gameManager.PlayerMotivationPoints += whole;
        }
    }
}
