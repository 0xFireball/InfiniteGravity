using InfiniteGravity.Assets;
using InfiniteGravity.Configuration;
using InfiniteGravity.Scenes.Base;
using InfiniteGravity.Scenes.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Fuf;
using Nez.Fuf.UI;
using Nez.UI;

namespace InfiniteGravity.Scenes.Menu {
    public class GameSelectScene : BaseUIScene {
        public override void initialize() {
            base.initialize();

            var uiAssets = Core.services.GetService<UiAssets>();

            var infoText = new TextComposer(uiAssets.DisposableDroidLarge) {
                Text = "Game Select"
            }.attach(this, new Vector2(NGame.ViewportWidth / 2, 120), Color.WhiteSmoke, "info_text");
            infoText.updateOffsets(new Vector2(infoText.TextComponent.width / 2,
                                       infoText.TextComponent.height / 2) * -1);

            var table = canvas.stage.addElement(new Table());
            table.setFillParent(true);

            var gameContext = Core.services.GetService<GameContext>();

            var uiSkin = new Skin("UI/Skins/menu_skin", FufCore.contentSource);
            var menuButtonDimens = new Vector2(160, 42);
            var menuButtonPadding = 2;
            var menuGroupComposer =
                new MenuGroupComposer(uiSkin.get<TextButtonStyle>("pink_metal"), menuButtonDimens, menuButtonPadding);
            menuGroupComposer.Buttons.Add(new MenuGroupComposer.MenuGroupButton {
                Text = "START",
                Click = bt => { switchSceneFade<GamePlayScene>(); }
            });
            menuGroupComposer.Buttons.Add(new MenuGroupComposer.MenuGroupButton {
                Text = "CANCEL",
                Click = bt => { switchSceneFade<MenuScene>(); }
            });
            menuGroupComposer.attachTo(table);

            var textButtons = canvas.stage.findAllElementsOfType<TextButton>();
            canvas.stage.setGamepadFocusElement(textButtons[0]);
            setupGamepadInput(canvas);
        }

        public override void update() {
            if (Input.isKeyPressed(Keys.Escape) && _active) {
                switchSceneFade<MenuScene>();
            }

            base.update();
        }
    }
}