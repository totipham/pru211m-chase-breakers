using UnityEngine;

namespace Object {
    public class PowerUpItem : Item {
        private void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.CompareTag("Player")) {
                // Drop();
                // var slowDown = _player.SlowDown(true, slowDownVelocity, effectTime);
                // StartCoroutine(slowDown);
            }
        
            if (other.gameObject.CompareTag("Police")) {
                // Drop();
            }
        }
    }
}