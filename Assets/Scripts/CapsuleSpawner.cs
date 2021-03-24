using UnityEngine;

public class CapsuleSpawner : MonoBehaviour
{
    public GameObject[] capsulePrefabArray; //The Capsules that will be spawned by the spawner
    public int[] weightofCapsulesSerially; //Here you should give the weight of the capsule serially, ideally the weight should be in decreasing order and the total sum should be 100
    [Range(0,100)]
    public int capsuleSpawnProbabilty = 30; //Probabilty of capsule spawn
    private float capsuleSpeed = 5f;
    int capsulePrefabArrayLength;
    int capsuleToSpwanIndex;
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
            
            //Now Selecting the index from the capsule prefab array according to the weight
            int randomNumber = Random.Range(0, 101);
            for(int i=0; i<weightofCapsulesSerially.Length; i++)
            {
                if (randomNumber <= weightofCapsulesSerially[i])
                {
                    //Debug.Log("Capsule to spawn index " + capsuleToSpwanIndex);
                    capsuleToSpwanIndex = i;
                    break;
                }
                else
                {
                    randomNumber = randomNumber - weightofCapsulesSerially[i];
                }
                
            }
            //GameObject randomCapsule = capsulePrefabArray[capsuleToSpwanIndex];
            GameObject randomCapsule = capsulePrefabArray[2]; //This line is for checking individual capsules
            GameObject capsule = objectPooler.SpawnFromPool(randomCapsule.ToString(), enemy.transform.position, Quaternion.identity);
            Rigidbody2D rigidbody = capsule.GetComponent<Rigidbody2D>();
            rigidbody.velocity = new Vector2(0, -capsuleSpeed);
        }
    }

}
