using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class CapsuleSpawner : MonoBehaviour
{
    public GameObject[] capsulePrefabArray; //The Capsules that will be spawned by the spawner
    [Range(0,100)]
    public int capsuleSpawnProbabilty = 30; //Probabilty of capsule spawn
    private float capsuleSpeed = 5f;
    int capsulePrefabArrayLength;
     ObjectPooler objectPooler;

    public static CapsuleSpawner CapsuleSpawnerInstance;

    private void Awake()
    {
        if(CapsuleSpawnerInstance == null)
        {
            CapsuleSpawnerInstance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        objectPooler = ObjectPooler.ObjectPullerInstance;
        capsulePrefabArrayLength = capsulePrefabArray.Length;

    }

    /*
     This Method will be called by the enemy when it gets destroed
     */
    public void SpawnCapsule(GameObject enemy)
    {
        int thisTimeSpwanProbality = Random.Range(0, 101);
        if (thisTimeSpwanProbality <= capsuleSpawnProbabilty)
        {
            int capsuleToSpwanIndex = Random.Range(0, capsulePrefabArrayLength); //This is a random index from the capsule prefab Array
            GameObject randomCapsule = capsulePrefabArray[capsuleToSpwanIndex];
            GameObject capsule = objectPooler.SpawnFromPool(randomCapsule.ToString(), enemy.transform.position, Quaternion.identity);
            Rigidbody2D rigidbody = capsule.GetComponent<Rigidbody2D>();
            rigidbody.velocity = new Vector2(0, -capsuleSpeed);
        }
    }

}
