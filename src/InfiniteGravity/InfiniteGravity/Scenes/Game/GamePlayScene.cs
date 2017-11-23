using System;
using InfiniteGravity.Components.Camera;
using InfiniteGravity.Components.Characters;
using InfiniteGravity.Components.Characters.Base;
using InfiniteGravity.Components.Misc;
using InfiniteGravity.Configuration;
using InfiniteGravity.Scenes.Base;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Fuf;
using Nez.Tiled;

namespace InfiniteGravity.Scenes.Game {
    public class GamePlayScene : BaseGameScene {
        private GameContext _gameContext;
        private string mapSource = "Maps";

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

            // sprites
            var player = createEntity("player",
                new Vector2(Core.instance.defaultResolution.X / 2f, y: Core.instance.defaultResolution.Y / 2f));
            player.addComponent<Rookie>();
            player.addComponent<PlayerCharacterController>();

            // map
            var mapEntity = createEntity("map_tiles");
            var mapAsset = content.Load<TiledMap>($"{mapSource}/{_gameContext.map}");
            var mapComponent = mapEntity.addComponent(new TiledMapComponent(mapAsset, "blocks"));
            // map behind everything
            mapComponent.renderLayer = renderlayer_background;
            
            // add component to make camera follow the player
            var lockedCamera = camera.entity.addComponent(new LockedCamera(player, camera));

            camera.zoom = -0.5f;
        }
    }
}