using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public float depth = 1;
    public bool isSpawned;

    PlayerController player;
    public float distanceThreshold = 5f;
    private Vector2 lastEndPosition;

    private void Awake()
    {
        isSpawned = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float realVelocity = player.velocity.x / depth;
        Vector2 pos = transform.position;
        pos.x -= realVelocity * Time.fixedDeltaTime;
        
        Renderer objectRenderer = gameObject.GetComponentInChildren<Renderer>();
        float rightmostPoint = objectRenderer.bounds.max.x;
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(new Vector3(rightmostPoint, 0f, 0f));
        float distanceToScreenRight = Camera.main.pixelWidth - screenPoint.x;
        if (distanceToScreenRight >= distanceThreshold && !isSpawned)
        {
            isSpawned = true;
            // gameObject.GetComponent<GroundSpawner>().AddRoof();
            Debug.Log("Building: Can add now");
        }
        
        if (pos.x <= -30)
        {
            pos.x = 20;
        }

        transform.position = pos;
    }
}