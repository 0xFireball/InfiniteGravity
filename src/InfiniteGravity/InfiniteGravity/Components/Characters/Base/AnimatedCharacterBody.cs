﻿using System;
using Microsoft.Xna.Framework;
using Nez;

namespace InfiniteGravity.Components.Characters {
    public abstract class AnimatedCharacterBody : CharacterBody {
        private Direction lastSideDirection;

        public override void update() {
            base.update();

            var sprite = entity.getComponent<Character>();
            var animation = Character.Animations.Idle;

            if (Math.Abs(angularVelocity) > 0) {
                animation = Character.Animations.Ready;
            }

            if (movementState == MovementState.Attached) {
                // attached to a surface
                // get velocity in the direction of the surface
                // get the surface vector
                var surfaceVec = Vector2Ext.transform(attachedSurfaceNormal, Matrix2D.createRotation(Mathf.PI / 2));
                // the velocity in the direction of the surface is the projection of the velocity onto the surface
                var velOnSurface = Vector2.Dot(surfaceVec, velocity);
                if (Math.Abs(velOnSurface) > Mathf.epsilon) {
                    animation = Character.Animations.Run;
                }
            }

            if (_controller.moveDirectionInput.value.Length() > 0) {
                lastSideDirection = _controller.moveDirectionInput.value.X > 0 ? Direction.Right : Direction.Left;
            }

            sprite.facing = lastSideDirection;

            if (!sprite.Animation.isAnimationPlaying(animation)) {
                sprite.Animation.play(animation);
            }
        }
    }
}