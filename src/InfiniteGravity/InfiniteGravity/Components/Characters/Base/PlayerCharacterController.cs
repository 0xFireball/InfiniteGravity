using Microsoft.Xna.Framework.Input;
using Nez;
using VirtualButton = Nez.VirtualButton;
using VirtualJoystick = Nez.VirtualJoystick;

namespace InfiniteGravity.Components.Characters {
    public class PlayerCharacterController : CharacterController {
        public override void initialize() {
            base.initialize();

            moveDirectionInput.nodes.Add(new VirtualJoystick.KeyboardKeys(VirtualInput.OverlapBehavior.CancelOut,
                Keys.A, Keys.D, Keys.W, Keys.S));
            moveDirectionInput.nodes.Add(new VirtualJoystick.GamePadLeftStick());
            
            thrustInput.nodes.Add(new VirtualAxis.KeyboardKeys(VirtualInput.OverlapBehavior.CancelOut, Keys.Z, Keys.X));

            primaryActionInput.nodes.Add(new VirtualButton.MouseLeftButton());
            primaryActionInput.nodes.Add(new VirtualButton.KeyboardKey(Keys.J));
            
            secondaryActionInput.nodes.Add(new VirtualButton.MouseRightButton());
            secondaryActionInput.nodes.Add(new VirtualButton.KeyboardKey(Keys.K));

            targetDirectionInput.nodes.Add(new VirtualJoystick.GamePadRightStick());
            targetDirectionInput.nodes.Add(new Nez.Fuf.VirtualJoystick.MouseDirectionalJoystick(entity.scene.camera));
            
            aimActionInput.nodes.Add(new VirtualButton.KeyboardKey(Keys.Q));
        }
    }
}