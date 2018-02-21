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

        public BitmapFont AndinaBMFont { get; }

        public NezSpriteFont Andina { get; }

        public NezSpriteFont AndinaLarge { get; }

        public UiAssets() {
            PixeledBMFont = FufCore.contentSource.Load<BitmapFont>("Fonts/bm_pixeled");
            AndinaBMFont = FufCore.contentSource.Load<BitmapFont>("Fonts/bm_andina");

            Andina = new NezSpriteFont(FufCore.contentSource.Load<SpriteFont>("Fonts/andina"));
            AndinaLarge = new NezSpriteFont(FufCore.contentSource.Load<SpriteFont>("Fonts/andina_lg"));

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