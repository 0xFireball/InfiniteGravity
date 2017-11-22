using InfiniteGravity.Assets;
using InfiniteGravity.Configuration;
using InfiniteGravity.Scenes;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Fuf;

namespace InfiniteGravity {
    public class NGame : FufCore {
        public const int ViewportWidth = 960;
        public const int ViewportHeight = 540;

        public const string GameTitle = "InfiniteGravity";
        public const string GameVersion = "0.1.001 dev";

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
            
            Core.defaultSamplerState = SamplerState.AnisotropicClamp;

            // Fixed timestep for physics updates
            IsFixedTimeStep = true;

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