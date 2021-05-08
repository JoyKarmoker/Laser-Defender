using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [System.Serializable]
    public class SpawnSetup
    {
        public GameObject enemy;
        public float secToWait;
    }

    public SpawnSetup[] spawnSetup;
    

    ObjectPooler objectPooler;
    int posInTypeOneSpawner = 0;
    Vector2 spawningPos = new Vector2(0.0f, 17.0f);
    // Start is called before the first frame update
    void Start()
    {
        //posInTypeOneSpawner = 0;
        //objectPooler = ObjectPooler.ObjectPullerInstance;
        Debug.Log("First Wave");
        if (objectPooler)
        {
            Debug.Log("Found Object Pooler");
        }

    }


    public IEnumerator SpawnAllEnemies()
    {
        posInTypeOneSpawner = 0;
        objectPooler = ObjectPooler.ObjectPullerInstance;
        for (int enemyCount = 0; enemyCount <spawnSetup.Length; enemyCount++)
        {
            //Debug.Log("Spawning all enemys in type one wave");
            
            GameObject newEnemy = Instantiate(spawnSetup[enemyCount].enemy, transform.position, Quaternion.identity) as GameObject; //Instantiating the Enemy Game Object
            //Debug.Log(spawnSetup[enemyCount].enemy.ToString());
            //GameObject newEnemy = objectPooler.SpawnFromPool(spawnSetup[enemyCount].enemy.ToString(), spawningPos, Quaternion.identity); //Instantiatiating The Game Object from Object Pooler
            
            TypeOneEnemy typeOneEnemy = newEnemy.GetComponent<TypeOneEnemy>();
            if(typeOneEnemy)
            {
                typeOneEnemy.SetPositionInFormation(posInTypeOneSpawner);
                //Debug.Log("Pos In Type One Spawner  " + posInTypeOneSpawner);
                posInTypeOneSpawner++;
            }
            //totalEnemys++;
            //GameObject newEnemy = objectPooler.SpawnFromPool(enemyPrefab.ToString(), transform.position, Quaternion.identity); //Instantiatiating The Game Object from Object Pooler
            ///TypeOneEnemy enemyBehaviour = newEnemy.GetComponent<TypeOneEnemy>(); //Getting the Enemy Script from Enemy Game Object               
            //enemyBehaviour.SpawnSetup(flyInPathPrefab.GetComponent<Path>(), enemyCount, enemyFormationPrefab.GetComponent<Formation>(), enemySpeed, enemyRotationSpeed);
            //posInFormation++;
            //spawnedEnemys.Add(newEnemy);
            yield return new WaitForSeconds(spawnSetup[enemyCount].secToWait);

        }
    }
}
