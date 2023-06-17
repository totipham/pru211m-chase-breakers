using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float gravity;
    public Vector2 velocity;
    public float jumpVelocity = 30f;
    public float groundHeight = -2f;
    public bool isGrounded = false;
    public float acceleration = 10f;
    public float maxAcceleration = 10f;
    public float maxVelocity = 30f;
    public Joystick joystick;
    public float jumpSpeed = 2.2f;
    public bool isFall = false;

// Start is called before the first frame update
    void Start()
    {
        joystick = GetComponent<Joystick>();
    }

// Update is called once per frame
    void Update()
    {
        if (isGrounded)
        {
            isFall = false;
            if (Input.GetKeyDown(KeyCode.UpArrow) || joystick.GetAxisX() > 0)
            {
                isGrounded = false;
                velocity.y = jumpVelocity;
            }

            //If player want to bow down
            if (Input.GetKeyDown(KeyCode.DownArrow) || joystick.GetAxisX() < 0)
            {
                //TODO: Bow Down Animation
            }
        }
        else
        {
            //Fall down
            if (Input.GetKeyDown(KeyCode.DownArrow) || joystick.GetAxisX() < 0)
            {
                isFall = true;
            }
        }
    }

    void FixedUpdate()
    {
        Vector2 pos = transform.position;
        float velocityRatio = velocity.x / maxVelocity;

        if (isGrounded)
        {
            acceleration = maxAcceleration * (1 - velocityRatio); //a = a0 * (1 - v/v0)
            velocity.x += acceleration * Time.fixedDeltaTime; //v = a

            if (velocity.x >= maxVelocity)
            {
                velocity.x = maxVelocity;
            }
        }
        else
        {
            //Fall down
            if (isFall)
            {
                pos.y += gravity * Time.deltaTime * jumpSpeed;
            }
            else
            {
                pos.y += velocity.y * Time.fixedDeltaTime * velocity.x / maxVelocity * jumpSpeed;
                velocity.y += gravity * Time.fixedDeltaTime * velocity.x / maxVelocity * jumpSpeed;
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