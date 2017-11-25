using Nez;

namespace InfiniteGravity.Components.Characters {
    public class CharacterController : Component {
        public VirtualJoystick moveDirectionInput = new VirtualJoystick(true);
        public VirtualJoystick targetDirectionInput = new VirtualJoystick(true);
        
        public VirtualAxis thrustInput = new VirtualAxis();
        
        public VirtualButton primaryActionInput = new VirtualButton();
        public VirtualButton secondaryActionInput = new VirtualButton();
    }
}