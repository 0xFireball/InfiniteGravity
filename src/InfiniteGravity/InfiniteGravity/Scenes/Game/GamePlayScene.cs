using System;
using InfiniteGravity.Components.Camera;
using InfiniteGravity.Components.Characters;
using InfiniteGravity.Components.Misc;
using InfiniteGravity.Configuration;
using InfiniteGravity.Scenes.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Fuf;
using Nez.Fuf.Util;
using Nez.Sprites;
using Nez.Tiled;

namespace InfiniteGravity.Scenes.Game {
    public class GamePlayScene : BaseGameScene {
        private GameContext _gameContext;
        private string mapSource = "Maps";

        private const int renderlayer_backdrop = 257;
        private const int renderlayer_ui_overlay = 1024;
        private const int renderlayer_cursor_overlay = 1 << 31;

        private const int renderlayer_pause_overlay = 2048;

        private Renderer pauseRenderer;
        private bool paused = false;

        public override void initialize() {
            base.initialize();

            // data
            _gameContext = Core.services.GetService<GameContext>();

            // Hide cursor
            Core.instance.IsMouseVisible = false;

            clearColor = new Color(147, 140, 106); // temporary background color

            // add fixed renderer
            var fixedRenderer =
                addRenderer(new ScreenSpaceRenderer(1023, renderlayer_ui_overlay, renderlayer_cursor_overlay));
            fixedRenderer.shouldDebugRender = false;

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
            
            // dummy
            var dummy = createEntity("dummy", new Vector2(916, 560));
            dummy.addComponent<AntiRookie>();
            dummy.addComponent<LogicCharacterController>();

            // map
            var mapEntity = createEntity("map_tiles");
            var mapAsset = content.Load<TiledMap>($"{mapSource}/{_gameContext.map}");
            var mapComponent = mapEntity.addComponent(new TiledMapComponent(mapAsset, "blocks"));
            // map behind everything
            mapComponent.renderLayer = renderlayer_background;
            
            // add component to make camera follow the player
            var lockedCamera = camera.entity.addComponent(new LockedCamera(player, camera));

            pauseRenderer = new ScreenSpaceRenderer(255, renderlayer_pause_overlay);
            var pauseBackdrop = createEntity("pause_backdrop");
            var backdropTexture = Graphics.createSingleColorTexture(1, 1, new Color(100, 100, 100, 100));
//            Graphics.instance.batcher.draw(backdropTexture, new RectangleF(0, 0, Core.instance.defaultResolution.X, Core.instance.defaultResolution.Y));
            var backdropSprite = pauseBackdrop.addComponent(new Sprite(backdropTexture));
            backdropSprite.renderLayer = renderlayer_pause_overlay;
            backdropSprite.transform.scale = Core.instance.defaultResolution.ToVector2();
            backdropSprite.transform.position = Core.instance.defaultResolution.ToVector2() / 2;
            pauseRenderer.shouldDebugRender = false;

//            camera.zoom = -0.5f;
        }

        public override void update() {
            base.update();

            if (Input.isKeyPressed(Keys.Escape)) {
                if (paused) {
                    removeRenderer(pauseRenderer);
                    Time.timeScale = 1f;
                } else {
                    addRenderer(pauseRenderer);
                    Time.timeScale = 0f;
                }
                paused = !paused;
            }
        }
    }
}