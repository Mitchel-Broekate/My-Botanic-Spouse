using UnityEngine;

public class ItemStats : MonoBehaviour
{
    [SerializeField] string _statEffect;
    [SerializeField] float _effectAmount;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.GetMask("Plant"))
        {
            if (_statEffect == null || _effectAmount == 0) 
            {
                Debug.LogWarning("effects not given");
                return;
            }

            other.gameObject.GetComponent<PlantManager>().ChangePlantStats(_statEffect, _effectAmount);
        }
    }

    public string ChangeStatEffect
    {
        set
        {
            _statEffect = value;
        }
    }
    public float ChangeEffectAmount
    {
        set
        {
            _effectAmount = value;
        }
    }
}
