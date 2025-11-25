using UnityEngine;

public class PlantGiveAffection : MonoBehaviour
{
    [SerializeField] PlantManager plantManager;
    [SerializeField] float _affectionToGive;

    public void GiveAffection()
    {
        if(plantManager.AllowGivingAffection)
        {
            plantManager.ChangePlantStats("Affection", -_affectionToGive);

            Debug.Log("Touched a plant");
        }
        else
        {
            Debug.Log("Can't touch boywife yet");
        }

        //Play cute boywife animation :3 :P :D
    }
}
