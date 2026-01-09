using UnityEngine;

public class PlantGiveAffection : MonoBehaviour
{
    [SerializeField] PlantManager plantManager;
    GameManager gameManager;
    [SerializeField] float _affectionToGive;
    [SerializeField] int motivationAmount;

    [SerializeField] Animator animator;

    void Start()
    {
        gameManager = GameObject.Find("GameManager(PlayerManager)").GetComponent<GameManager>();
    }

    public void GiveAffection()
    {
        if(plantManager.AllowGivingAffection)
        {
            plantManager.ChangePlantStats("Affection", -_affectionToGive);

            gameManager.PlayerMotivationPoints += motivationAmount;

            Debug.Log("Touched a plant");

            //Play cute boywife animation :3 :P :D
            animator.SetTrigger("Happy");
        }
        else
        {
            Debug.Log("Can't touch boywife yet");
        }

    }
}
