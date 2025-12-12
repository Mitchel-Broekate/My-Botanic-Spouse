using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WaterTank : MonoBehaviour
{
    #region Vars
    [Header("Frame Detection")]
    [SerializeField] LayerMask _layerMask;
    GameObject frame;
    
    [Header("Timer Conditions")]
    [Tooltip("Time in seconds")]
    [SerializeField] float _timerDuration;
    bool _isTimerActive;

    [Header("MP Config")]
    [SerializeField] float _mpCooldownTime;
    bool _canAddMP;

    ItemStats _itemStats;
    GameManager _gameManager;
    PlantManager _plantManager;
    XRGrabInteractable _xRGrabInteractable;

    void Start()
    {
        _itemStats = GetComponent<ItemStats>();
        _xRGrabInteractable = GetComponent<XRGrabInteractable>();
        _gameManager = GameObject.Find("GameManager(PlayerManager)").GetComponent<GameManager>();
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
        if(collision.gameObject.layer == LayerMask.NameToLayer("Frame"))
        {
            frame = collision.gameObject;

            _xRGrabInteractable.enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;

            //change pos, rot, and parent
            transform.parent = frame.transform;
            transform.localPosition = Vector3.zero;
            transform.rotation = frame.transform.rotation;

            _plantManager = frame.transform.parent.GetComponent<PlantManager>();
            
            //activate timer + conditions
            _isTimerActive = true;
        }
    }  

    /// <summary>
    /// Executes the timer for the tank durability 
    /// </summary>
    void DurabilityTimer()
    {
        _timerDuration -= Time.deltaTime;
        if(_timerDuration <= 0)
        {
            //start thirst drain again
            _plantManager.BeingWatered(false);
            //do particle
            Destroy(gameObject, 0.3f);
        }
        else
        {
            WaterTankConditions();
        }
    }

    /// <summary>
    /// Stops the thirst drain and gives mp overtime
    /// </summary>
    void WaterTankConditions()
    {
        if(!_plantManager.checkBeingWatered)
        {
            _plantManager.BeingWatered(true);
        }

        if(_canAddMP)
        {
            _gameManager.PlayerMotivationPoints += _itemStats.GetMotivationPoints;
            StartCoroutine(AddMPCooldown(_mpCooldownTime)); 
        }
    }
    
    /// <summary>
    /// Disables the AddMP Function for a given duration
    /// </summary>
    /// <param name="duration"></param>
    /// <returns></returns>
    IEnumerator AddMPCooldown(float duration)
    {
        _canAddMP = false;
        yield return new WaitForSeconds(duration);
        _canAddMP = true;
    }
}
