using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void play()
    {
        SceneManager.LoadScene("MainGameScene", LoadSceneMode.Single);
    }
    public void quit()
    {
        Application.Quit();
        Debug.Log("Player left the game");
    }

    public void settings()
    {

    }
}
