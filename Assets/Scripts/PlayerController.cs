using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // public Joystick joystick;
    private Rigidbody2D _rigid;
    private Camera _camera;
    private SwipeControl _swipeLogic;

    public Vector2 velocity;
    public float jumpVelocity;
    public float distance = 0;

    public float maxVelocity = 10f;

    public bool isGrounded;
    public bool isFall;
    public bool isDead;
    public bool canClimb;
    public bool isClimbing;
    public bool isJumping;
    public bool isStopping;
    public bool isSlowDown;

    private RaycastHit2D _hit;
    private RaycastHit2D _backHit;
    private Animator _animator;

    //UI
    public GameOverScreen gameOverScreen;

    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource gameOverSound;
    [SerializeField] private AudioSource backgroundSound;
    [SerializeField] private AudioSource collideSound;

    void Start()
    {
        // joystick = GetComponent<Joystick>();
        _rigid = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _swipeLogic = GetComponent<SwipeControl>();
        _camera = Camera.main;
        // isGrounded = true;
        isFall = false;
        isDead = false;
        canClimb = false;
    }

    void Update()
    {
        SwipeControl.SwipeDirection direction = _swipeLogic.getSwipeDirection();
        if (isDead || isStopping)
        {
            return;
        }

        // if (canClimb) {
        //     if (Input.GetKeyDown(KeyCode.UpArrow) || joystick.GetAxisX() > 0) {
        //         isClimbing = true;
        //     }
        // } else {
        if (isGrounded)
        {
            isFall = false;
            isJumping = false;
            isClimbing = false;

            //Control: Up
            if (Input.GetKeyDown(KeyCode.UpArrow) || direction == SwipeControl.SwipeDirection.Jump)
            {
                isGrounded = false;
                jumpSound.Play();
                Jump();
            }

            //Control: Down
            if (Input.GetKeyDown(KeyCode.DownArrow) || direction == SwipeControl.SwipeDirection.Slide)
            {
                //TODO: Bow Down Animation
            }
        }
        else
        {
            //Control: Down
            if (Input.GetKeyDown(KeyCode.DownArrow) || direction == SwipeControl.SwipeDirection.Slide)
            {
                isFall = true;
            }
        }
        // }
    }

    void FixedUpdate()
    {
        //Animation: Set Animation
        _animator.SetBool("Is Jumping", isJumping);
        _animator.SetBool("Is Falling", isFall);

        if (isStopping)
        {
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
        if (isGrounded)
        {
            Vector2 pos = transform.position;
            velocity.x = maxVelocity;

            // _hit = Physics2D.Raycast(transform.position,
            //     new Vector3(1, 0.5f, 0), 1f);
            // Debug.DrawRay(transform.position, new Vector3(1, 0.5f, 0), Color.green);

            _backHit = Physics2D.Raycast(pos,
                new Vector2(0.35f, 1), 0.5f);
            Debug.DrawRay(pos, new Vector2(0.35f, 1), Color.yellow);

            // if (_hit.collider) {
            //     if (_hit.collider.CompareTag("Ground")) {
            //         canClimb = true;
            //     }
            // } else {
            //     canClimb = false;
            // }

            if (_backHit.collider)
            {
                if (_backHit.collider.CompareTag("Ground"))
                {
                    //Remove constraint X
                    _rigid.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                    StopRunning();
                }
            }
        }
        else //Player: Is in the air
        {
            //Player: Fall down
            if (isFall)
            {
                _rigid.gravityScale = 40;
            }
        }

        //Player: Die
        if (transform.position.y < -6)
        {
            isDead = true;
            StopRunning();
            Debug.Log("Is Dead: " + isDead);
        }

        //Player: dicstance
        distance += (velocity.x + Time.fixedDeltaTime) / 50;
    }

    void GameOver()
    {
        gameOverScreen.Setup(Mathf.FloorToInt(distance));
        gameObject.GetComponent<PlayerController>().enabled = false;
        gameOverSound.Play();
        backgroundSound.Stop();
    }

    public IEnumerator SlowDown(bool isCollide, float minusVelocity, float waitTime)
    {
        isSlowDown = true;
        if (isCollide)
        {
            Debug.Log("Animation: SLOW DOWN");
            _animator.SetTrigger("Collide");
            collideSound.Play();
        }
        maxVelocity -= minusVelocity;
        yield return new WaitForSeconds(waitTime);
        isSlowDown = false;
        maxVelocity += minusVelocity;
    }

    void Jump()
    {
        Debug.Log("Action: JUMPING");
        isJumping = true;
        _rigid.velocity = Vector3.zero;
        _rigid.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
    }

    void Climb()
    {
        Debug.Log("Action: CLIMBING");

        velocity = Vector3.zero;
        _rigid.gravityScale = 0;

        Vector3 playerPos = transform.position;

        //Raycast check if player is in the ground
        RaycastHit2D climbHit = Physics2D.Raycast(playerPos, Vector2.right, 0.1f);

        Debug.DrawRay(playerPos, Vector2.right, Color.red);

        if (climbHit.collider)
        {
            //KEEP THIS HERE
        }
        else
        {
            transform.position = new Vector3(playerPos.x + 0.1f, playerPos.y, playerPos.z);
            canClimb = false;
            isGrounded = true;
        }

        transform.position = new Vector3(playerPos.x, playerPos.y + 2.1f, playerPos.z);
    }

    void StopRunning()
    {
        Debug.Log("Action: STOP RUNNING");
        velocity = Vector3.zero;
        isStopping = true;
    }

    public void Dead()
    {
        isStopping = true;
        isDead = true;
        StopRunning();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("COLLIDE: " + other.gameObject.name);
        if (other.gameObject.CompareTag("Ground"))
        {
            if (!isGrounded)
            {
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

    bool IsVisibleFromCamera()
    {
        Vector3 viewportPosition =
            _camera.WorldToViewportPoint(transform
                .position);
        return viewportPosition.x >= 0 && viewportPosition.x <= 1 && viewportPosition.y >= 0 &&
               viewportPosition.y <= 1;
    }
}