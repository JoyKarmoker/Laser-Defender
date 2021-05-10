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

    EnemySpawner enemySpawner;
    int posInTypeOneSpawner = 0;
    Formation typeOneEnemyFormation;

    public IEnumerator SpawnAllEnemies()
    {
        GameObject typeOneEnemyFormationObject = GameObject.Find("Type One Enemy Formation");
        typeOneEnemyFormation = typeOneEnemyFormationObject.GetComponent<Formation>();
        posInTypeOneSpawner = 0;
        enemySpawner = EnemySpawner.enemySpawnerInstance;

        //Stop the spreading of typeone enemyFormation
        if(typeOneEnemyFormation)
        {
            typeOneEnemyFormation.enemyInThisFormation.Clear(); // Clearing the formation list
            Debug.Log("Setting the spread od typeone enemy formation to false");
            typeOneEnemyFormation.spreadStarted = false;
        }

        for (int enemyCount = 0; enemyCount <spawnSetup.Length; enemyCount++)
        {
            //Debug.Log("Spawning all enemys in type one wave");
            
            GameObject newEnemy = Instantiate(spawnSetup[enemyCount].enemy, transform.position, Quaternion.identity) as GameObject; //Instantiating the Enemy Game Object
            
            TypeOneEnemy typeOneEnemy = newEnemy.GetComponent<TypeOneEnemy>();
            if(typeOneEnemy)
            {
                typeOneEnemy.SetPositionInFormation(posInTypeOneSpawner);
                //Debug.Log("Pos In Type One Spawner  " + posInTypeOneSpawner);
                posInTypeOneSpawner++;
            }
            enemySpawner.AddSpawnedEnemy(); //When a enemy is spawned the number of enemy increases that are currently present in the scene
            yield return new WaitForSeconds(spawnSetup[enemyCount].secToWait);

        }
        //Start THe Spreading of type one enemy formation
        if (typeOneEnemyFormation)
        {
            Debug.Log("Setting the spread od typeone enemy formation to true");
            typeOneEnemyFormation.spreadStarted = true;
        }
    }
}
