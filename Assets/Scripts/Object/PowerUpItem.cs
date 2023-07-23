using UnityEngine;

namespace Object {
    public class PowerUpItem : Item {
        private void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.CompareTag("Player")) {
                Debug.Log("POWERUP: Player hit powerup");
                // Drop();
                // var slowDown = _player.SlowDown(true, slowDownVelocity, effectTime);
                // StartCoroutine(slowDown);
            }
        
            if (other.gameObject.CompareTag("Police")) {
                // Drop();
            }
        }

        public override void Process() {
            throw new System.NotImplementedException();
        }
    }
}