using Microsoft.Xna.Framework;

namespace InfiniteGravity.Components.Characters.Gear {
    public class Gun : Weapon {
        public Gun(Vector2 offset) : base(offset) { }

        public float range = 200f;
    }
}