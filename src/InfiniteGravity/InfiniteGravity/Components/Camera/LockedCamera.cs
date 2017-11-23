using Microsoft.Xna.Framework;
using Nez;

namespace InfiniteGravity.Components.Camera {
    public class LockedCamera : Component, IUpdatable {
        private readonly Entity _targetEntity;
        private readonly Nez.Camera camera;

        private Vector2 _precisePosition;
        private Vector2 _lastPosition;

        public LockedCamera(Entity targetEntity, Nez.Camera camera) {
            _targetEntity = targetEntity;
            this.camera = camera;
        }

        public override void onAddedToEntity() {
            base.onAddedToEntity();

            entity.updateOrder = int.MaxValue;
        }

        public void update() {
            if (_targetEntity != null) {
                updateFollow();
            }
        }

        private void updateFollow() {
            // handle teleportation
            if (_lastPosition != camera.position) {
                _precisePosition = camera.position;
            }

            // lock position
            _precisePosition = _targetEntity.position;
            // lock rotation
            camera.transform.localRotation = -_targetEntity.transform.localRotation;

            camera.position = _precisePosition;
            
            _lastPosition = camera.position;
        }
    }
}