using Microsoft.Xna.Framework;

namespace InfiniteGravity.Components.Characters.Gear {
    public class MeleeWeapon : Weapon {
        public Rectangle reach;

        public MeleeWeapon(Vector2 offset, Rectangle reach) : base(offset) {
            this.reach = reach;
        }
    }
}