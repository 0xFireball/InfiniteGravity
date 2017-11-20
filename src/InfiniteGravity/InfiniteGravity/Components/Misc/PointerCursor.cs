using Microsoft.Xna.Framework;
using Nez.Fuf.Sprites;

namespace InfiniteGravity.Components.Misc {
    public class PointerCursor : FufDeferredSprite<PointerCursor.Animations> {
        private readonly int _renderLayer;
        public const int curSize = 16;

        public enum Animations {
            Base
        }

        public PointerCursor(int renderLayer) : base("cur_pointer") {
            _renderLayer = renderLayer;
        }

        protected override void loadSprites() {
            loadGraphic($"Sprites/Misc/{spriteAsset}", true, curSize, curSize);
            animation.renderLayer = _renderLayer;
            animation.setLocalOffset(new Vector2(curSize / 2f));
        }
    }
}