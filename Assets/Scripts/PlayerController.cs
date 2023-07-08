using UnityEngine;

public class PlayerController : MonoBehaviour {
    public Joystick joystick;
    private Rigidbody2D _rigid;
    private Camera _camera;

    public Vector2 velocity;
    public float jumpVelocity;

    //FIXME: I am dynamic change
    public float groundHeight = -2f;

    public float maxVelocity = 10f;

    public bool isGrounded;
    public bool isFall;
    public bool isDead;
    public bool canClimb;
    public bool isClimbing;
    public bool isJumping;
    public bool isStopping;

    private RaycastHit2D _hit;
    private RaycastHit2D _backHit;
    private Animator _animator;
    
    //UI
    public GameOverScreen gameOverScreen;

    void Start() {
        joystick = GetComponent<Joystick>();
        _rigid = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _camera = Camera.main;
        // isGrounded = true;
        isFall = false;
        isDead = false;
        canClimb = false;
    }

    void Update() {
        if (isDead || isStopping) {
            return;
        };

        // if (canClimb) {
        //     if (Input.GetKeyDown(KeyCode.UpArrow) || joystick.GetAxisX() > 0) {
        //         isClimbing = true;
        //     }
        // } else {
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
        // }
    }

    void FixedUpdate() {
        //Animation: Set Animation
        _animator.SetBool("Is Jumping", isJumping);
        _animator.SetBool("Is Falling", isFall);

        if (isStopping) {
            _animator.SetTrigger("Dead");
            GameOver();
            return;
        }
        
        //Player: Is climbing
        // if (isClimbing && canClimb) {
        //     isGrounded = false;
        //     Climb();
        // } else {
        //     _rigid.gravityScale = 10;
        // }

        //Player: Is in the ground
        if (isGrounded) {
            Vector2 pos = transform.position;
            velocity.x = maxVelocity;

            // _hit = Physics2D.Raycast(transform.position,
            //     new Vector3(1, 0.5f, 0), 1f);
            // Debug.DrawRay(transform.position, new Vector3(1, 0.5f, 0), Color.green);
            
            _backHit = Physics2D.Raycast(transform.position,
                new Vector2(0.35f, 1), 0.5f);
            Debug.DrawRay(transform.position, new Vector2(0.35f, 1), Color.yellow);

            // if (_hit.collider) {
            //     if (_hit.collider.CompareTag("Ground")) {
            //         canClimb = true;
            //     }
            // } else {
            //     canClimb = false;
            // }
            
            if (_backHit.collider) {
                if (_backHit.collider.CompareTag("Ground")) {
                    StopRunning();
                }
            } 

            //Player: Die
            if (!IsVisibleFromCamera()) {
                isDead = true;
                StopRunning();
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

    void GameOver() {
        gameOverScreen.Setup(10000); 
        gameObject.GetComponent<PlayerController>().enabled = false;
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
        Debug.Log("COLLIDE: " + other.gameObject.name);
        if (other.gameObject.CompareTag("Ground")) {
            if (!isGrounded) {
                isGrounded = true;
                _rigid.gravityScale = 10;
            }

            //Raycast right
            // RaycastHit2D groundHit = Physics2D.Raycast(transform.position, Vector2.right, 0.5f);

            // if (_hit.collider) {
            //     if (_hit.collider.CompareTag("Ground") && !isClimbing) {
                    //If right have ground, do stopping
                    // StopRunning();
                    
                    //Animation layoff
            //     }
            // }
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