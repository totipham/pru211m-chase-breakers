using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MoveObject : MonoBehaviour {
    // public float depth = 1;
    // public bool isSpawned;

    public float realVelocity;
    public bool isStopped;
    private PlayerController _player;

    // public float distanceThreshold = 5f;
    // private Vector2 lastEndPosition;
    // private GroundSpawner _platformSpawner;

    private void Start() {
        // isSpawned = false;
        // _platformSpawner = GameObject.Find("PlatformPooling").GetComponent<GroundSpawner>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnEnable() {
        // isSpawned = false;
        // GenerateNextPlatform();
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (isStopped) return;
        realVelocity = _player.velocity.x;
        Vector2 pos = transform.position;
        pos.x -= realVelocity * Time.fixedDeltaTime;
        
        // _rigid.velocity = new Vector2(-realVelocity, 0f);

        // Renderer objectRenderer = gameObject.GetComponentInChildren<Renderer>();
        // float rightmostPoint = objectRenderer.bounds.max.x;
        // Vector3 screenPoint = Camera.main.WorldToScreenPoint(new Vector3(rightmostPoint, 0f, 0f));
        // float distanceToScreenRight = Camera.main.pixelWidth - screenPoint.x;
        //
        // if (distanceToScreenRight >= distanceThreshold && !isSpawned)
        // {
        //     isSpawned = true;
        //     Debug.Log("Building: Can add now");
        // }

        if (pos.x <= -30) {
            gameObject.SetActive(false);
        }

        transform.position = pos;
    }

    // private void GenerateNextPlatform() {
    //     Debug.Log("Platform: Generating next platform, isSpawned: " + isSpawned);
    //     if (isSpawned) return;
    //
    //     Vector2 nextPlatformPosition = transform.position;
    //     nextPlatformPosition.x += 20;
    //
    //     isSpawned = true;
    //     Debug.Log("Platform: " + _platformSpawner.gameObject.name
    //     );
    //     // _platformSpawner.GenerateNextPlatform(nextPlatformPosition);
    // }
}