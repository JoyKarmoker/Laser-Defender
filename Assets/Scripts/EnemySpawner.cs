using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public List<Wave> waveList = new List<Wave>();
    int currentWave;
    //[HideInInspector] public List<GameObject> spawnedEnemys = new List<GameObject>();


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

        while (currentWave < waveList.Count)
        {
            Debug.Log("Spawning new enemy");
            yield return StartCoroutine(waveList[currentWave].SpawnAllEnemies());
            //yield return StartCoroutine(SpawnAllEnemiesInCurrentWave(waveList[currentWave]));
            currentWave++;
        }
        //Invoke("CheckEnemyStates", 1f);
        //currentSuperWave++;
    }
}
