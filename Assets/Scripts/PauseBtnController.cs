using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseBtnController : MonoBehaviour
{
    public void ClickPauseBtn()
    {
        Time.timeScale = 0.0f;     
    }

    public void ClickResumeBtn()
    {
        Time.timeScale = 1.0f;
    }

    public void GoToMain()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadSceneAsync("MainMenu");
        //SceneManager.UnloadSceneAsync()
    }
}
