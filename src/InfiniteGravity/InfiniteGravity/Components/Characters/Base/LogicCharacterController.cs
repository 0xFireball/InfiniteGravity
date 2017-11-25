using Nez.Fuf;

namespace InfiniteGravity.Components.Characters {
    public class LogicCharacterController : CharacterController {
        public VirtualJoystick.LogicJoystick moveDirectionLogical { get; } = new VirtualJoystick.LogicJoystick();
        public VirtualJoystick.LogicJoystick targetDirectionLogical { get; } = new VirtualJoystick.LogicJoystick();

        public VirtualAxis.LogicAxis thrustLogical { get; } = new VirtualAxis.LogicAxis();

        public VirtualButton.LogicButton primaryActionLogical { get; } = new VirtualButton.LogicButton();
        public VirtualButton.LogicButton secondaryActionLogical { get; } = new VirtualButton.LogicButton();

        public override void initialize() {
            moveDirectionInput.nodes.Add(moveDirectionLogical);
            targetDirectionInput.nodes.Add(targetDirectionLogical);

            thrustInput.nodes.Add(thrustLogical);

            primaryActionInput.nodes.Add(primaryActionLogical);
            secondaryActionInput.nodes.Add(secondaryActionLogical);

            base.initialize();
        }
    }
}