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
    public bool isGonnaClimb;

    private RaycastHit2D _hit;

    void Start()
    {
        joystick = GetComponent<Joystick>();
        _rigid = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
        isGrounded = true;
        isFall = false;
        isDead = false;
        isGonnaClimb = false;
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

                if (isGonnaClimb)
                {
                    Climb();
                }
                else
                {
                    Jump();
                }
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

        //Player: Is in the ground
        if (isGrounded)
        {
            velocity.x = maxVelocity;

            _hit = Physics2D.Raycast(transform.position,
                new Vector3(1, 0.5f, 0), 10f);

            // Debug.Log("Player can " + _hit.collider);
            Debug.DrawRay(transform.position, new Vector3(1, 0.5f, 0), Color.green);

            if (_hit.collider)
            {
                if (_hit.collider.CompareTag("Ground"))
                {
                    isGonnaClimb = true;
                    Debug.Log("Player can ABC: " + _hit.collider.tag);
                }

                Debug.Log("Player CAN climb");
            }
            else
            {
                Debug.Log("Player can't climb");
                isGonnaClimb = false;
            }

            //Player: Die
            if (!IsVisibleFromCamera())
            {
                isDead = true;
                Debug.Log("Is Dead: " + isDead);
            }
        }
        else //Player: Is in the air
        {
            //Player: Fall down
            if (isFall)
            {
                _rigid.gravityScale = 30;
            }
        }

        transform.position = pos;
    }

    void Jump()
    {
        Debug.Log("Jumping on the ground");
        _rigid.velocity = Vector3.zero;
        _rigid.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
    }

    void Climb()
    {
        Debug.Log("Action: Climbing on the ground");
        _rigid.velocity = Vector3.zero;
        // _rigid.AddForce(Vector2.up * 25f, ForceMode2D.Impulse);
        _rigid.AddForce(velocity * jumpVelocity, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") && !isGrounded)
        {
            isGrounded = true;
            _rigid.gravityScale = 10;
        }
    }

    bool IsVisibleFromCamera()
    {
        Vector3 viewportPosition =
            _camera.WorldToViewportPoint(transform
                .position);
        return viewportPosition.x >= 0 && viewportPosition.x <= 1 && viewportPosition.y >= 0 &&
               viewportPosition.y <= 1;
    }
}