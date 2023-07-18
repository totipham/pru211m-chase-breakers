using UnityEngine;

namespace Object {
    public abstract class ObstacleItem : Item {
        public abstract void Drop();
        
        // public float slowDownVelocity = 5f;
        
        //
        // void Drop() {
        //     gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        //     gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.down * 10f;
        // }
        // private void OnCollisionEnter2D(Collision2D other) {
        //     if (other.gameObject.CompareTag("Player")) {
        //         Drop();
        //         var slowDown = player.SlowDown(true, slowDownVelocity, effectTime);
        //         StartCoroutine(slowDown);
        //     }
        //
        //     if (other.gameObject.CompareTag("Police")) {
        //         Drop();
        //     }
        // }
    }
}