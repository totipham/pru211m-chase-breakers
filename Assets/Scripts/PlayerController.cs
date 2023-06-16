using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float gravity;
    public float gravityScale = 1.0f;
    public Vector2 velocity;
    public float jumpVelocity = 30f;
    public float jumpVelocityScale = 1.0f;
    public float groundHeight = -2f;
    public bool isGrounded = false;
    public bool isFall = false;
    public float acceleration = 10f;
    public float maxAcceleration = 10f;
    public float maxVelocity = 100f;
    public Joystick joystick;

    public float initialJumpHeight;
    public float maxJumpHeight = 5.0f;

// Start is called before the first frame update
    void Start()
    {
        joystick = GetComponent<Joystick>();
    }

// Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || joystick.direction.y > 0)
            {
                isGrounded = false;

                // Increase the jump velocity based on the x velocity
                jumpVelocityScale = 1.0f + Mathf.Pow(velocity.x / maxVelocity, 4);

                velocity.y = jumpVelocity * jumpVelocityScale;

                // Store the initial jump height
                initialJumpHeight = pos.y;
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) || joystick.direction.y < 0)
            {
                //TODO: Bow Down Animation
                Debug.Log("HOLDDDDDDDDDDDDDDD");
            }

            float velocityRatio = velocity.x / maxVelocity;
            acceleration = maxAcceleration * (1 - velocityRatio);
            velocity.x += acceleration * Time.deltaTime;

            if (velocity.x >= maxVelocity)
            {
                velocity.x = maxVelocity;
            }
        }
        else
        {
            // Increase the gravity scale based on the x velocity
            gravityScale = 1.0f + Mathf.Pow(velocity.x / maxVelocity, 4);

            if (Input.GetKeyDown(KeyCode.DownArrow) || joystick.direction.y < 0)
            {
                pos.y += gravity * gravityScale * Time.deltaTime;
            }
            else
            {
                pos.y += velocity.y * Time.deltaTime;

                velocity.y += gravity * gravityScale * Time.deltaTime;
            }

            if (pos.y <= groundHeight)
            {
                pos.y = groundHeight;
                isGrounded = true;
            }
        }

        transform.position = pos;
    }
}