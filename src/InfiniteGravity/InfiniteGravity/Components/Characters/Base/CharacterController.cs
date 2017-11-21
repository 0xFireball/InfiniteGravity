using Nez;

namespace InfiniteGravity.Components.Characters.Base {
    public class CharacterController : Component {
        public VirtualJoystick moveDirectionInput = new VirtualJoystick(true);
        public VirtualJoystick targetDirectionInput = new VirtualJoystick(true);
        
        public VirtualAxis thrustInput = new VirtualAxis();
        public VirtualButton primaryActionInput = new VirtualButton();
    }
}