using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleSpawner : MonoBehaviour
{
    public GameObject[] capsulePrefabArray;
    private float capsuleSpeed = 5f;
     ObjectPooler objectPooler;
    // Start is called before the first frame update
    void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject thisCapsule in capsulePrefabArray)
        {
            if(isTimetoSpawn(thisCapsule))
            {
                Spawn(thisCapsule);
            }
        }
    }

    void Spawn(GameObject capsuleGameObject)
    {
       // GameObject capsule = Instantiate(capsuleGameObject) as GameObject;
        GameObject capsule = objectPooler.SpawnFromPool(capsuleGameObject.ToString(), this.transform.position, Quaternion.identity) as GameObject;
        Rigidbody2D rigidbody = capsule.GetComponent<Rigidbody2D>();
        rigidbody.velocity = new Vector2(0, -capsuleSpeed);
        //capsule.transform.position = this.transform.position;

    }

    bool isTimetoSpawn(GameObject capsuleGameObject)
    {
        Capsule capsule = capsuleGameObject.GetComponent<Capsule>();
        float meanSpawnDelay = capsule.seenEverySeconds;
        float spawnPerSeconds = 1/meanSpawnDelay;

        if(Time.deltaTime > meanSpawnDelay)
        {
            Debug.LogWarning("Spawn Rate capped by frame rate");
            return false;
        }

        float thershold = spawnPerSeconds * Time.deltaTime / 7;
        if(Random.value < thershold)
        {
            return true;
        }

        else
        {
            return false;
        }
        
    }
}
