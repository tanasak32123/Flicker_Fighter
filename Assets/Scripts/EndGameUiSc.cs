using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;


public class EndGameUiSc : MonoBehaviour
{
    public TextMeshProUGUI winText;
    public GameObject Red;
    public GameObject Blue;

    public void win(int winner)
    {

        FindObjectOfType<AudioManager>().Stop("Background");
        FindObjectOfType<AudioManager>().Play("Victory");
        Debug.Log(winner);
        if(winner == 1)
        {
            Red.SetActive(false);
            Blue.SetActive(true);
            return;
        }
        Red.SetActive(true);
        Blue.SetActive(false);
    }

    public void NewGame(int level)
    {
        SceneManager.LoadSceneAsync(level);
    }
}
