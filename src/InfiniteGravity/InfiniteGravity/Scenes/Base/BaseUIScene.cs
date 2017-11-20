using InfiniteGravity.Components.Misc;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Fuf;

namespace InfiniteGravity.Scenes.Base {
    public class BaseUIScene : BaseGameScene {
        private const int renderlayer_cursor_overlay = 1 << 31;

        protected UICanvas canvas;
        protected Entity uiRoot;

        public override void initialize() {
            base.initialize();

            // Hide cursor
            Core.instance.IsMouseVisible = false;

            // ...and add custom cursor
            var overlayRenderer = addRenderer(new ScreenSpaceRenderer(int.MaxValue, renderlayer_cursor_overlay));
            var targetCursor = createEntity("cursor");
            targetCursor.addComponent(new PointerCursor(renderlayer_cursor_overlay));
            targetCursor.addComponent<MouseFollow>();

            uiRoot = createEntity("ui_root");
            canvas = uiRoot.addComponent<UICanvas>();
        }

        protected void setupGamepadInput(UICanvas canvas) {
            canvas.stage.keyboardEmulatesGamepad = true;
            canvas.stage.keyboardActionKey = Keys.E;
            canvas.stage.keyboardLeftKey = Keys.A;
            canvas.stage.keyboardRightKey = Keys.D;
            canvas.stage.keyboardUpKey = Keys.W;
            canvas.stage.keyboardDownKey = Keys.S;
        }
    }
}