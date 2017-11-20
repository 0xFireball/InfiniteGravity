using Nez.Fuf.Sprites;

namespace InfiniteGravity.Components.Misc {
    public class TargetCursor : FufDeferredSprite<TargetCursor.Animations> {
        public const int curSize = 16;

        public enum Animations {
            Base
        }

        public TargetCursor() : base("cur_target") { }

        protected override void loadSprites() {
            loadGraphic($"Sprites/Misc/{spriteAsset}", true, curSize, curSize);
        }
    }
}