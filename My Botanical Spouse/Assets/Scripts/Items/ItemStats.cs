using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ItemStats : MonoBehaviour
{
    #region Vars
    [SerializeField] string _statEffect;
    [SerializeField] float _effectAmount;
    [SerializeField] int _itemCost;
    [SerializeField] int _motivationPoints;
    XRGrabInteractable xRGrabInteractable;
    #endregion

    /// <summary>
    /// Gets the Interactable component for the Interaction Manager
    /// </summary>
    void Start()
    {
        xRGrabInteractable = GetComponent<XRGrabInteractable>();
        xRGrabInteractable.interactionManager = GameObject.Find("XR Interaction Manager").GetComponent<XRInteractionManager>();
    }

    /// <summary>
    /// Getter/Setter for the stat effect
    /// </summary>
    public string StatEffect
    {
        get
        {
            return _statEffect;
        }
        set
        {
            _statEffect = value;
        }
    }

    /// <summary>
    /// Getter/Setter for the effect amount
    /// </summary>
    public float EffectAmount
    {
        get
        {
            return _effectAmount;
        }
        set
        {
            _effectAmount = value;
        }
    }

    /// <summary>
    /// Getter for the item cost
    /// </summary>
    public int GetItemCost
    {
        get
        {
            return _itemCost;
        }
    }

    /// <summary>
    /// Getter for the motivation points
    /// </summary>
    public int GetMotivationPoints
    {
        get
        {
            return _motivationPoints;
        }
    }
}
