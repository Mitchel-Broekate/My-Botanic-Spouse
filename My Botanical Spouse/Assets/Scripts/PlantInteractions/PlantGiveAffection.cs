using UnityEngine;

public class PlantGiveAffection : MonoBehaviour
{
    [SerializeField] PlantManager plantManager;
    [SerializeField] float _affectionToGive;

    public void GiveAffection()
    {
        plantManager.ChangePlantStats("Affection", _affectionToGive);

        Debug.Log("Touched a plant");

        //play animation
    }
    //Add affection in the PlantManager
    //Play cute boywife animation :3 :P :D
}
