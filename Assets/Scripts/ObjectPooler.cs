using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public Dictionary<string, Queue<GameObject>> poolDictionary;


    [System.Serializable]
    public class Pool
    {
        private string tag;
        public GameObject prefab;
        public int size;

        public void SetTag(string givenTag)
        {
            tag = givenTag;
        }
        public string GetTag()
        {
            return tag;
        }
    }

    public static ObjectPooler ObjectPullerInstance;
    private void Awake()
    {
        if (ObjectPullerInstance == null)
            ObjectPullerInstance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public List<Pool> pools;
    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach(Pool pool in pools)
        {
            //pool.tag = 
            Queue<GameObject> objectPool =  new Queue<GameObject>();

            for(int i =0; i<pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            
            pool. SetTag(pool.prefab.ToString());
            poolDictionary.Add(pool.GetTag(), objectPool);
        }

    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if(!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("The Key Does not exist");
            return null;
        }
        GameObject objectToSpwan = poolDictionary[tag].Dequeue();
        objectToSpwan.SetActive(true);
        objectToSpwan.transform.position = position;
        objectToSpwan.transform.rotation = rotation;
        poolDictionary[tag].Enqueue(objectToSpwan);
        return objectToSpwan;
    }

}
