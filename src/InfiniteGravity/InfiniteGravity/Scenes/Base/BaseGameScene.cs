using System;
using Microsoft.Xna.Framework;
using Nez;

namespace InfiniteGravity.Scenes.Base {
    public class BaseGameScene : Scene {
        protected const int renderlayer_background = 256;
        protected const int renderlayer_foreground = 0;

        private Color bgColor = new Color(40, 40, 40);

        protected bool _active = true;

        protected Renderer mainRenderer;

        public override void initialize() {
            base.initialize();

            // add a new renderer that renders layer 0 with renderOrder 0
            mainRenderer = addRenderer(new RenderLayerRenderer(0, renderlayer_background, renderlayer_foreground));
            mainRenderer.camera = camera;

            clearColor = bgColor;
        }

        protected void switchSceneFade<TScene>(float duration = 0.5f) where TScene : BaseGameScene, new() {
            if (_active && !Core.instance.inScreenTransition) {
                _active = false;
                Core.startSceneTransition(new CrossFadeTransition(() => new TScene()) {
                    fadeDuration = duration
                });
            }
        }
    }
}