﻿using System;
using System.Collections.Generic;
using InfiniteGravity.Components.Characters.Gear;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Fuf.Physics;

namespace InfiniteGravity.Components.Characters {
    public abstract class CharacterBody : KinematicBody {
        protected CharacterController _controller;

        public float movementSpeed = 240.0f;
        public float angularThrust = (float) Math.PI * 0.4f;
        public float fallThrust = 40.0f;

        public Vector2 attachedSurfaceNormal = Vector2.Zero;

        public float surfaceAlignAngleVariance = (float) Math.PI / 6f;
        public float surfaceAttachEpsilon = (float) Math.PI / 64f;

        public float surfaceAlignLerp = 0.2f;

        public float baseDrag = 0f;
        public float surfaceDrag = 200f;

        public float jumpVelocity = -240f;

        public ActionState actionState;

        /// <summary>
        /// Timer for current action (time remaining)
        /// </summary>
        public float actionTime = 0;

        // Actions

        public bool actionCombo = false;

        private const float attackActionDuration = 0.38f;

        // Melee weapon
        public float meleeComboTime = 0.1f;

        public int meleeComboCount = 0;

        public Collider bodyCollider;

        public enum ActionState {
            None,
            Cooldown,
            Interact,
            Melee,
            Gun
        };

        public enum MovementState {
            Free,
            Aligning,
            Attached
        }

        public enum BodyState {
            Normal,
            Hurt
        }

        public float bodyStateTime;

        public MovementState movementState = MovementState.Free;
        
        public BodyState bodyState;

        public Direction lastFacing;

        public override void initialize() {
            base.initialize();

            maxAngular = (float) Math.PI * 0.2f;
            angularDrag = (float) Math.PI * 0.3f;

            maxVelocity = new Vector2(fallThrust * 2);
        }

        public override void onAddedToEntity() {
            base.onAddedToEntity();

            _controller = entity.getComponent<CharacterController>();
        }

        private List<CollisionResult> collisionResults = new List<CollisionResult>();

        public override Vector2 applyMotion(Vector2 posDelta) {
            var physicsDeltaMovement = posDelta;

            drag = new Vector2(baseDrag);
            attachedSurfaceNormal = Vector2.Zero;

            movementState = MovementState.Free;

            // collision
            collisionResults.Clear();

            if (bodyCollider.collidesWithAnyMultiple(physicsDeltaMovement, collisionResults)) {
                for (var i = 0; i < collisionResults.Count; i++) {
                    var result = collisionResults[i];

                    // check if touching tilemap
                    if (result.collider.entity.getComponent<TiledMapComponent>() != null) {
                        // TODO: Set touching flags
                        // TODO: collision against map collision layer, cancel velocity

                        // accept hard movement adjustment
                        physicsDeltaMovement -= result.minimumTranslationVector;

                        var halfPi = ((float) Math.PI / 2);

                        var upward = new Vector2(0, -1);
                        upward = Vector2Ext.transform(upward, Matrix2D.createRotation(angle));

                        var normalAngle = Mathf.atan2(result.normal.Y, result.normal.X);

                        var angleToNormal = Math.Abs(Mathf.acos(Vector2.Dot(upward, result.normal)));

                        if (angleToNormal < surfaceAlignAngleVariance) {
                            var targetAngle = (normalAngle + halfPi) % (Mathf.PI * 2);
                            if (angleToNormal < surfaceAttachEpsilon) {
                                attachedSurfaceNormal = result.normal;
                                angle = targetAngle;
                                movementState = MovementState.Attached;
                            } else {
                                // angle closer to the target
                                var currentAngle = angle % (Mathf.PI * 2);
                                angle = Mathf.lerpAngle(currentAngle, targetAngle, surfaceAlignLerp);
                                movementState = MovementState.Aligning;
                            }
                            drag = new Vector2(surfaceDrag);
                        }
                    }
                }
            }

            return physicsDeltaMovement;
        }

        public override void update() {
            base.update();

            if (_controller != null) {
                movement();
                updateActions();
                updateBody();
            }
        }

        protected float calculateSurfaceAngle() {
            return Mathf.atan2(attachedSurfaceNormal.Y, attachedSurfaceNormal.X) +
                   (float) (Math.PI / 2f);
        }

        protected void transformBySurfaceAngle(ref Vector2 vec) {
            vec = Vector2Ext.transform(vec, Matrix2D.createRotation(calculateSurfaceAngle()));
        }

        protected void transformByRotation(ref Vector2 vec) {
            vec = Vector2Ext.transform(vec, Matrix2D.createRotation(entity.transform.localRotation));
        }

        private void movement() {
            var movementVel = Vector2.Zero;

            // if not anchored, use the jetpack

            if (movementState == MovementState.Attached) {
                // attached, run along the surface
                if (Math.Abs(_controller.moveDirectionInput.value.X) > 0) {
                    var run = new Vector2(_controller.moveDirectionInput.value.X, 0) * movementSpeed;
                    transformBySurfaceAngle(ref run);
                    // run = MathUtils.cartesianToScreenSpace(run);
                    velocity += run;
                }
                // jump off the surface
                if (_controller.thrustInput > 0) {
                    var jump = new Vector2(0, jumpVelocity);
                    transformBySurfaceAngle(ref jump);
                    attachedSurfaceNormal = Vector2.Zero;
                    velocity += jump;
                }
            } else {
                // jetpack steering
                if (movementState == MovementState.Free) {
                    if (Math.Abs(_controller.moveDirectionInput.value.X) > 0) {
                        var turn = _controller.moveDirectionInput.value.X * angularThrust;
                        angularVelocity += turn;
                    }
                }
            }

            if (_controller.thrustInput < 0) {
                var fall = new Vector2(0, _controller.thrustInput * -fallThrust);
                transformByRotation(ref fall);
                velocity += fall;
            }

            // facing
            if (_controller.moveDirectionInput.value.Length() > 0) {
                lastFacing = _controller.moveDirectionInput.value.X > 0 ? Direction.Right : Direction.Left;
            }

            // TODO: movement
            velocity += movementVel;
        }

        private void updateActions() {
            // TODO: Action input
            if (actionState == ActionState.None) {
                // no overlapping actions?
                // TODO: Melee can cancel gun animation?

                if (_controller.primaryActionInput) {
                    actionState = ActionState.Melee;
                    actionTime = attackActionDuration; // match animation length
                    meleeComboCount = 0;
                    hitMelee();
                }

                if (_controller.secondaryActionInput) {
                    actionState = ActionState.Gun;
                    actionTime = attackActionDuration;
                    hitGun();
                }
            }

            // update action status
            if (actionState != ActionState.None) {
                if (actionTime <= 0) {
                    if (actionCombo) {
                        actionCombo = false;
                        if (actionState == ActionState.Melee && meleeComboCount < 3) {
                            // melee combo
                            meleeComboCount++;
                            actionTime = attackActionDuration;
                            hitMelee();
                        }
                    } else if (actionState == ActionState.Cooldown) {
                        // action is over
                        actionState = ActionState.None;
                    } else {
                        // force a cooldown
                        actionState = ActionState.Cooldown;
                        actionTime = 0.2f;
                    }
                } else {
                    actionTime -= Time.deltaTime;
                }
            }

            // combos
            if (movementState == MovementState.Attached && actionState == ActionState.Melee) {
                if (_controller.primaryActionInput.isPressed && actionTime < meleeComboTime) {
                    // melee combo
                    actionCombo = true;
                }
            }
        }

        private void updateBody() {
            if (bodyStateTime <= 0) {
                bodyState = BodyState.Normal;
            } else {
                bodyStateTime -= Time.deltaTime;
            }
        }

        private void hitMelee() {
            // perform a melee hit
            var meleeWeapon = entity.getComponent<MeleeWeapon>();
            var dir = lastFacing == Direction.Right ? 1 : -1;
            var facingFlipScale = new Vector2(dir, 1);
            var offset = meleeWeapon.offset;
            offset = Vector2Ext.transform(offset, Matrix2D.createScale(facingFlipScale));
            var reach = meleeWeapon.reach;
            // reflect X based on direction
            RectangleExt.scale(ref reach, facingFlipScale);
//            var swordCollider = new BoxCollider(offset.X + reach.X,
//               offset.Y + reach.Y, reach.Width, reach.Height);
            var swordCollider = new BoxCollider(0, 0, reach.Width, reach.Height);
            swordCollider.localOffset = new Vector2(offset.X + reach.X + reach.Width / 2f, offset.Y + reach.Y + reach.Height / 2f);
            entity.addComponent(swordCollider);
            collisionResults.Clear();
            swordCollider.collidesWithAnyMultiple(Vector2.Zero, collisionResults);

            for (var i = 0; i < collisionResults.Count; i++) {
                var result = collisionResults[i];

                var character = result.collider?.entity.getComponent<Character>();
                if (character != null) {
                    character.body.hurt();
                }
            }
            
            entity.removeComponent(swordCollider);
        }

        private void hitGun() {
            // perform a gun hit
            var gunWeapon = entity.getComponent<Gun>();
            var dir = lastFacing == Direction.Right ? 1 : -1;
            var facingFlipScale = new Vector2(dir, 1);
            var offset = gunWeapon.offset;
            offset = Vector2Ext.transform(offset, Matrix2D.createScale(facingFlipScale));
            transformByRotation(ref offset);
            var gunPos = offset + entity.position;
            // draw a line from the gun starting point
            var gunDirection = new Vector2(dir, 0);
            transformByRotation(ref gunDirection);
            var bulletTravel = gunDirection * gunWeapon.range;
            // TODO: Inaccuracy/spread
            var hit = Physics.linecast(gunPos, gunPos + bulletTravel);

            var character = hit.collider?.entity.getComponent<Character>();
            if (character != null) {
                character.body.hurt();
            }
        }

        private void hurt() {
            bodyState = BodyState.Hurt;
            bodyStateTime = 0.38f;
        }
    }
}