using UnityEngine;

public class PlantGiveAffection : MonoBehaviour
{
    [SerializeField] PlantManager plantManager;
    [SerializeField] GameManager gameManager;
    [SerializeField] float _affectionToGive;
    [SerializeField] int motivationAmount;

    public void GiveAffection()
    {
        if(plantManager.AllowGivingAffection)
        {
            plantManager.ChangePlantStats("Affection", -_affectionToGive);

            gameManager.PlayerMotivationPoints += motivationAmount;

            Debug.Log("Touched a plant");
        }
        else
        {
            Debug.Log("Can't touch boywife yet");
        }

        //Play cute boywife animation :3 :P :D
    }
}
