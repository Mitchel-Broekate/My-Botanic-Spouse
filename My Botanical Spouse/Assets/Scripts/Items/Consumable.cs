using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour
{
    ItemStats itemStats;
    string _statEffect;
    float _effectAmount;

    void Start()
    {
        itemStats = GetComponent<ItemStats>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.GetMask("Plant"))
        {
            _statEffect = itemStats.StatEffect;
            _effectAmount = itemStats.EffectAmount;

            if (_statEffect == null || _effectAmount == 0) 
            {
                Debug.LogWarning("effects not given");
                return;
            }

            other.gameObject.GetComponent<PlantManager>().ChangePlantStats(_statEffect, _effectAmount);
            //Create effects
            Destroy(gameObject);
        }
    }


}
