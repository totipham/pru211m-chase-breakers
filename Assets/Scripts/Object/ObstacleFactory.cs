using UnityEngine;

namespace Object {
    public class ObstacleFactory : ItemFactory {
        public override Item CreateItem() {
            //Logic to create a new obstacle
            return new BoxObstacleItem();
        }
    }
}