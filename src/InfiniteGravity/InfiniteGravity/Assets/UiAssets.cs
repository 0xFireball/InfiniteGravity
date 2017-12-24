using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.BitmapFonts;
using Nez.Fuf;
using Nez.UI;

namespace InfiniteGravity.Assets {
    public class UiAssets {
        public TextButtonStyle TextButtonStyle { get; }

        public BitmapFont PixeledBMFont { get; }

        public NezSpriteFont DisposableDroidLarge { get; }

        public NezSpriteFont DisposableDroid { get; }

        public UiAssets() {
            DisposableDroidLarge = new NezSpriteFont(FufCore.contentSource.Load<SpriteFont>("Fonts/disposable_droid_lg"));
            DisposableDroid = new NezSpriteFont(FufCore.contentSource.Load<SpriteFont>("Fonts/disposable_droid"));

            PixeledBMFont = FufCore.contentSource.Load<BitmapFont>("Fonts/bm_pixeled");

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