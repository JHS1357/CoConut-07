using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoconutLife : MonoBehaviour
{
    public static CoconutLife instance;
    private void Awake()
    {
        if (instance == null) { instance = this; }
    }

    public int life = 3;
    
    public GameObject[] cocos;
    public AudioClip clip;

    public Camera cocoCam;

    public void EnemyEntry()
    {
        StartCoroutine(CocoCamera());
    }

    private IEnumerator CocoCamera()
    {
        cocoCam.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
     
        SoundManager.instance.SFXPlay("CoconutFall", clip);
        cocos[--life].SetActive(false);
        yield return new WaitForSeconds(1.0f);

        if (life <= 0) GameOver();

        cocoCam.gameObject.SetActive(false);
    }

    private void GameOver()
    {
        SceneManager.LoadScene("GameOverScene");
    }
}
