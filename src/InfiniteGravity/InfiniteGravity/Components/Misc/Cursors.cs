using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez.Fuf;
using Nez.Fuf.Sprites;

namespace InfiniteGravity.Components.Misc {
    namespace RogueScientist.Components.Misc {
        public class PointerCursor : FufAnimatedSprite<PointerCursor.Animations> {
            public const int curSize = 16;

            public enum Animations {
                Base
            }

            public PointerCursor(int renderLayer) :
                base(FufCore.contentSource.Load<Texture2D>("Sprites/Misc/cur_pointer"), 16, 16) {
                animation.renderLayer = renderLayer;
                animation.setLocalOffset(new Vector2(curSize / 2f));
            }
        }

        public class TargetCursor : FufAnimatedSprite<TargetCursor.Animations> {
            public const int curSize = 16;

            public enum Animations {
                Base
            }

            public TargetCursor(int renderLayer) :
                base(FufCore.contentSource.Load<Texture2D>("Sprites/Misc/cur_target"), 16, 16) {
                animation.renderLayer = renderLayer;
                animation.setLocalOffset(new Vector2(curSize / 2f));
            }
        }
    }
}