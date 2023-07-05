using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundSpawner : MonoBehaviour
{
    //variables visible in the inspector
    [Header("Spawn settings")] [Space(5)] public float maxSpawnWait;
    public float minSpawnWait;

    [Header("Add track parts here:")] public GameObject[] trackParts;

    [Header("Add roof parts here:")] public GameObject[] roofParts;

    //not visible in the inspector
    int lastParcourPart;

    private Vector2 lastEndPosition;
    
    // Start is called before the first frame update
    IEnumerator Start()
    {
        lastEndPosition = gameObject.GetComponentInChildren<Tilemap>().transform.position;
        yield return new WaitForSeconds(5);
        while (true)
        {
            SpawnNew();
            float spawnWait = Random.Range(minSpawnWait, maxSpawnWait);
            yield return new WaitForSeconds(spawnWait);
        }
    }
    
    void SpawnNew()
    {
        if (trackParts.Length > 0)
        {
            int random = 0;

            if (trackParts.Length == 1) {
                random = Random.Range(0, trackParts.Length);
            }
            
            if (lastParcourPart != random)
            {
                GameObject newParcour =
                    Instantiate(trackParts[random], transform.position, transform.rotation) as GameObject;
                newParcour.transform.parent = GameObject.Find("Spawner").transform;
                lastParcourPart = random;
            }
            else
            {
                SpawnNew();
            }
        }
    }
    
    
    public void AddRoof()
    {
        if (roofParts.Length <= 0) return;
        
        //get random roof and instantiate it as a child of the track object
        int random = Random.Range(0, roofParts.Length);
        // Vector3 pos = gameObject.transform.position;
        //
        // pos.x += 17f;

        // gameObject.transform.position = lastEndPosition;
        
        GameObject newRoof = Instantiate(roofParts[random], lastEndPosition, transform.rotation) as GameObject;
        newRoof.transform.parent = GameObject.Find("Spawner").transform;
    }

}
