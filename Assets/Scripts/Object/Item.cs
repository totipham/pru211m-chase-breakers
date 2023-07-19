using UnityEngine;

namespace Object {
    public abstract class Item : MonoBehaviour {
        protected PlayerController player;
        public float effectTime = 0.5f;

        void OnEnable() {
            player = GameObject.Find("Player").GetComponent<PlayerController>();
            gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

    }
}