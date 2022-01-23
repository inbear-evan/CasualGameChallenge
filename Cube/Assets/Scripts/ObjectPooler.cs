using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler instance;
    private void Awake()
    {
        instance = this;
    }
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefabs;
        public int size;
    }
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();   
        
        foreach(Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for(int i = 0; i< pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefabs);
                obj.transform.parent = transform.parent;
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn.t excist");
            return null;
        }        

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        InterfacePool poolObj = objectToSpawn.GetComponent<InterfacePool>();
        if(poolObj != null)
        {
            poolObj.OnObjectSpawn();
        }
        poolDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }

    public GameObject SpawnFromPoolEffect(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn.t excist");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        if (objectToSpawn.GetComponent<ParticleSystem>().IsAlive())
        {
            objectToSpawn.SetActive(false);
        }
        

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        InterfacePool poolObj = objectToSpawn.GetComponent<InterfacePool>();
        if (poolObj != null)
        {
            poolObj.OnObjectSpawn();
        }
        poolDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }
}
