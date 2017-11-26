using Microsoft.Xna.Framework;
using Nez;

namespace InfiniteGravity.Components.Characters.Gear {
    public class Weapon : Component {
        public Vector2 offset;

        public Weapon(Vector2 offset) {
            this.offset = offset;
        }
    }
}