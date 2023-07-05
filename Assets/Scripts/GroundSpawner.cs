using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class GroundSpawner : MonoBehaviour {
    private const float PLATFORM_GAP = 28f;
    private const float SPAWN_DISTANCE = -5f;
    
    public float maxSpawnWait;
    public float minSpawnWait;

    private int _platformLength;
    private int _obstacleLength;
    private int _lastObstaclePartIndex;
    private Vector2 _lastEndPosition;
    private ObjectPooling _objectPooling;
    private GameObject _currentPlatform;

    IEnumerator Start() {
        _objectPooling = GetComponent<ObjectPooling>();
        _platformLength = _objectPooling.GetLengthByTag("Platform");
        _currentPlatform = GenerateNextPlatform(new Vector2(-6, -4));
        _obstacleLength = _objectPooling.GetLengthByTag("Obstacle");

        yield return new WaitForSeconds(5);

        // while (true) {
        //     GenerateObstacle();
        //     float spawnWait = Random.Range(minSpawnWait, maxSpawnWait);
        //     yield return new WaitForSeconds(spawnWait);
        // }
    }

    private void FixedUpdate() {
        //FIXME: Change appropriate position
        Vector2 pos = _currentPlatform.transform.position;
        if (pos.x <= SPAWN_DISTANCE) {
            _currentPlatform = GenerateNextPlatform(new Vector2(pos.x + PLATFORM_GAP, pos.y));
        }
    }

    void GenerateObstacle() {
        if (_obstacleLength > 1) {
            int random = _obstacleLength == 1 ? 0 : Random.Range(0, _obstacleLength);
            while (_lastObstaclePartIndex != random) {
                random = _obstacleLength == 1 ? 0 : Random.Range(0, _obstacleLength);
                GameObject obstacle = _objectPooling.SpawnFromPoolByIndex("Obstacle", transform.position, Quaternion.identity, random);
                obstacle.transform.parent = transform;
                _lastObstaclePartIndex = random;
            }
        } else {
            Debug.LogWarning("Obstacle length is less than 1, must add more obstacles to the pool");
        }
    }


    private GameObject GenerateNextPlatform(Vector2 nextPlatformPosition) {
        GameObject spawnPlatform = null;
        List<string> typeList = _objectPooling.GetTypeListByTag("Platform");
        
        if (_platformLength > 0) {
            while (spawnPlatform == null) {
                var random = typeList.Count == 1 ? 0 : Random.Range(0, typeList.Count);
                string type = typeList[random];
                spawnPlatform =
                    _objectPooling.SpawnFromPoolByType("Platform", nextPlatformPosition, Quaternion.identity, type);
            }
            
        }

        return spawnPlatform;
    }
}