using InfiniteGravity.Assets;
using InfiniteGravity.Scenes.Base;
using InfiniteGravity.Scenes.Menu;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Fuf;
using Nez.Fuf.UI;
using Nez.Tweens;
using Nez.UI;

namespace InfiniteGravity.Scenes {
    public class MenuScene : BaseUIScene {

        public override void initialize() {
            base.initialize();

            clearColor = new Color(35, 35, 35);

            var uiAssets = Core.services.GetService<UiAssets>();

            var titleTextCom = new TextComposer(uiAssets.DisposableDroidLarge) {
                Text = NGame.GameTitle
            }.attach(this, new Vector2(NGame.ViewportWidth / 2f, 120), new Color(125, 74, 94), "title_text");
            titleTextCom.updateOffsets(new Vector2(titleTextCom.TextComponent.width / 2,
                                           titleTextCom.TextComponent.height / 2) * -1);

            titleTextCom.TextComponent
                .tweenColorTo(new Color(172, 117, 139), 0.8f).setDelay(0.3f)
                .setNextTween(titleTextCom.TextComponent.tweenColorTo(Color.WhiteSmoke, 0.6f))
                .start();

            var table = canvas.stage.addElement(new Table());
            table.setFillParent(true);

            var uiSkin = new Skin("UI/Skins/menu_skin", FufCore.contentSource);
            var menuButtonDimens = new Vector2(160, 42);
            var menuButtonPadding = 2;
            var menuGroupComposer =
                new MenuGroupComposer(uiSkin.get<TextButtonStyle>("pink_metal"), menuButtonDimens, menuButtonPadding);
            menuGroupComposer.Buttons.Add(new MenuGroupComposer.MenuGroupButton {
                Text = "PLAY",
                Click = bt => { switchSceneFade<GameSelectScene>(); }
            });
            menuGroupComposer.Buttons.Add(new MenuGroupComposer.MenuGroupButton {
                Text = "SETTINGS",
                Click = bt => { switchSceneFade<SettingsScene>(); }
            });
            menuGroupComposer.Buttons.Add(new MenuGroupComposer.MenuGroupButton {
                Text = "EXIT",
                Click = bt => { Core.exit(); }
            });
            menuGroupComposer.attachTo(table);

            var textButtons = canvas.stage.findAllElementsOfType<TextButton>();
            canvas.stage.setGamepadFocusElement(textButtons[0]);
            setupGamepadInput(canvas);

            var borderPadding = 20;

            var petaphaserTextCom = new TextComposer(uiAssets.DisposableDroid) {
                Text = "PetaPhaser"
            }.attach(this, new Vector2(0, NGame.ViewportHeight), Color.WhiteSmoke, "petaphaser_t");
            petaphaserTextCom.updateOffsets(new Vector2(borderPadding,
                -(petaphaserTextCom.TextComponent.height / 2 + borderPadding)));

            petaphaserTextCom.TextComponent
                .tweenColorTo(new Color(255, 15, 127), 0.6f).setDelay(0.4f)
                .setEaseType(EaseType.QuadInOut)
                .setLoops(LoopType.RestartFromBeginning, 96, 2.7f)
                .start();

            var versionTextCom = new TextComposer(uiAssets.DisposableDroid) {
                Text = $"v{NGame.GameVersion}"
            }.attach(this, new Vector2(NGame.ViewportWidth, NGame.ViewportHeight), Color.White, "version_t");
            versionTextCom.updateOffsets(new Vector2(versionTextCom.TextComponent.width + borderPadding,
                                             versionTextCom.TextComponent.height / 2 + borderPadding) * -1);
        }

        public override void update() {
            base.update();
        }
    }
}