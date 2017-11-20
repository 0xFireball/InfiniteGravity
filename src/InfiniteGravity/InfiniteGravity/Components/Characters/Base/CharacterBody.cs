using System;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Fuf;
using Nez.Fuf.Physics;

namespace InfiniteGravity.Components.Characters.Base {
    public abstract class CharacterBody : KinematicBody {
        private CharacterController _controller;

        public float movementSpeed = 120.0f;

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

            // TODO: movement
            velocity += movementVel;
        }

        private void actionInput() {
            // TODO: actions
        }
    }
}