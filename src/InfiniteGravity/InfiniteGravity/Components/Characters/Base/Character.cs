using Nez.Fuf.Sprites;

namespace InfiniteGravity.Components.Characters {
    public abstract class Character : FufDeferredSprite<Character.Animations> {
        public enum Animations {
            Idle,
            Ready,
            Run
        }

        public CharacterBody body;

        protected Character(string spriteAsset) : base(spriteAsset) { }

        public override void initialize() {
            base.initialize();
            
            // TODO
        }
    }
}