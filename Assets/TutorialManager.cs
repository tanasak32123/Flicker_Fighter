using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    public GameObject[] Videos;
    public GameObject tutorial;
    public GameObject NextButton;
    public GameObject PrevButton;
    public GameObject FinishButton;
    public int index = 0;
    // Update is called once per frame

    void Update()
    {
        for (int i = 0; i < popUps.Length; i++)
        {
            popUps[i].SetActive(false);
        }
        if(index ==  0)
        {
            PrevButton.SetActive(false);
            NextButton.SetActive(true);
            FinishButton.SetActive(false);
        }else if(index == popUps.Length-1)
        {
            NextButton.SetActive(false);
            PrevButton.SetActive(true);
            FinishButton.SetActive(true);
        }
        else
        {
            NextButton.SetActive(true);
            PrevButton.SetActive(true);
            FinishButton.SetActive(false);
        }
        popUps[index].SetActive(true);
    }

    public void Next()
    {
        if (index < popUps.Length-1)
        {
            index++;
        }
    }

    public void Previous()
    {
        if (index > 0)
        {
            index--;
        }
    }

    public void Close()
    {
        tutorial.SetActive(false);
        index = 0;
    }
}
