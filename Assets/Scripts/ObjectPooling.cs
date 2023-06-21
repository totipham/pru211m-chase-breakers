using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    [Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;     
    }
    
    [Serializable]
    public class ObjectPoolingData
    {
        public List<PoolData> poolDataList;
    }

    [Serializable]
    public class PoolData
    {
        public string tag;
        public int activeObjectCount;
        public List<Quaternion> objectRotations;
    }


    public List<Pool> pools;

    public Dictionary<string, List<GameObject>> poolDictionary;
    public static ObjectPooling Instance;

    void Awake()
    {
        Instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<string, List<GameObject>>();
        
        foreach (Pool pool in pools)
        {
            List<GameObject> poolList = new List<GameObject>();
            
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                poolList.Add(obj);
            }
            
            poolDictionary.Add(pool.tag, poolList);
        }
    }
    
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }
        
        for (int i = 0; i < poolDictionary[tag].Count; i++)
        {
            if (!poolDictionary[tag][i].activeInHierarchy)
            {
                GameObject objectToSpawn = poolDictionary[tag][i];
                
                objectToSpawn.SetActive(true);
                objectToSpawn.transform.position = position;
                objectToSpawn.transform.rotation = rotation;
                
                return objectToSpawn;
            }
        }

        return null;
    }
    
    public void DestroyObject(string tag, GameObject objectToDestroy, float timeToDestroy)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return;
        }
        
        StartCoroutine(DestroyObjectCoroutine(tag, objectToDestroy, timeToDestroy));
    }
    
    IEnumerator DestroyObjectCoroutine(string tag, GameObject objectToDestroy, float timeToDestroy)
    {
        yield return new WaitForSeconds(timeToDestroy);
        
        for (int i = 0; i < poolDictionary[tag].Count; i++)
        {
            if (poolDictionary[tag][i] == objectToDestroy)
            {
                poolDictionary[tag][i].SetActive(false);
                yield break;
            }
        }
    }
}
