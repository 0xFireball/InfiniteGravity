﻿using System;
using Microsoft.Xna.Framework;
using Nez;

namespace InfiniteGravity.Components.Characters {
    public abstract class AnimatedCharacterBody : CharacterBody {
        private Direction _lastSideDirection;

        public override void update() {
            base.update();

            var sprite = entity.getComponent<Character>();
            var animation = Character.Animations.Idle;

            // movement-based animation

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

            // action-based animation
            switch (actionState) {
                case ActionState.Melee:
                    if (meleeComboCount == 0) animation = Character.Animations.Melee1;
                    else if (meleeComboCount == 1) animation = Character.Animations.Melee2;
                    else if (meleeComboCount == 2) animation = Character.Animations.Melee3;
                    
                    break;
                case ActionState.Gun:
                    animation = Character.Animations.Gun1;
                    break;
                default:
                    // attack animations should never be flipped partway through
                    
                    // facing direction

                    if (_controller.moveDirectionInput.value.Length() > 0) {
                        _lastSideDirection = _controller.moveDirectionInput.value.X > 0 ? Direction.Right : Direction.Left;
                    }

                    sprite.facing = _lastSideDirection;
                    break;
            }

            if (!sprite.Animation.isAnimationPlaying(animation)) {
                sprite.Animation.play(animation);
            }
        }
    }
}