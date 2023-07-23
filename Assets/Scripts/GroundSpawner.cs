using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GroundSpawner : MonoBehaviour
{
    private const float PlatformGap = 28f;
    private const float SpawnDistance = -5f;

    private int _obstacleLength;
    private int _lastObstaclePartIndex;
    private Vector2 _lastEndPosition;
    private ObjectPooling _objectPooling;
    private GameObject _currentPlatform;
    private bool _canGenerateObstacle;
    private bool _isGeneratedNextPlatform;
    public bool isContinueGame = false;

    private List<string> _obstacleNameList;


    IEnumerator Start()
    {
        _canGenerateObstacle = false;
        _objectPooling = GetComponent<ObjectPooling>();
        _obstacleNameList = _objectPooling.GetTypeListByTag("Obstacle");
        _obstacleLength = _obstacleNameList.Count;
        
        if (!isContinueGame) {
            _currentPlatform = GenerateStartPlatform();
            yield return new WaitForSeconds(5);
        } else {
            _currentPlatform = _objectPooling.GetFirstActiveGameObject("Platform");
        }
        
        _canGenerateObstacle = true;
    }

    void FixedUpdate()
    {
        //FIXME: Change appropriate position
        Vector2 pos = _currentPlatform.transform.position;
        _isGeneratedNextPlatform = false;
        if (pos.x <= SpawnDistance)
        {
            Debug.Log("Generate next platform");
            _currentPlatform = GenerateNextPlatform(new Vector2(pos.x + PlatformGap, pos.y));
            if (_canGenerateObstacle && _isGeneratedNextPlatform)
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
            string obstacleName = _obstacleNameList[random];
            Debug.Log("OBSTACLE: " + _currentPlatform.transform.name + " | " + obstacleName);


            int childCount = _currentPlatform.transform.childCount;
            if (childCount <= 1)
            {
                Debug.LogWarning("Platform has no area to spawn obstacle");
                return;
            }
            
            if (_currentPlatform.transform.name == "Platform_Ten" && obstacleName == "Obstacle_Police") {
                return;
            }

            //Generate obstacle at random position between Start and End point
            // int randomPos = Random.Range(1, childCount);
            Transform startTransform = _currentPlatform.transform.GetChild(1).transform;
            Transform endTransform = _currentPlatform.transform.GetChild(2).transform;
            
            Vector2 randomPosVector = new Vector2(Random.Range(startTransform.position.x, endTransform.position.x),
                Random.Range(startTransform.position.y, endTransform.position.y));
            
            float posX = randomPosVector.x;
            float posY = randomPosVector.y;

            // float posX = areaPos.x;
            // float posY = areaPos.y;
            // float startPoint = posX;
            // float endPoint = posX + areaTransform.localScale.x;
            

            Vector2 spawnPos = new Vector2(posX, posY + 1f);
            // _objectPooling.SpawnFromPoolByType("Obstacle", spawnPos,
            //     Quaternion.identity, obstacleType);
            _objectPooling.SpawnFromPool(obstacleName, spawnPos,
                Quaternion.identity);

        }
        else
        {
            Debug.LogWarning("Obstacle length is less than 1, must add more obstacles to the pool");
        }
    }

    private GameObject GenerateStartPlatform()
    {
        // return _objectPooling.SpawnFromPoolByType("StartPlatform", new Vector2(-6, -4), Quaternion.identity, "Normal");
        return _objectPooling.SpawnFromPool("StartPlatform_Normal", new Vector2(-6, -4), Quaternion.identity);
    }

    private GameObject GenerateNextPlatform(Vector2 nextPlatformPosition) {
        Debug.Log("Next Platform");
        GameObject spawnPlatform = null;
        // List<string> typeList = _objectPooling.GetTypeListByTag("Platform");
        List<string> objNameList = _objectPooling.GetTypeListByTag("Platform");

        if (objNameList.Count > 0)
        {
            while (spawnPlatform == null)
            {
                // var random = typeList.Count == 1 ? 0 : Random.Range(0, typeList.Count);
                var random = objNameList.Count == 1 ? 0 : Random.Range(0, objNameList.Count);
                // string type = typeList[random];
                string objName = objNameList[random];
                // Debug.Log("OBSTACLE: " + "Type: " + type);
                
                Debug.Log("Try to spawn: " + objName);
                
                spawnPlatform =
                    // _objectPooling.SpawnFromPoolByType("Platform", nextPlatformPosition, Quaternion.identity, type);
                    _objectPooling.SpawnFromPool(objName, nextPlatformPosition, Quaternion.identity);
            }
        }

        _isGeneratedNextPlatform = true;
        return spawnPlatform;
    }
}