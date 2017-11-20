using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.BitmapFonts;
using Nez.UI;

namespace InfiniteGravity.Assets {
    public class UiAssets {
        public TextButtonStyle TextButtonStyle { get; }

        public BitmapFont PixeledBMFont { get; }

        public NezSpriteFont DisposableDroidLarge { get; }

        public NezSpriteFont DisposableDroid { get; }

        public UiAssets() {
            DisposableDroidLarge = new NezSpriteFont(Core.content.Load<SpriteFont>("Fonts/disposable_droid_lg"));
            DisposableDroid = new NezSpriteFont(Core.content.Load<SpriteFont>("Fonts/disposable_droid"));

            PixeledBMFont = Core.content.Load<BitmapFont>("Fonts/bm_pixeled");

            const int buttonColorVal = 60;
            TextButtonStyle = new TextButtonStyle(
                new PrimitiveDrawable(new Color(buttonColorVal, buttonColorVal, buttonColorVal)),
                new PrimitiveDrawable(new Color(buttonColorVal, buttonColorVal, buttonColorVal, 204)),
                new PrimitiveDrawable(new Color(buttonColorVal, buttonColorVal, buttonColorVal, 153))) {
                downFontColor = Color.Gray,
                font = PixeledBMFont,
                fontColor = Color.GhostWhite,
                disabledFontColor = Color.DimGray
            };
        }
    }
}