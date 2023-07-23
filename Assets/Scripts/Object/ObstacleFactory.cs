using UnityEngine;

namespace Object {
    public class ObstacleFactory {
        public ObstacleItem GetItem(string item) {
            //Logic to create a new obstacle
            return new BoxObstacleItem();
        }
    }
}