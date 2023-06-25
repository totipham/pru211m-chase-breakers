using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Joystick joystick;
    private Rigidbody2D _rigid;
    private Camera _camera;

    public Vector2 velocity;
    public float jumpVelocity;

    //FIXME: I am dynamic change
    public float groundHeight = -2f;

    public float maxVelocity = 10f;
    public float acceleration = 3f;
    public float maxAcceleration = 10f;

    public bool isGrounded;
    public bool isFall;
    public bool isDead;

    void Start()
    {
        joystick = GetComponent<Joystick>();
        _rigid = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
        isGrounded = true;
        isFall = false;
        isDead = false;
    }

    void Update()
    {
        if (isDead)
        {
            return;
        }

        if (isGrounded)
        {
            isFall = false;

            //Control: Up
            if (Input.GetKeyDown(KeyCode.UpArrow) || joystick.GetAxisX() > 0)
            {
                isGrounded = false;
                Jump();
            }

            //Control: Down
            if (Input.GetKeyDown(KeyCode.DownArrow) || joystick.GetAxisX() < 0)
            {
                //TODO: Bow Down Animation
            }
        }
        else
        {
            //Control: Down
            if (Input.GetKeyDown(KeyCode.DownArrow) || joystick.GetAxisX() < 0)
            {
                isFall = true;
            }
        }
    }

    void FixedUpdate()
    {
        Vector2 pos = transform.position;
        // float velocityRatio = velocity.x / maxVelocity;

        if (isGrounded)
        {
            // acceleration = maxAcceleration * (1 - velocityRatio);
            // velocity.x += acceleration * Time.fixedDeltaTime;
            //
            // if (velocity.x > maxVelocity)
            // {
                velocity.x = maxVelocity;
            // }

            //Player: Die
            if (!IsVisibleFromCamera())
            {
                isDead = true;
                Debug.Log("Is Dead: " + isDead);
            }
        }
        else
        {
            //Player: Fall down
            if (isFall)
            {
                _rigid.gravityScale = 30;
            }

            // if (pos.y <= groundHeight)
            // {
            //     pos.y = groundHeight;
            //     isGrounded = true;
            //     _rigid.gravityScale = 10;
            // }
        }

        transform.position = pos;
    }
    
    bool IsVisibleFromCamera()
    {
        Vector3 viewportPosition =
            _camera.WorldToViewportPoint(transform
                .position);
        return viewportPosition.x >= 0 && viewportPosition.x <= 1 && viewportPosition.y >= 0 &&
               viewportPosition.y <= 1;
    }

    void Jump()
    {
        _rigid.velocity = Vector3.zero;
        _rigid.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") && !isGrounded)
        {
            isGrounded = true;
            _rigid.gravityScale = 10;
        }
    }
}