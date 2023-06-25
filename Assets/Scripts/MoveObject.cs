using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public float depth = 1;
    public bool isSpawned;

    PlayerController player;

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

        if (pos.x <= -2.5 && !isSpawned)
        {
            isSpawned = true;
            gameObject.GetComponent<GroundSpawner>().AddRoof();
        }

        if (pos.x <= -20)
        {
            Destroy(gameObject);
        }

        transform.position = pos;
    }
}