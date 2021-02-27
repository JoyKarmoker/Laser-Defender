using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float secAfterEnemyStartSpawn = 3f;
    int currentWave = 0;
    int totalWaves;
    int posInFormation = 0;
    int posInSmallEnemyFormation = 0;
    int posInMediumEnemyFormation = 0;
    int posInBossEnemyFormation = 0;
    public bool inFormation;
    ObjectPooler objectPooler;


    [HideInInspector]public static List<GameObject> enemyFormationList = new List<GameObject>();
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

    [System.Serializable]
    public class SuperWave
    {
        public List<Wave> waveList = new List<Wave>();
    }
    public List<SuperWave> superWaveList = new List<SuperWave>();
    int currentSuperWave = 0;
    //public int superWave = 3;
    //public List<Wave> waveList = new List<Wave>();
    [HideInInspector] public List<GameObject> spawnedEnemys = new List<GameObject>();
    
    IEnumerator Start()
    {
        
        objectPooler = ObjectPooler.ObjectPullerInstance;
        //totalWaves = waveList.Count;
        yield return new WaitForSeconds(secAfterEnemyStartSpawn);
        StartSuperWave();
        //yield return StartCoroutine(SpawnAllWaves());
       // Invoke("CheckEnemyStates", 1f);
    }

    //When all the enemys are destroyed this will be called by the enemy script to start a new Super Wave
    public void StartSuperWave()
    {
        
        //enemyStartSpawnTime = enemyStartSpawnTime - Time.deltaTime;
        if (currentSuperWave<superWaveList.Count)
        {
            //inFormation = false;
            
            StartCoroutine(SpawnAllWaves());
            Invoke("CheckEnemyStates", 1f);

        }
    }

    /*private IEnumerator StartSuperWave()
    {
        if (currentSuperWave < superWaveList.Count)
        {
            //inFormation = false;
            spawnedEnemys.Clear();
            StartCoroutine(SpawnAllWaves());
            yield return new WaitForSeconds(secAfterEnemyStartSpawn);
            //Invoke("CheckEnemyStates", 1f);

        }
    }*/

    private IEnumerator SpawnAllWaves()
    {
        currentWave = 0;
        posInSmallEnemyFormation = 0;
        posInMediumEnemyFormation = 0;
        posInBossEnemyFormation = 0;
        spawnedEnemys.Clear();

        while (currentWave < superWaveList[currentSuperWave].waveList.Count)
        {
            yield return StartCoroutine(SpawnAllEnemiesInCurrentWave(superWaveList[currentSuperWave].waveList[currentWave]));
            //yield return StartCoroutine(SpawnAllEnemiesInCurrentWave(waveList[currentWave]));
            currentWave++;
        }
        //Invoke("CheckEnemyStates", 1f);
        currentSuperWave++;
    }

    private IEnumerator SpawnAllEnemiesInCurrentWave(Wave currentWave)
    {
        //total number of eneyms that can fit in this wave
        int totalEnemysInThisFormation = currentWave.enemyFormationPrefab.GetComponent<Formation>().gridSizeX * currentWave.enemyFormationPrefab.GetComponent<Formation>().gridSizeY;
        int totalFormatationInThisScene = enemyFormationList.Count;
        for (int i = 0; i < totalFormatationInThisScene; i++)
        {
            enemyFormationList[i].GetComponent<Formation>().StopActivateSpread();
        }
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
                //GameObject newEnemy = Instantiate(currentWave.EnemyPrefab, transform.position, Quaternion.identity) as GameObject; //Instantiating the Enemy Game Object
                //totalEnemys++;
                GameObject newEnemy = objectPooler.SpawnFromPool(currentWave.EnemyPrefab.ToString(), transform.position, Quaternion.identity); //Instantiatiating The Game Object from Object Pooler
                Enemy enemyBehaviour = newEnemy.GetComponent<Enemy>(); //Getting the Enemy Script from Enemy Game Object               
                enemyBehaviour.SpawnSetup(currentWave.pathPrefab.GetComponent<PathEnemy>(), posInFormation, currentWave.enemyFormationPrefab.GetComponent<Formation>(), currentWave.enemySpeed, currentWave.enemyRotationSpeed);
                posInFormation++;
                spawnedEnemys.Add(newEnemy);
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
        /*int totalFormatationInThisScene = enemyFormationList.Count;
        for (int i = 0; i < totalFormatationInThisScene; i++)
        {
            Debug.Log("Spread Started");
            enemyFormationList[i].GetComponent<Formation>().StartActivateSpread();
        }*/
    }

    void CheckEnemyStates()
    {
        inFormation = false;
        int totalSpawnedEnemys = spawnedEnemys.Count;
        for (int i = (totalSpawnedEnemys-1); i >=0; i--)
        {
            if(spawnedEnemys[i].GetComponent<Enemy>().enemyStates != Enemy.EnemyStates.IDLE)
            {
                inFormation = false;
                Invoke("CheckEnemyStates", 1f);
               // break;
                return;
            }
        }
        inFormation = true;

        if(inFormation)
        {
            //Start the active spreading coroutine in the spawner scripts for all the formation object present in the scene
            int totalFormatationInThisScene = enemyFormationList.Count;
            for (int i = 0; i <totalFormatationInThisScene; i++)
            {
                enemyFormationList[i].GetComponent<Formation>().StartActivateSpread();
            }
            CancelInvoke("CheckEnemyStates");
        }    
    }
}