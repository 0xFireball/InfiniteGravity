using InfiniteGravity.Scenes.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using Nez.Tweens;

namespace InfiniteGravity.Scenes {
    public class IntroScene : BaseGameScene {
#if DEBUG
        private const float intro_length = 1f;
#else
        private const float intro_length = 2f;
#endif

        public override void initialize() {
            base.initialize();

            // Hide cursor
            Core.instance.IsMouseVisible = false;

            clearColor = new Color(10);

            var coverTexture = content.Load<Texture2D>("petaphaser_cover");
            var cover = createEntity("cover", Core.instance.defaultResolution.ToVector2() / 2);
            var coverSprite = cover.addComponent(new Sprite(coverTexture));
            var targetWidth = Core.instance.defaultResolution.X * 0.7f;
            var baseScale = (int) (targetWidth / coverSprite.width);
            coverSprite.transform.localScale = new Vector2(baseScale);
            var coverBlend = new BlendState();
            coverBlend.AlphaSourceBlend =
                coverBlend.ColorSourceBlend = Blend.SourceAlpha;
            coverBlend.AlphaDestinationBlend =
                coverBlend.ColorDestinationBlend = Blend.One;
            coverSprite.material = new Material(coverBlend);

            coverSprite.color = Color.Transparent;
            coverSprite.tweenColorTo(Color.White, 0.4f)
                .setEaseType(EaseType.QuadIn)
                .setDelay(0.7f)
                .setCompletionHandler(t => {
                    coverSprite.transform.tweenLocalScaleTo(baseScale * 1.2f, 0.4f)
                        .setEaseType(EaseType.CubicOut)
                        .setDelay(intro_length)
                        .start();
                    coverSprite.tweenColorTo(Color.Transparent, 0.4f)
                        .setEaseType(EaseType.CubicOut)
                        .setDelay(intro_length)
                        .setCompletionHandler(_ => {
                            Core.startSceneTransition(new CrossFadeTransition(() => new MenuScene()) {
                                fadeDuration = 0.2f
                            });
                        }).start();
                })
                .start();
        }
    }
}