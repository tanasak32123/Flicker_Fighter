using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelecter : MonoBehaviour
{
    public void OpenLevel(int level)
    {
        SceneManager.LoadScene(level);
    }
}
