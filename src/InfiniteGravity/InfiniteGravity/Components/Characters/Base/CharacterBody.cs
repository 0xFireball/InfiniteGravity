using System;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Fuf;
using Nez.Fuf.Physics;

namespace InfiniteGravity.Components.Characters.Base {
    public abstract class CharacterBody : KinematicBody {
        private CharacterController _controller;

        public float movementSpeed = 120.0f;
        public float angularThrust = (float) Math.PI * 0.4f;

        public override void initialize() {
            base.initialize();
            
            maxAngular = (float) Math.PI * 0.2f;
            angularDrag = (float) Math.PI * 0.3f;
        }

        public override void onAddedToEntity() {
            base.onAddedToEntity();

            _controller = entity.getComponent<CharacterController>();
        }

        public override void update() {
            base.update();

            if (_controller != null) {
                movement();
                actionInput();
            }

            // collision
            if (entity.getComponent<BoxCollider>().collidesWithAny(out var result)) {
                // check if touching tilemap
                if (result.collider.entity.getComponent<TiledMapComponent>() != null) {
                    entity.position -= result.minimumTranslationVector;
                    // TODO: Set touching flags
                    // TODO: collision against map collision layer, cancel velocity
                }
            }
        }

        private void movement() {
            var movementVel = Vector2.Zero;

            if (Math.Abs(_controller.moveDirectionInput.value.X) > 0) {
                var turn = _controller.moveDirectionInput.value.X * angularThrust;
                angularVelocity += turn;
            }

            // TODO: movement
            velocity += movementVel;
        }

        private void actionInput() {
            // TODO: actions
        }
    }
}