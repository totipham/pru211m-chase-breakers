using UnityEngine;

namespace Object {
    public abstract class ItemFactory : MonoBehaviour {
        
        public abstract Item CreateItem();
    }
}