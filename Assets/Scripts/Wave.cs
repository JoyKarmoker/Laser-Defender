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
    int posInTypeOneSpawner;
    // Start is called before the first frame update
    void Start()
    {
        objectPooler = ObjectPooler.ObjectPullerInstance;
        if (objectPooler)
        {
            Debug.Log("Found Object Pooler");
        }

    }


    public virtual IEnumerator SpawnAllEnemies()
    {
        Debug.Log("Spawning waves");
        for (int enemyCount = 0; enemyCount <spawnSetup.Length; enemyCount++)
        {
            Debug.Log("Spawning all enemys in type one wave");

            GameObject newEnemy = Instantiate(spawnSetup[enemyCount].enemy, transform.position, Quaternion.identity) as GameObject; //Instantiating the Enemy Game Object
            //GameObject newEnemy = objectPooler.SpawnFromPool(enemyPrefab.ToString(), transform.position, Quaternion.identity); //Instantiatiating The Game Object from Object Pooler
            TypeOneEnemy typeOneEnemy = newEnemy.GetComponent<TypeOneEnemy>();
            if(typeOneEnemy)
            {
                typeOneEnemy.SetPositionInFormation(posInTypeOneSpawner);
                Debug.Log("Found Type One Enemy");
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
