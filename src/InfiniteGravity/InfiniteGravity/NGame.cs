using System.IO;
using FFAssetPack;
using InfiniteGravity.Assets;
using InfiniteGravity.Configuration;
using InfiniteGravity.Scenes;
using MGLayers;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Fuf;

namespace InfiniteGravity {
    public class NGame : FufCore {
        public const int ViewportWidth = 960;
        public const int ViewportHeight = 540;

        public const string GameTitle = "InfiniteGravity";
        public const string GameVersion = "0.0.2 dev";

        public const string PackedContentFile = "PackedContent.pak";

        private readonly GameContext gameContext;

        public NGame(GameContext context) : base(width: ViewportWidth, height: ViewportHeight,
            isFullScreen: context.configuration.graphics.fullscreen, windowTitle: GameTitle) {
            gameContext = context;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            base.Initialize();

            Window.Title = GameTitle;
            Window.AllowUserResizing = false;

//            Core.defaultSamplerState = gameContext.configuration.graphics.antialiasing ? SamplerState.LinearClamp : SamplerState.PointClamp;

            // Fixed timestep for physics updates
            IsFixedTimeStep = true;

            // Add content source for asset pak file
            if (File.Exists(PackedContentFile)) {
                contentSource.addContentSource(
                    new PakFileContentSource(new PakFile(File.OpenRead(PackedContentFile))), 0);
            }

            // Register code assets
            services.AddService(typeof(UiAssets), new UiAssets());

            // Register context service
            services.AddService(typeof(GameContext), gameContext);

            var resolutionPolicy = Scene.SceneResolutionPolicy.ShowAllPixelPerfect;
            if (gameContext.configuration.graphics.scaleMode ==
                GameConfiguration.GraphicsConfiguration.ScaleMode.Stretch) {
                resolutionPolicy = Scene.SceneResolutionPolicy.BestFit;
            }

            Scene.setDefaultDesignResolution(defaultResolution.X, defaultResolution.Y, resolutionPolicy);

            var introScene = new IntroScene();
            scene = introScene;
        }
    }
}