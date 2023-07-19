using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

public class ObjectPooling : MonoBehaviour {
    [Serializable]
    public class Pool {
        public string tag;
        public string type;
        public GameObject prefab;
        public int size;
    }

    [Serializable]
    public class ObjectPoolingData {
        public List<PoolData> poolDataList;
    }

    [Serializable]
    public class PoolData {
        public string objName;
        public int activeObjectCount;
        public List<SerializableVector> objectPositions;
        public List<Quaternion> objectRotations;
    }

    public List<Pool> pools;

    private Dictionary<string, List<GameObject>> _poolDictionary;
    private Dictionary<string, List<string>> _typeDictionary;
    private List<string> _typeList;
    public static ObjectPooling Instance;

    void Awake() {
        Instance = this;
        _poolDictionary = new Dictionary<string, List<GameObject>>();
        _typeList = new List<string>();
        GenerateObjectInPool();
    }

    // // Start is called before the first frame update
    // void Start() {
    // }

    private void GenerateObjectInPool() {
        foreach (Pool pool in pools) {
            List<GameObject> _poolList = new List<GameObject>();
            String objName = $"{pool.tag}_{pool.type}";

            for (int i = 0; i < pool.size; i++) {
                GameObject obj = Instantiate(pool.prefab);
                obj.transform.parent = transform;
                obj.transform.name = objName;
                obj.SetActive(false);

                if (_poolDictionary.ContainsKey(objName)) {
                    _poolDictionary[objName].Add(obj);
                } else {
                    _poolList.Add(obj);
                }
            }

            if (!_poolDictionary.ContainsKey(objName)) {
                _poolDictionary.Add(objName, _poolList);
            }
        }
    }

    public GameObject SpawnFromPool(string objName, Vector3 position, Quaternion rotation) {
        if (!_poolDictionary.ContainsKey(objName)) {
            Debug.LogWarning("Pool with name " + objName + " doesn't exist.");
            return null;
        }
    
        for (int i = 0; i < _poolDictionary[objName].Count; i++) {
            if (!_poolDictionary[objName][i].activeInHierarchy) {
                GameObject objectToSpawn = _poolDictionary[objName][i];
    
                objectToSpawn.SetActive(true);
                objectToSpawn.transform.position = position;
                objectToSpawn.transform.rotation = rotation;
    
                return objectToSpawn;
            }
        }
        return null;
    }
    
    public GameObject GetFirstActiveGameObject(String tag) {
        //Loop through all object in pool check if name start by tag string
        foreach (KeyValuePair<string, List<GameObject>> entry in _poolDictionary) {
            if (entry.Key.StartsWith(tag)) {
                foreach (GameObject obj in entry.Value) {
                    if (obj.activeInHierarchy) {
                        return obj;
                    }
                }
            }
        }
        return null;
    }

    // public GameObject SpawnFromPoolByIndex(string tag, Vector3 position, Quaternion rotation, int index) {
    //     var getIndex = index;
    //     Debug.Log("Pooling inside: " + _poolDictionary);
    //     bool isExist = _poolDictionary.ContainsKey(tag);
    //     if (!isExist) {
    //         Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
    //         return null;
    //     }
    //
    //     if (index >= _poolDictionary[tag].Count) {
    //         Debug.LogWarning("Index " + index + " is out of range.");
    //         return null;
    //     }
    //
    //     //Check if active is true
    //     if (_poolDictionary[tag][index].activeInHierarchy) {
    //         Debug.LogWarning("Object at index " + index + " is already active.");
    //
    //         getIndex = _poolDictionary[tag].Count + index;
    //         if (getIndex >= _poolDictionary[tag].Count) {
    //             //Duplicate pool
    //             GenerateObjectInPool();
    //         }
    //     }
    //
    //     GameObject objectToSpawn = _poolDictionary[tag][getIndex];
    //
    //     objectToSpawn.SetActive(true);
    //     objectToSpawn.transform.position = position;
    //     objectToSpawn.transform.rotation = rotation;
    //
    //     return objectToSpawn;
    // }

    // public GameObject SpawnFromPoolByType(string tag, Vector3 position, Quaternion rotation, string type) {
    //     String objName = $"{tag}_{type}";
    //     if (!_poolDictionary.ContainsKey(objName)) {
    //         Debug.LogWarning("Pool with tag " + objName + " doesn't exist.");
    //         return null;
    //     }
    //
    //     for (int i = 0; i < _poolDictionary[objName].Count; i++) {
    //         if (!_poolDictionary[objName][i].activeInHierarchy) {
    //             
    //             //Check if _poolDictionary[tag][i] is not null
    //             if (_poolDictionary[objName][i] == null) {
    //                 Debug.LogWarning("Object at index " + i + " is null.");
    //                 return null;
    //             }
    //             
    //             GameObject objectToSpawn = _poolDictionary[objName][i];
    //
    //             objectToSpawn.SetActive(true);
    //             objectToSpawn.transform.position = position;
    //             objectToSpawn.transform.rotation = rotation;
    //
    //             return objectToSpawn;
    //         }
    //     }
    //
    //     return null;
    // }

    public List<string> GetTypeListByTag(string tag) {
        List<string> typeList = new List<string>();

        foreach (Pool pool in pools) {
            if (pool.tag.Equals(tag)) {
                typeList.Add($"{pool.tag}_{pool.type}");
            }
        }

        return typeList;
    }

    // public void DestroyObject(string tag, GameObject objectToDestroy, float timeToDestroy) {
    //     if (!poolDictionary.ContainsKey(tag)) {
    //         Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
    //         return;
    //     }
    //
    //     StartCoroutine(DestroyObjectCoroutine(tag, objectToDestroy, timeToDestroy));
    // }

    // public void DestroyObjectByIndex(string tag, int index, float timeToDestroy) {
    //     if (!poolDictionary.ContainsKey(tag)) {
    //         Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
    //         return;
    //     }
    //
    //     if (index >= poolDictionary[tag].Count) {
    //         Debug.LogWarning("Index " + index + " is out of range.");
    //         return;
    //     }
    //
    //     StartCoroutine(DestroyObjectCoroutine(tag, poolDictionary[tag][index], timeToDestroy));
    // }

    // public int GetLengthByTag(string tag) {
    //     if (!_poolDictionary.ContainsKey(tag)) {
    //         Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
    //         return 0;
    //     }
    //
    //     return _poolDictionary[tag].Count;
    // }

    // IEnumerator DestroyObjectCoroutine(string tag, GameObject objectToDestroy, float timeToDestroy) {
    //     yield return new WaitForSeconds(timeToDestroy);
    //
    //     for (int i = 0; i < poolDictionary[tag].Count; i++) {
    //         if (poolDictionary[tag][i] == objectToDestroy) {
    //             poolDictionary[tag][i].SetActive(false);
    //             yield break;
    //         }
    //     }
    // }
    public void SaveObjectPooling (string fileName)
    {
        ObjectPoolingData objectPoolingData = new ObjectPoolingData();
        objectPoolingData.poolDataList = new List<PoolData>();

        foreach (Pool pool in pools)
        {
            string objName = $"{pool.tag}_{pool.type}";
            PoolData poolData = new PoolData();
            // poolData.type = pool.type;
            // poolData.activeObjectCount = GetActiveObjectCount(pool.tag);
            poolData.objName = objName;
            poolData.activeObjectCount = GetActiveObjectCount(objName);
            
            poolData.objectPositions = new List<SerializableVector>();
            poolData.objectRotations = new List<Quaternion>();

            foreach (GameObject obj in _poolDictionary[objName])
            {
                if (obj.activeInHierarchy)
                {
                    poolData.objectPositions.Add(obj.transform.position);
                    poolData.objectRotations.Add(obj.transform.rotation);
                }
            }

            objectPoolingData.poolDataList.Add(poolData);
        }

        string jsonData = JsonUtility.ToJson(objectPoolingData, true);
        System.IO.File.WriteAllText(fileName, jsonData);

        Debug.Log("Game saved to: " + fileName);
    }

    private int GetActiveObjectCount(string objName)
    {
        int count = 0;

        if (_poolDictionary.ContainsKey(objName))
        {
            foreach (GameObject obj in _poolDictionary[objName])
            {
                if (obj.activeInHierarchy)
                {
                    count++;
                }
            }
        }

        return count;
    }
    
    public void LoadGame(string fileName)
    {
        if (System.IO.File.Exists(fileName))
        {
            string jsonData = System.IO.File.ReadAllText(fileName);
            ObjectPoolingData objectPoolingData = JsonUtility.FromJson<ObjectPoolingData>(jsonData);

            if (objectPoolingData != null)
            {
                foreach (PoolData poolData in objectPoolingData.poolDataList)
                {
                    if (_poolDictionary.ContainsKey(poolData.objName))
                    {
                        foreach (GameObject obj in _poolDictionary[poolData.objName])
                        {
                            obj.SetActive(false);
                        }

                        for (int i = 0; i < poolData.activeObjectCount; i++)
                        {
                            GameObject obj = SpawnFromPool(poolData.objName, Vector3.zero, Quaternion.identity);
                            if (obj != null && i < poolData.objectPositions.Count && i < poolData.objectRotations.Count)
                            {
                                obj.transform.position = poolData.objectPositions[i];
                                obj.transform.rotation = poolData.objectRotations[i];
                            }
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Pool with type " + poolData.objName + " doesn't exist.");
                    }
                }

                Debug.Log("Game loaded from: " + fileName);
            }
            else
            {
                Debug.LogWarning("Failed to load game data from: " + fileName);
            }
        }
        else
        {
            Debug.LogWarning("File not found: " + fileName);
        }
    }
}