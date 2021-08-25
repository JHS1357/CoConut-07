using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;

    private float countdown = 2.0f;         // 웨이브 시간 기준
    public float timeBetweenWaves = 5.0f;   // 웨이브 발생 시간간격
    public float spawnTime = .5f;           // 적 스폰 시간간격

    private int waveIndex = 0;  // 웨이브 마다의 적 수 
  
    // Update is called once per frame
    void Update()
    {
        if (countdown <= 0.0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;
    }

    IEnumerator SpawnWave()
    {
        waveIndex++;

        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnTime);
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
