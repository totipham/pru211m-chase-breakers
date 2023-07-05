using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopBackgroundController : MonoBehaviour
{
    public float depth = 1;

    PlayerController player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float realVelocity = player.velocity.x / depth;
        Vector2 pos = transform.position;

        pos.x -= realVelocity * Time.fixedDeltaTime;

        if (pos.x <= -21.6f)
            pos.x = 21.6f;

        transform.position = pos;
    }
}
