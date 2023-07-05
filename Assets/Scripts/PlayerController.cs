using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour {
    public Joystick joystick;
    private Rigidbody2D _rigid;
    private Camera _camera;

    public Vector2 velocity;
    public float jumpVelocity;

    //FIXME: I am dynamic change
    public float groundHeight = -2f;

    public float maxVelocity = 10f;
    // public float acceleration = 3f;
    // public float maxAcceleration = 10f;
    // public float climbSpeed = 5f;

    public bool isGrounded;
    public bool isFall;
    public bool isDead;
    public bool canClimb;
    public bool isClimbing;
    public bool isJumping;
    public bool isStopping;

    private RaycastHit2D _hit;

    void Start() {
        joystick = GetComponent<Joystick>();
        _rigid = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
        isGrounded = true;
        isFall = false;
        isDead = false;
        canClimb = false;
    }

    void Update() {
        if (isDead || isStopping) return;

        if (canClimb) {
            if (Input.GetKeyDown(KeyCode.UpArrow) || joystick.GetAxisX() > 0) {
                isClimbing = true;
            }
        } else {
            if (isGrounded) {
                isFall = false;
                isJumping = false;
                isClimbing = false;

                //Control: Up
                if (Input.GetKeyDown(KeyCode.UpArrow) || joystick.GetAxisX() > 0) {
                    isGrounded = false;
                    Jump();
                }

                //Control: Down
                if (Input.GetKeyDown(KeyCode.DownArrow) || joystick.GetAxisX() < 0) {
                    //TODO: Bow Down Animation
                }
            } else {
                //Control: Down
                if (Input.GetKeyDown(KeyCode.DownArrow) || joystick.GetAxisX() < 0) {
                    isFall = true;
                }
            }
        }
    }

    void FixedUpdate() {
        if (isStopping) return;
        
        //Player: Is climbing
        if (isClimbing && canClimb) {
            isGrounded = false;
            Climb();
        } else {
            _rigid.gravityScale = 10;
        }

        //Player: Is in the ground
        if (isGrounded) {
            Vector2 pos = transform.position;
            velocity.x = maxVelocity;

            _hit = Physics2D.Raycast(transform.position,
                new Vector3(1, 0.5f, 0), 2f);
            Debug.DrawRay(transform.position, new Vector3(1, 0.5f, 0), Color.green);

            if (_hit.collider) {
                if (_hit.collider.CompareTag("Ground")) {
                    canClimb = true;
                }
            } else {
                canClimb = false;
            }

            //Player: Die
            if (!IsVisibleFromCamera()) {
                isDead = true;
                Debug.Log("Is Dead: " + isDead);
            }

            transform.position = pos;
        } else //Player: Is in the air
        {
            //Player: Fall down
            if (isFall) {
                _rigid.gravityScale = 30;
            }
        }
    }

    void Jump() {
        Debug.Log("Action: JUMPING");
        isJumping = true;
        _rigid.velocity = Vector3.zero;
        _rigid.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
    }

    void Climb() {
        Debug.Log("Action: CLIMBING");
        
        velocity = Vector3.zero;
        _rigid.gravityScale = 0;

        Vector3 playerPos = transform.position;

        //Raycast check if player is in the ground
        RaycastHit2D climbHit = Physics2D.Raycast(playerPos,
            Vector2.right, 0.1f);

        Debug.DrawRay(playerPos, Vector2.right, Color.red);

        if (climbHit.collider) {
            //KEEP THIS HERE
        } else {
            transform.position = new Vector3(playerPos.x + 0.1f, playerPos.y, playerPos.z);
            canClimb = false;
            isGrounded = true;
        }

        transform.position = new Vector3(playerPos.x, playerPos.y + 2.1f, playerPos.z);
    }
    
    void StopRunning() {
        Debug.Log("Action: STOP RUNNING");
        velocity = Vector3.zero;
        isStopping = true;
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground")) {
            if (!isGrounded) {
                isGrounded = true;
                _rigid.gravityScale = 10;
            }

            //Raycast right
            RaycastHit2D groundHit = Physics2D.Raycast(transform.position, Vector2.right, 0.5f);

            if (groundHit.collider) {
                if (groundHit.collider.CompareTag("Ground") && !isClimbing) {
                    //If right have ground, do stopping
                    StopRunning();
                    
                    //Animation layoff
                }
            }
        }
    }

    bool IsVisibleFromCamera() {
        Vector3 viewportPosition =
            _camera.WorldToViewportPoint(transform
                .position);
        return viewportPosition.x >= 0 && viewportPosition.x <= 1 && viewportPosition.y >= 0 &&
               viewportPosition.y <= 1;
    }
}