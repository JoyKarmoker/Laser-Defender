using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewWave : MonoBehaviour
{
    public GameObject enemyPrefab;

    public virtual IEnumerator SpawnAllEnemies()
    {
        /*
        for (int enemyCount = 0; enemyCount < totalEnemysInThisWave; enemyCount++)
        {


            GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity) as GameObject; //Instantiating the Enemy Game Object
                                                                                                                   //totalEnemys++;
                                                                                                                   //GameObject newEnemy = objectPooler.SpawnFromPool(currentWave.EnemyPrefab.ToString(), transform.position, Quaternion.identity); //Instantiatiating The Game Object from Object Pooler
            Enemy enemyBehaviour = newEnemy.GetComponent<Enemy>(); //Getting the Enemy Script from Enemy Game Object               
            enemyBehaviour.SpawnSetup(pathPrefab.GetComponent<PathEnemy>(), enemyCount, enemyFormationPrefab.GetComponent<Formation>(), enemySpeed, enemyRotationSpeed);
            //posInFormation++;
            spawnedEnemys.Add(newEnemy);
            

        }
        */
        yield return new WaitForSeconds(1);
    }
}
