using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeOneWave : Wave
{
    //public GameObject enemyFormationPrefab; 
    
    [Header("Enemy Properties")]
    public float enemySpeed = 10f;
    public float enemyRotationSpeed = 10f;
    public int totalEnemysInThisWave; //Should be less or equal than the total positions in enemyFormation Prefab
    public float enemySpawnInterval = 1f;

    [Header("Prefabs")]
    public GameObject flyInPathPrefab;
    public GameObject enemyFormationPrefab; //In which Formation This waves enemy will sit
    public List<KeyValuePair<TypeOneEnemy, float>> enemyAndIntervalList = new List<KeyValuePair<TypeOneEnemy, float>>(); 


    int totalPositionInTeFormation;
    [HideInInspector] public List<GameObject> spawnedEnemys = new List<GameObject>();
    ObjectPooler objectPooler;
    // Start is called before the first frame update
    void Start()
    {
        objectPooler = ObjectPooler.ObjectPullerInstance;
        if(objectPooler)
        {
            Debug.Log("Found Object Pooler");
        }
        totalPositionInTeFormation = enemyFormationPrefab.GetComponent<Formation>().gridSizeX * enemyFormationPrefab.GetComponent<Formation>().gridSizeY;
        if(totalEnemysInThisWave > totalPositionInTeFormation)
        {
            totalEnemysInThisWave = totalPositionInTeFormation; //If the total number of enemies is greater than the total position of formation
        }

        //enemyFormationPrefab.GetComponent<Formation>().StopActivateSpread();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*public override IEnumerator SpawnAllEnemies()
    {
        Debug.Log("Spawning waves");
        for (int enemyCount = 0; enemyCount < totalEnemysInThisWave; enemyCount++)
        {
            Debug.Log("Spawning all enemys in type one wave");
            
                GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity) as GameObject; //Instantiating the Enemy Game Object
                if(newEnemy)
            {
                Debug.Log("Found Enemy");
            }
                //totalEnemys++;
                //GameObject newEnemy = objectPooler.SpawnFromPool(enemyPrefab.ToString(), transform.position, Quaternion.identity); //Instantiatiating The Game Object from Object Pooler
                ///TypeOneEnemy enemyBehaviour = newEnemy.GetComponent<TypeOneEnemy>(); //Getting the Enemy Script from Enemy Game Object               
                //enemyBehaviour.SpawnSetup(flyInPathPrefab.GetComponent<Path>(), enemyCount, enemyFormationPrefab.GetComponent<Formation>(), enemySpeed, enemyRotationSpeed);
                //posInFormation++;
                spawnedEnemys.Add(newEnemy);
                yield return new WaitForSeconds(enemySpawnInterval);
            
        }
    }*/
}
