using Microsoft.Xna.Framework.Graphics;
using Nez.Fuf.Sprites;

namespace InfiniteGravity.Components.Characters {
    public abstract class Character : FufAnimatedSprite<Character.Animations> {
        public enum Animations {
            Idle,
            Ready,
            Run,
            Melee1,
            Melee2,
            Melee3,
            Gun1,
            Hurt1,
        }

        public CharacterBody body;

        public Character(Texture2D texture, int width, int height) : base(texture, width, height) { }
    }
}