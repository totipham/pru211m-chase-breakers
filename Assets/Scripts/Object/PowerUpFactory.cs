namespace Object {
    public class PowerUpFactory : ItemFactory {
        public override Item CreateItem() {
            return new GhostPowerUpItem();
        }
    }
}