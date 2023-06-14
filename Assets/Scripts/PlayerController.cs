using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private SwipeControlManager swipeControlManager;

    public float gravity;
    public Vector2 velocity;
    public float jumpVelocity = 30f;
    public float groundHeight = -2f;
    public bool isGrounded = false;
    public bool isFall = false;

    // Start is called before the first frame update
    void Start()
    {
        swipeControlManager = GetComponent<SwipeControlManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow)) 
            // if (swipeControlManager.IsSwipingUp())
            {
                isGrounded = false;
                velocity.y = jumpVelocity;
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                //TODO: Bow Down Animation
                Debug.Log("HOLDDDDDDDDDDDDDDD");
            }
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.DownArrow))
            // if (swipeControlManager.IsSwipingDown())
            {
                isFall = true;
            }

            if (isFall)
            {
                pos.y += gravity * Time.deltaTime;
            }
            else
            {
                pos.y += velocity.y * Time.deltaTime;
                velocity.y += gravity * Time.deltaTime;
            }

            if (pos.y <= groundHeight)
            {
                pos.y = groundHeight;
                isGrounded = true;
                isFall = false;
            }
        }


        transform.position = pos;
    }

    // void FixedUpdate()
    // {
    //     if (!isGrounded)
    //     {
    //         if (isFall)
    //         {
    //             pos.y += gravity * Time.fixedDeltaTime;
    //         }
    //         else
    //         {
    //             pos.y += velocity.y * Time.fixedDeltaTime;
    //             velocity.y += gravity * Time.fixedDeltaTime;
    //         }
    //
    //
    //         if (pos.y <= groundHeight)
    //         {
    //             pos.y = groundHeight;
    //             isGrounded = true;
    //             isFall = false;
    //         }
    //     }
    //
    //     transform.position = pos;
    // }
}