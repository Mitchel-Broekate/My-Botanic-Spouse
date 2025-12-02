using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ItemStats : MonoBehaviour
{
    [SerializeField] string _statEffect;
    [SerializeField] float _effectAmount;
    [SerializeField] int _itemCost;

    XRGrabInteractable xRGrabInteractable;

    void Start()
    {
        xRGrabInteractable = GetComponent<XRGrabInteractable>();
        xRGrabInteractable.interactionManager = GameObject.Find("XR Interaction Manager").GetComponent<XRInteractionManager>();
    }

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


    public int GetItemCost
    {
        get
        {
            return _itemCost;
        }
    }
}
