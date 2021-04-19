using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemySpawner : MonoBehaviour
{
     
    public List<NewWave> newWaveList = new List<NewWave>();
    int currentWave;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnAllWaves());
        //Invoke("CheckEnemyStates", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnAllWaves()
    {
        currentWave = 0;
        //posInSmallEnemyFormation = 0;
        //posInMediumEnemyFormation = 0;
        //posInBossEnemyFormation = 0;
        //spawnedEnemys.Clear();

        while (currentWave < newWaveList.Count)
        {
            yield return StartCoroutine(newWaveList[currentWave].SpawnAllEnemies());
            //yield return StartCoroutine(SpawnAllEnemiesInCurrentWave(waveList[currentWave]));
            currentWave++;
        }
        //Invoke("CheckEnemyStates", 1f);
        //currentSuperWave++;
    }
}
