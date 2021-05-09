using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public List<Wave> waveList = new List<Wave>();
    int currentWave;
    int enemyPrsentInScene;
    //[HideInInspector] public List<GameObject> spawnedEnemys = new List<GameObject>();

    public static EnemySpawner enemySpawnerInstance;
    private void Awake()
    {
        if (enemySpawnerInstance == null)
            enemySpawnerInstance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyPrsentInScene = 0;
        StartCoroutine(SpawnAllWaves());
        //Invoke("CheckEnemyStates", 1f);
        //SpawnAllWaves();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddSpawnedEnemy()
    {
        enemyPrsentInScene = enemyPrsentInScene + 1;
    }
    public void RemoveSpawnedEnemy()
    {
        enemyPrsentInScene = enemyPrsentInScene - 1;
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
            if(enemyPrsentInScene <= 0)
            {
                Debug.Log("Spawning new Wave");
                StartCoroutine(waveList[currentWave].SpawnAllEnemies());
                //yield return StartCoroutine(SpawnAllEnemiesInCurrentWave(waveList[currentWave]));
                currentWave++;
            }
            yield return new WaitForSeconds(1f);

        }
        //Invoke("CheckEnemyStates", 1f);
        //currentSuperWave++;
    }
}
