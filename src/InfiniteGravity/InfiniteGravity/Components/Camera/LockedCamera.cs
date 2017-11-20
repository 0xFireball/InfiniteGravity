using Nez;

namespace InfiniteGravity.Components.Camera {
    public class LockedCamera : FollowCamera {
        public LockedCamera(Entity targetEntity, Nez.Camera camera) : base(targetEntity, camera) { }
        public LockedCamera(Entity targetEntity) : base(targetEntity) { }

        public override void update() {
            base.update();

            camera.transform.localRotation = -_targetEntity.transform.localRotation;
        }
    }
}