using InfiniteGravity.Components.Misc;
using InfiniteGravity.Configuration;
using InfiniteGravity.Scenes.Base;
using Nez;
using Nez.Fuf;

namespace InfiniteGravity.Scenes.Game {
    public class GamePlayScene : BaseGameScene {
        private GameContext _gameContext;

        private const int renderlayer_backdrop = 65535;
        private const int renderlayer_ui_overlay = 1 << 30;
        private const int renderlayer_cursor_overlay = 1 << 31;

        public override void initialize() {
            base.initialize();

            // data
            _gameContext = Core.services.GetService<GameContext>();

            // Hide cursor
            Core.instance.IsMouseVisible = false;

            // add fixed renderer
            var fixedRenderer =
                addRenderer(new ScreenSpaceRenderer(int.MaxValue, renderlayer_ui_overlay, renderlayer_cursor_overlay));

            // ...and add custom cursor
            var targetCursor = createEntity("cursor");
            var cursorComponent = targetCursor.addComponent<TargetCursor>();
            cursorComponent.sprite.renderLayer = renderlayer_cursor_overlay;
            targetCursor.addComponent<MouseFollow>();
        }
    }
}