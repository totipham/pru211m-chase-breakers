using UnityEngine;

namespace Object {
    public abstract class ObstacleItem : Item {
        public float slowDownVelocity = 5f;
        public float slowDownTime = 0.5f;
        private PlayerController _player;

        // Start is called before the first frame update
        void OnEnable() {
            _player = GameObject.Find("Player").GetComponent<PlayerController>();
            gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        void Drop() {
            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.down * 10f;
        }

        private void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.CompareTag("Player")) {
                Debug.Log("Hit by player");
            
                Vector3 hit = other.contacts[0].normal;
                float angle = Vector3.Angle(hit, Vector3.up);
                bool top = Mathf.Abs(180f - angle) < 1f;
                bool side = Mathf.Abs(90f - angle) < 1f;
            
                if (top) {
                    _player.isGrounded = true;
                    // Drop();
                }
            
                if (side) {
                    var slowDown = _player.SlowDown(true, slowDownVelocity, slowDownTime);
                    StartCoroutine(slowDown);
                }
            }

            if (other.gameObject.CompareTag("Police")) {
                Drop();
            }
        }
    }
}