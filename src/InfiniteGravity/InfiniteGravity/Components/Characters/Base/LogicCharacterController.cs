using Nez.Fuf;

namespace InfiniteGravity.Components.Characters.Base {
    public class LogicCharacterController : CharacterController {
        public VirtualJoystick.LogicJoystick moveDirectionLogical { get; } = new VirtualJoystick.LogicJoystick();
        public VirtualJoystick.LogicJoystick targetDirectionLogical { get; } = new VirtualJoystick.LogicJoystick();

        public VirtualButton.LogicButton primaryActionLogical { get; } = new VirtualButton.LogicButton();

        public override void initialize() {
            moveDirectionInput.nodes.Add(moveDirectionLogical);
            targetDirectionInput.nodes.Add(targetDirectionLogical);

            primaryActionInput.nodes.Add(primaryActionLogical);

            base.initialize();
        }
    }
}