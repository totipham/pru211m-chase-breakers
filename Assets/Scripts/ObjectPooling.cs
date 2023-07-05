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
        public string tag;
        public int activeObjectCount;
        public List<Quaternion> objectRotations;
    }

    public List<Pool> pools;

    private Dictionary<string, List<GameObject>> _poolDictionary;
    private Dictionary<string, List<string>> _typeDictionary;
    private List<GameObject> _poolList;
    private List<string> _typeList;
    private bool _isDuplicateDictionary;
    public static ObjectPooling Instance;

    void Awake() {
        Instance = this;
        _poolDictionary = new Dictionary<string, List<GameObject>>();
        _poolList = new List<GameObject>();
        _typeList = new List<string>();
        GenerateObjectInPool();
    }

    // // Start is called before the first frame update
    // void Start() {
    // }

    private void GenerateObjectInPool() {
        foreach (Pool pool in pools) {
            // List<GameObject> poolList = new List<GameObject>();

            for (int i = 0; i < pool.size; i++) {
                var prefab = pool.prefab;
                GameObject obj = Instantiate(prefab);
                obj.transform.parent = transform;
                obj.transform.name = $"{pool.tag}_{pool.type}";
                obj.SetActive(false);

                if (_isDuplicateDictionary) {
                    _poolDictionary[pool.tag].Add(obj);
                } else {
                    _poolList.Add(obj);
                }
            }

            if (!_isDuplicateDictionary) {
                _poolDictionary.Add(pool.tag, _poolList);
            }
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation) {
        if (!_poolDictionary.ContainsKey(tag)) {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        for (int i = 0; i < _poolDictionary[tag].Count; i++) {
            if (!_poolDictionary[tag][i].activeInHierarchy) {
                GameObject objectToSpawn = _poolDictionary[tag][i];

                objectToSpawn.SetActive(true);
                objectToSpawn.transform.position = position;
                objectToSpawn.transform.rotation = rotation;

                return objectToSpawn;
            }
        }

        return null;
    }

    public GameObject SpawnFromPoolByIndex(string tag, Vector3 position, Quaternion rotation, int index) {
        var getIndex = index;
        Debug.Log("Pooling inside: " + _poolDictionary);
        bool isExist = _poolDictionary.ContainsKey(tag);
        if (!isExist) {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        if (index >= _poolDictionary[tag].Count) {
            Debug.LogWarning("Index " + index + " is out of range.");
            return null;
        }

        //Check if active is true
        if (_poolDictionary[tag][index].activeInHierarchy) {
            Debug.LogWarning("Object at index " + index + " is already active.");

            getIndex = _poolDictionary[tag].Count + index;
            if (getIndex >= _poolDictionary[tag].Count) {
                //Duplicate pool
                _isDuplicateDictionary = true;
                GenerateObjectInPool();
            }
        }

        GameObject objectToSpawn = _poolDictionary[tag][getIndex];

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        return objectToSpawn;
    }

    public GameObject SpawnFromPoolByType(string tag, Vector3 position, Quaternion rotation, string type) {
        if (!_poolDictionary.ContainsKey(tag)) {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        for (int i = 0; i < _poolDictionary[tag].Count; i++) {
            if (!_poolDictionary[tag][i].activeInHierarchy && _poolDictionary[tag][i].name.Equals($"{tag}_{type}")) {
                GameObject objectToSpawn = _poolDictionary[tag][i];

                objectToSpawn.SetActive(true);
                objectToSpawn.transform.position = position;
                objectToSpawn.transform.rotation = rotation;

                return objectToSpawn;
            }
        }

        return null;
    }

    public List<string> GetTypeListByTag(string tag) {
        List<string> typeList = new List<string>();

        foreach (Pool pool in pools) {
            if (pool.tag.Equals(tag)) {
                typeList.Add(pool.type);
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

    public int GetLengthByTag(string tag) {
        if (!_poolDictionary.ContainsKey(tag)) {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return 0;
        }

        return _poolDictionary[tag].Count;
    }

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
}