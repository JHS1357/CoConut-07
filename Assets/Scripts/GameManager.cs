using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    [Header("Stage")]
    [Tooltip("현재 wave")]
    public int nCurWave;
    [Tooltip("wave사이 쉬는 시간")]
    public float waveTermTime = 10.0f;
    [Tooltip("스폰 텀 시간")]
    public float spawnTermTime = 1.0f;


    [Header("Enemy")]
    [Tooltip("현재 wave 적의 수")]
    public int nMonsterCnt = -1;       //마리수?
    [Tooltip("잡거나 놓친 적의 수")]
    public int nUsedMonsterCnt;        //잡거나 놓친 마리수.

    [NonSerialized] public static GameManager instance = null;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        //else Destroy(this.gameObject);
        nCurWave = 0;


        //SummonEnemy(0);     //0웨이브 시작.
        //SummonEnemy(1);     //1웨이브 시작.
        //GameObjectPoolingManager.instance.
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void SummonEnemy(int wave)
    {
        StartCoroutine(summon(wave));
    }

    /// <summary>
    /// 한 웨이브에 대한 소환.
    /// </summary>
    IEnumerator summon(int wave)        //나중에 hp,방어력,스피드를 소환시 셋팅할수있도록
    {

        List<GameObject> enemy = GameObjectPoolingManager.instance.RequestGameObjects(wave);  //wave 리턴.
        nMonsterCnt = enemy.Count;
        nUsedMonsterCnt = 0;
        foreach (GameObject e in enemy)
        {
            e.SetActive(true);
            yield return new WaitUntil(() => e.activeSelf);
            //e.GetComponent<EnemyController>().mySetDestination(new Vector3(20.0f, 0.0f, 20.0f));    //나중에 도착지로 설정.


            yield return new WaitForSeconds(spawnTermTime);
        }
        
        yield return new WaitForSeconds(1.0f);
    }
    /// <summary>
    /// 외부 스크립트에서 GameRoutine를 실행시키기 위해 제작. 함수는 의미 x
    /// </summary>
    public void StartGame()
    {
        //StartGame은 CountDownController에서 소환.
        StartCoroutine(GameRoutine());
        CoconutLife.instance.life = 3;
    }

    /// <summary>
    /// 전체 게임의 흐름.
    /// </summary>
    IEnumerator GameRoutine()
    {
        //0wave시작
        SummonEnemy(nCurWave);

        //0wave 클리어? or 타이머로 routine 설정.
        yield return new WaitUntil(() => (nMonsterCnt == nUsedMonsterCnt));

        //0wave쉬는시간
        TimerController.instance.BreakTimerSet();
        yield return new WaitForSeconds(waveTermTime);

        //-----------------------------------------------

        //1wave시작
        nCurWave++;
        TimerController.instance.RoundTimerSet();
        SummonEnemy(nCurWave);


        //1wave 클리어? or 타이머로 routine 설정.
        yield return new WaitUntil(() => (nMonsterCnt == nUsedMonsterCnt));

        //1wave쉬는시간
        TimerController.instance.BreakTimerSet();
        yield return new WaitForSeconds(waveTermTime);


        //-----------------------------------------------

        //2wave시작
        nCurWave++;
        TimerController.instance.RoundTimerSet();
        SummonEnemy(nCurWave);


        //2wave 클리어? or 타이머로 routine 설정.
        yield return new WaitUntil(() => (nMonsterCnt == nUsedMonsterCnt));

        //2wave쉬는시간
        TimerController.instance.BreakTimerSet();
        yield return new WaitForSeconds(waveTermTime);

        //-----------------------------------------------

        //3wave시작
        nCurWave++;
        TimerController.instance.RoundTimerSet();
        SummonEnemy(nCurWave);



        //게임 종료
        yield return null;
        
    }
}
