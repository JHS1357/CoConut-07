using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonCtrl : MonoBehaviour
{
    private GameObject main;
    private GameObject levelSelect;
    private GameObject option;
    private GameObject information;

    private void Start()
    {
        main = GameObject.Find("MAIN");
        levelSelect = GameObject.Find("LEVELSELECT");
        option = GameObject.Find("OPTION");
        information = GameObject.Find("INFORMATION");

        main.SetActive(true);
        levelSelect.SetActive(false);
        option.SetActive(false);
        information.SetActive(false);
    }

    public void GameStart()
    {
        SceneManager.LoadSceneAsync("LoadingScene");
        
    }

    public void ToMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void LevelSelect()
    {
        main.SetActive(false);
        levelSelect.SetActive(true);
    }

    public void Option()
    {
        main.SetActive(false);
        option.SetActive(true);
    }

    public void Information()
    {
        main.SetActive(false);
        information.SetActive(true);
    }

    public void Close()
    {
        if (option.activeInHierarchy) option.SetActive(false);
        if (levelSelect.activeInHierarchy) levelSelect.SetActive(false);
        if (information.activeInHierarchy) information.SetActive(false);
  
        main.SetActive(true);
    }
}