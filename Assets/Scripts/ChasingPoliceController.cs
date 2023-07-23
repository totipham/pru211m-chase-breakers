using UnityEngine;

public class ChasingPoliceController : MonoBehaviour
{
    private Rigidbody2D _rigid;

    public float jumpVelocity;
    public bool isGrounded;

    private RaycastHit2D _hit;
    private RaycastHit2D _downHit;
    private Animator _animator;
    private PlayerController _player;

    [SerializeField] private AudioSource caughtSound;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _rigid = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_player.isDead) return;

        Vector2 pos = transform.position;
        _animator.SetBool("Is Jumping", !isGrounded);

        _hit = Physics2D.Raycast(pos, Vector2.right, 1.3f);
        Debug.DrawRay(pos, Vector2.right * 1.3f, Color.red);

        _downHit = Physics2D.Raycast(pos, new Vector2(0.4f, -1f), 1.3f);
        Debug.DrawRay(pos, new Vector2(0.4f, -1f) * 1.3f, Color.yellow);

        if (_hit.collider)
        {
            if (_hit.collider.gameObject.CompareTag("Ground"))
            {
                isGrounded = false;
                Jump();
            }
        }
        else
        {
            if (_downHit.collider)
            {
                if (_downHit.collider.gameObject.CompareTag("Ground"))
                {
                }
            }
            else
            {
                if (isGrounded)
                {
                    isGrounded = false;
                    Jump();
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (_player.isSlowDown)
        {
            Vector2 pos = transform.position;
            if (pos.x < _player.transform.position.x - 0.25f)
            {
                pos.x += 1.5f * Time.fixedDeltaTime;
            }
            transform.position = pos;
        }

    }

    void Jump()
    {
        _rigid.velocity = Vector3.zero;
        _rigid.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            if (!isGrounded)
            {
                isGrounded = true;
                _rigid.gravityScale = 10;
            }
        }

        if (other.gameObject.CompareTag("Player"))
        {
            _animator.SetTrigger("Attack");
            caughtSound.Play();
            _player.Dead();
        }

        if (other.gameObject.CompareTag("ObstaclePolice"))
        {
            other.gameObject.GetComponent<ArmedPoliceController>().isCollideWithPolice = true;

            //Passing through the obstacle
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}