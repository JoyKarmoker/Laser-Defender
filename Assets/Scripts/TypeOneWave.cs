using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeOneWave : NewWave
{
    //public GameObject enemyFormationPrefab; 
    
    [Header("Enemy Properties")]
    public float enemySpeed = 10f;
    public float enemyRotationSpeed = 10f;
    public int totalEnemysInThisWave; //Should be less or equal than the total positions in enemyFormation Prefab
    public float enemySpawnInterval = 1f;

    [Header("Prefabs")]
    public GameObject pathPrefab;
    public GameObject enemyFormationPrefab; //In which Formation This waves enemy will sit


    int totalPositionInTeFormation;
    [HideInInspector] public List<GameObject> spawnedEnemys = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        totalPositionInTeFormation = enemyFormationPrefab.GetComponent<Formation>().gridSizeX * enemyFormationPrefab.GetComponent<Formation>().gridSizeY;
        if(totalEnemysInThisWave > totalPositionInTeFormation)
        {
            totalEnemysInThisWave = totalPositionInTeFormation; //If the total number of enemies is greater than the total position of formation
        }

        enemyFormationPrefab.GetComponent<Formation>().StopActivateSpread();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator SpawnAllEnemies()
    {
        for (int enemyCount = 0; enemyCount < totalEnemysInThisWave; enemyCount++)
        {
 
            
                GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity) as GameObject; //Instantiating the Enemy Game Object
                //totalEnemys++;
                //GameObject newEnemy = objectPooler.SpawnFromPool(currentWave.EnemyPrefab.ToString(), transform.position, Quaternion.identity); //Instantiatiating The Game Object from Object Pooler
                Enemy enemyBehaviour = newEnemy.GetComponent<Enemy>(); //Getting the Enemy Script from Enemy Game Object               
                enemyBehaviour.SpawnSetup(pathPrefab.GetComponent<PathEnemy>(), enemyCount, enemyFormationPrefab.GetComponent<Formation>(), enemySpeed, enemyRotationSpeed);
                //posInFormation++;
                spawnedEnemys.Add(newEnemy);
                yield return new WaitForSeconds(enemySpawnInterval);
            
        }
    }
}
