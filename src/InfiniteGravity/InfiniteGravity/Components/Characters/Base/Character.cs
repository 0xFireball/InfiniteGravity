using Nez.Fuf.Sprites;

namespace InfiniteGravity.Components.Characters {
    public abstract class Character : FufDeferredSprite<Character.Animations> {
        public enum Animations {
            Idle,
            Ready,
            Run,
            Melee1,
            Melee2,
            Melee3,
            Gun1
        }

        public CharacterBody body;

        protected Character(string spriteAsset) : base(spriteAsset) { }

        public override void initialize() {
            base.initialize();
            
            // TODO
        }
    }
}