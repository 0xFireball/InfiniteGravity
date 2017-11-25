using System;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Fuf.Physics;

namespace InfiniteGravity.Components.Characters {
    public abstract class CharacterBody : KinematicBody {
        protected CharacterController _controller;

        public float movementSpeed = 120.0f;
        public float angularThrust = (float) Math.PI * 0.4f;
        public float fallThrust = 40.0f;

        public Vector2 attachedSurfaceNormal = Vector2.Zero;
        public float attachmentSlowdown = 12;

        public float surfaceAlignAngleVariance = (float) Math.PI / 6f;
        public float surfaceAttachEpsilon = (float) Math.PI / 64f;

        public float surfaceAlignLerp = 0.2f;

        public float baseDrag = 0f;
        public float surfaceDrag = 100f;

        public float jumpVelocity = -240f;

        public enum MovementState {
            Free,
            Aligning,
            Attached
        }

        public MovementState movementState = MovementState.Free;

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

        public override Vector2 applyMotion(Vector2 posDelta) {
            var physicsDeltaMovement = posDelta;

            drag = new Vector2(baseDrag);
            attachedSurfaceNormal = Vector2.Zero;

            movementState = MovementState.Free;

            // collision
            if (entity.getComponent<BoxCollider>().collidesWithAny(ref physicsDeltaMovement, out var result)) {
                // check if touching tilemap
                if (result.collider.entity.getComponent<TiledMapComponent>() != null) {
                    entity.position -= result.minimumTranslationVector;
                    // TODO: Set touching flags
                    // TODO: collision against map collision layer, cancel velocity

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

            return physicsDeltaMovement;
        }

        public override void update() {
            base.update();

            if (_controller != null) {
                movement();
                actionInput();
            }
        }

        protected float calculateSurfaceAngle() {
            return Mathf.atan2(attachedSurfaceNormal.Y, attachedSurfaceNormal.X) +
                   (float) (Math.PI / 2f);
        }

        private void movement() {
            var movementVel = Vector2.Zero;

            // if not anchored, use the jetpack

            if (movementState == MovementState.Attached) {
                // attached, run along the surface
                var surfaceAngle = calculateSurfaceAngle();
                if (Math.Abs(_controller.moveDirectionInput.value.X) > 0) {
                    var run = new Vector2(_controller.moveDirectionInput.value.X, 0) * movementSpeed;
                    run = Vector2Ext.transform(run, Matrix2D.createRotation(surfaceAngle));
                    // run = MathUtils.cartesianToScreenSpace(run);
                    velocity += run;
                }
                // jump off the surface
                if (_controller.thrustInput > 0) {
                    var jump = new Vector2(0, jumpVelocity);
                    jump = Vector2Ext.transform(jump, Matrix2D.createRotation(surfaceAngle));
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
                fall = Vector2Ext.transform(fall, Matrix2D.createRotation(entity.transform.localRotation));
                velocity += fall;
            }

            // TODO: movement
            velocity += movementVel;
        }

        private void actionInput() {
            // TODO: actions
        }
    }
}