using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "ScriptableObjects/ItemSO", order = 1)]
public class ItemSO : ScriptableObject
{
    public GameObject itemObject;
    public string itemEffect ="";
    public float effectAmount;

}
