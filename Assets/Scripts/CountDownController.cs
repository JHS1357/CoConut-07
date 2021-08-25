using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownController : MonoBehaviour
{
    public int countdownTime;
    public TMPro.TMP_Text countdownDisplay;
    float curTime = 0.0f;


    IEnumerator CountDownToStart()
    {
        countdownDisplay.gameObject.SetActive(true);
        while (countdownTime > 0)
        {
            curTime += Time.deltaTime;

            countdownDisplay.fontSize -= Time.deltaTime * 200;
            countdownDisplay.text = countdownTime.ToString();
            yield return new WaitForSeconds(Time.deltaTime);
            if(curTime>1.0f)
            {
                curTime = 0.0f;
                countdownTime--;
                countdownDisplay.fontSize = 400;
            }

        }
        countdownDisplay.fontSize = 170;
        countdownDisplay.text = "START!";
        yield return new WaitForSeconds(1.0f);

        GameManager.instance.StartGame();
        //GameManager.instance.SummonEnemy(GameManager.instance.nCurWave);

        TimerController.instance.RoundTimerSet();
        countdownDisplay.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        StopCoroutine(CountDownToStart());
        StartCoroutine(CountDownToStart());

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
