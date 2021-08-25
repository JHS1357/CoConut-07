using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class TimerController : MonoBehaviour
{
    public float runningTime = 0.0f;
    Coroutine timer= null;

    [NonSerialized] public static TimerController instance = null;
    //Text timerText;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        //else Destroy(this.gameObject);
        //TimerSet(); 
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResetTimer()
    {
        runningTime = 0.0f;
    }
    public void RoundTimerSet()
    {
        if (timer != null) StopCoroutine(timer); 
        runningTime = 0.0f;
        //if (timer != null) StopCoroutine(timer);
        timer = StartCoroutine(SetRoundTimer());

    }
    //Round + N / Break Time N
    IEnumerator SetRoundTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            runningTime += 0.1f;
            string minute = ((int)runningTime / 60).ToString("00");
            string second = (runningTime % 60).ToString("00.0");
            int round = GameManager.instance.nCurWave + 1;
            this.transform.GetComponentInChildren<Text>().text = ("Round " + round + "\n" + minute + ":" + second);
        }
    }

    public void BreakTimerSet()
    {
        StopCoroutine(timer);
        runningTime = GameManager.instance.waveTermTime;
        //if (timer != null) StopCoroutine(timer);
        timer = StartCoroutine(SetBreakTimer());

    }
    //Round + N / Break Time N
    IEnumerator SetBreakTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            runningTime -= 0.1f;
            string minute = ((int)runningTime / 60).ToString("00");
            string second = (runningTime % 60).ToString("00.0");
            int round = GameManager.instance.nCurWave + 1;
            this.transform.GetComponentInChildren<Text>().text = ("Break Time "+ round + "\n" + minute + ":" + second);
        }
    }

}
