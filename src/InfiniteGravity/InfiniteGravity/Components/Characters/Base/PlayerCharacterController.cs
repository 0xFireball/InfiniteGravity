using Microsoft.Xna.Framework.Input;
using Nez;
using VirtualButton = Nez.VirtualButton;
using VirtualJoystick = Nez.VirtualJoystick;

namespace InfiniteGravity.Components.Characters.Base {
    public class PlayerCharacterController : CharacterController {
        public override void initialize() {
            base.initialize();

            moveDirectionInput.nodes.Add(new VirtualJoystick.KeyboardKeys(VirtualInput.OverlapBehavior.CancelOut,
                Keys.Left, Keys.Right, Keys.Up, Keys.Down));
            moveDirectionInput.nodes.Add(new VirtualJoystick.KeyboardKeys(VirtualInput.OverlapBehavior.CancelOut,
                Keys.A, Keys.D, Keys.W, Keys.S));
            moveDirectionInput.nodes.Add(new VirtualJoystick.GamePadLeftStick());

            primaryActionInput.nodes.Add(new VirtualButton.MouseLeftButton());
            primaryActionInput.nodes.Add(new VirtualButton.KeyboardKey(Keys.F));

            targetDirectionInput.nodes.Add(new VirtualJoystick.GamePadRightStick());
            targetDirectionInput.nodes.Add(new Nez.Fuf.VirtualJoystick.MouseDirectionalJoystick(entity.scene.camera));
        }
    }
}