using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    /*
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    bool looping = true; //If this is true then the waves will keep repating like if we have 4 waves in this level 4 waves will keep repating

    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        } while (looping);
    }

    private IEnumerator SpawnAllWaves()
    {
        for(int waveIndex = startingWave; waveIndex <waveConfigs.Count ; waveIndex++)
        {
            int randomIndex = Random.Range(0, waveConfigs.Count);
            var currentWave = waveConfigs[randomIndex]; //Selects any of the wave randomly
            yield return StartCoroutine(SpawnAllEnemiesInCurrentWave(currentWave));
        }
    }

    private IEnumerator SpawnAllEnemiesInCurrentWave(WaveConfig waveConfig)
    {
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWayPoints()[0].transform.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }*/

    [SerializeField] float secAfterEnemyStartSpawn = 3f;
    int currentWave = 0;
    int totalWaves;
    int posInFormation = 0;
    int posInSmallEnemyFormation = 0;
    int posInMediumEnemyFormation = 0;
    int posInBossEnemyFormation = 0;
    [System.Serializable]
    public class Wave
    {
        [Header("Enemy Properties")]
        public float enemySpeed = 10f;
        public float enemyRotationSpeed = 10f;
        public int enemyAmount;

        [Header("Intervals")]
        public float enemySpawnInterval = 0.2f; // Time between enemys to spawn
        public float secToWaitForNextWave = 2f; // Time between waves to spawn

        [Header("Prefabs")]
        public GameObject pathPrefab;
        public GameObject enemyFormationPrefab; //In which Formation This waves enemy will sit
        public GameObject EnemyPrefab;
    }

    public List<Wave> waveList = new List<Wave>();

    IEnumerator Start()
    {
        totalWaves = waveList.Count;
        yield return new WaitForSeconds(secAfterEnemyStartSpawn);
        yield return StartCoroutine(SpawnAllWaves());
    }



    private IEnumerator SpawnAllWaves()
    {
        while (currentWave < totalWaves)
        {
            yield return StartCoroutine(SpawnAllEnemiesInCurrentWave(waveList[currentWave]));
            currentWave++;
        }
    }

    private IEnumerator SpawnAllEnemiesInCurrentWave(Wave currentWave)
    {
        //total number of eneyms that can fit in this wave
        int totalEnemysInThisFormation = currentWave.enemyFormationPrefab.GetComponent<Formation>().gridSizeX * currentWave.enemyFormationPrefab.GetComponent<Formation>().gridSizeY;
        if (currentWave.enemyFormationPrefab.tag == "SmallEnemyFormation")
        {
            posInFormation = posInSmallEnemyFormation;         
            if (posInFormation >= totalEnemysInThisFormation)
            {
                Debug.LogError("There are no more place of enemy in " + currentWave.enemyFormationPrefab.gameObject.name.ToString());
            }
        }

        else if (currentWave.enemyFormationPrefab.tag == "MediumEnemyFormation")
        {
            // Debug.Log("Pos in medium enemy Formation");
            posInFormation = posInMediumEnemyFormation;
        }
        else if (currentWave.enemyFormationPrefab.tag == "BossEnemyFormation")
        {
            posInFormation = posInBossEnemyFormation;
        }
        int totalEnemeysInThisWave = currentWave.enemyAmount;
        for (int enemyCount = 0; enemyCount < totalEnemeysInThisWave; enemyCount++)
        {
            //Validating Amount so that there is enough place in forrmation for all the enemys
            if (posInFormation >= totalEnemysInThisFormation)
            {
                Debug.LogError("There are no more place of enemy in " + currentWave.enemyFormationPrefab.gameObject.name.ToString() + "So This enemys will not spawn");
            }
            //If there is enough space for enemy in the formation then spawing the enemy
            else
            {
                GameObject newEnemy = Instantiate(currentWave.EnemyPrefab, transform.position, Quaternion.identity) as GameObject; //Instantiating the Enemy Game Object
                Enemy enemyBehaviour = newEnemy.GetComponent<Enemy>(); //Getting the Enemy Script from Enemy Game Object               
                enemyBehaviour.SpawnSetup(currentWave.pathPrefab.GetComponent<Path>(), posInFormation, currentWave.enemyFormationPrefab.GetComponent<Formation>(), currentWave.enemySpeed, currentWave.enemyRotationSpeed);
                posInFormation++; 
                yield return new WaitForSeconds(currentWave.enemySpawnInterval);
            }
        }
        if (currentWave.enemyFormationPrefab.tag == "SmallEnemyFormation")
        {
           posInSmallEnemyFormation = posInFormation;
        }

        else if (currentWave.enemyFormationPrefab.tag == "MediumEnemyFormation")
        {
            // Debug.Log("Pos in medium enemy Formation");
           posInMediumEnemyFormation = posInFormation;
        }
        else if (currentWave.enemyFormationPrefab.tag == "BossEnemyFormation")
        {
           posInBossEnemyFormation = posInFormation;
        }
        yield return new WaitForSeconds(currentWave.secToWaitForNextWave);
    }
}