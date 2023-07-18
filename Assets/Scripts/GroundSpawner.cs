using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GroundSpawner : MonoBehaviour
{
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
    private bool canGenerateObstacle;
    private bool isGeneratedNextPlatform;

    private List<string> obstacleTypeList;


    IEnumerator Start()
    {
        canGenerateObstacle = false;
        _objectPooling = GetComponent<ObjectPooling>();
        _platformLength = _objectPooling.GetLengthByTag("Platform");
        _currentPlatform = GenerateStartPlatform();
        obstacleTypeList = _objectPooling.GetTypeListByTag("Obstacle");
        _obstacleLength = obstacleTypeList.Count;

        yield return new WaitForSeconds(5);
        canGenerateObstacle = true;

        // while (true) {
        //if next have generated, then generate obstacle

        // float spawnWait = Random.Range(minSpawnWait, maxSpawnWait);
        // yield return new WaitForSeconds(spawnWait);
        // }
    }

    void FixedUpdate()
    {
        //FIXME: Change appropriate position
        Vector2 pos = _currentPlatform.transform.position;
        isGeneratedNextPlatform = false;
        if (pos.x <= SPAWN_DISTANCE)
        {
            _currentPlatform = GenerateNextPlatform(new Vector2(pos.x + PLATFORM_GAP, pos.y));
            if (canGenerateObstacle && isGeneratedNextPlatform)
            {
                GenerateObstacle();
            }
        }
    }

    void GenerateObstacle()
    {
        if (_obstacleLength > 1)
        {
            int random = _obstacleLength == 1 ? 0 : Random.Range(0, _obstacleLength);
            string obstacleType = obstacleTypeList[random];
            Debug.Log("OBSTACLE: " + _currentPlatform.transform.name + " | " + obstacleType);


            int childCount = _currentPlatform.transform.childCount;
            if (childCount <= 1)
            {
                Debug.LogWarning("Platform has no area to spawn obstacle");
                return;
            }

            int randomPos = Random.Range(1, childCount);

            Transform areaTransform = _currentPlatform.transform.GetChild(randomPos).transform;
            Vector2 areaPos = areaTransform.position;
            float posX = areaPos.x;
            float posY = areaPos.y;
            float startPoint = posX;
            float endPoint = posX + areaTransform.localScale.x;
            

            Vector2 spawnPos = new Vector2(posX, posY + 1f);
            _objectPooling.SpawnFromPoolByType("Obstacle", spawnPos,
                Quaternion.identity, obstacleType);

        }
        else
        {
            Debug.LogWarning("Obstacle length is less than 1, must add more obstacles to the pool");
        }
    }

    private GameObject GenerateStartPlatform()
    {
        return _objectPooling.SpawnFromPoolByType("StartPlatform", new Vector2(-6, -4), Quaternion.identity, "Normal");
    }

    private GameObject GenerateNextPlatform(Vector2 nextPlatformPosition)
    {
        GameObject spawnPlatform = null;
        List<string> typeList = _objectPooling.GetTypeListByTag("Platform");


        // foreach (var type in typeList) {
        //     Debug.Log("Type: " + type + " is available");
        // }

        // Debug.Log("Type list length: " + typeList.Count);

        if (_platformLength > 0)
        {
            while (spawnPlatform == null)
            {
                var random = typeList.Count == 1 ? 0 : Random.Range(0, typeList.Count);
                string type = typeList[random];
                Debug.Log("OBSTACLE: " + "Type: " + type);
                spawnPlatform =
                    _objectPooling.SpawnFromPoolByType("Platform", nextPlatformPosition, Quaternion.identity, type);
            }
        }

        isGeneratedNextPlatform = true;

        return spawnPlatform;
    }
}