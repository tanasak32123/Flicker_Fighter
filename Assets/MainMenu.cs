using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject tutorial;
    public void PlayGame(int level)
    {
        SceneManager.LoadSceneAsync(level);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void TutorialOpen()
    {
        tutorial.SetActive(true);
    }

}
