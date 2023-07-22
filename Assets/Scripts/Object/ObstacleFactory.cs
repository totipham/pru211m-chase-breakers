using UnityEngine;

namespace Object {
    public class ObstacleFactory : ItemFactory {
        public override Item GetItem(string item) {
            //Logic to create a new obstacle
            return new BoxObstacleItem();
        }
    }
}