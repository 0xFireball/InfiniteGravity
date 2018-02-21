using InfiniteGravity.Assets;
using InfiniteGravity.Configuration;
using InfiniteGravity.Scenes.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Fuf;
using Nez.UI;

namespace InfiniteGravity.Scenes {
    public class SettingsScene : BaseUIScene {
        private GameContext gameContext;

        public override void initialize() {
            base.initialize();

            gameContext = Core.services.GetService<GameContext>();
            
            var uiAssets = Core.services.GetService<UiAssets>();

            var uiSkin = new Skin("UI/Skins/menu_skin", FufCore.contentSource);

            var table = canvas.stage.addElement(new Table());
            table.setFillParent(true);
            table.pad(40);
            
            var leftTable = new Table();
            table.add(leftTable).setMaxWidth(100).pad(20).top();
            
            var rightTable = new Table();
            table.add(rightTable).expand().pad(20).center().top();

            var buttonHeight = 42;

            var themeId = "pink_metal";
            var backButton = new TextButton("BACK", uiSkin.get<TextButtonStyle>(themeId));
            backButton.onClicked += (b) => leave();
            leftTable.add(backButton).setMinWidth(100).setMinHeight(buttonHeight).setAlign(Align.left).setPadBottom(40);
            leftTable.row();

            canvas.stage.setGamepadFocusElement(backButton);
            setupGamepadInput();
            
            rightTable.add(new Label("SETTINGS", uiSkin.get<LabelStyle>(themeId))).setPadBottom(40).center();
            rightTable.row();

            string fullscreenToggleText() {
                return "FULLSCREEN: " + (gameContext.configuration.graphics.fullscreen ? "ENABLED" : "DISABLED");
            }

            var fullscreenToggle = new TextButton(fullscreenToggleText(), uiSkin.get<TextButtonStyle>(themeId));
            fullscreenToggle.onClicked += (b) => {
                gameContext.configuration.graphics.fullscreen = !gameContext.configuration.graphics.fullscreen;
                fullscreenToggle.setText(fullscreenToggleText());
            };
            rightTable.add(fullscreenToggle).setMinWidth(240).setMinHeight(buttonHeight);
            rightTable.row();

//            var selectBoxStyle = uiSkin.get<SelectBoxStyle>(themeId);
//            selectBoxStyle.scrollStyle = new ScrollPaneStyle();
//            selectBoxStyle.listStyle = new ListBoxStyle {
//                font = uiAssets.PixeledBMFont,
//                selection = new PrimitiveDrawable(Color.Blue)
//            };
//            var fakeButton = new SelectBox<string>(selectBoxStyle);
//            fakeButton.setItems("Test1", "Test2", "Fuf");
//            rightTable.add(fakeButton).setMinWidth(240).setMinHeight(buttonHeight);
//            rightTable.row();

            clearColor = new Color(35, 35, 35);
        }

        public override void update() {
            // exiting settings shortcut
            if (Input.isKeyPressed(Keys.Escape) && _active) {
                leave();
            }

            base.update();
        }

        public void saveSettings() {
            // save the context
            gameContext.configuration.save();
        }

        public void leave() {
            saveSettings();
            switchSceneFade<MenuScene>();
        }
    }
}