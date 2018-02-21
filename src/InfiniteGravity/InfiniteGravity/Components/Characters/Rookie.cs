using System;
using System.Collections.Generic;
using InfiniteGravity.Components.Characters.Gear;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Fuf;
using Nez.Sprites;
using Nez.Textures;

namespace InfiniteGravity.Components.Characters {
    public class Rookie : Character {
        public Rookie() : base(FufCore.contentSource.Load<Texture2D>("Sprites/Characters/rookie"), 128, 128) { }

        public override void initialize() {
            base.initialize();

            var animationFps = 10f;

            // create animation
            animation.addAnimation(Animations.Idle, new SpriteAnimation(new List<Subtexture> {
                subtextures[0]
            }) {fps = animationFps});

            animation.addAnimation(Animations.Ready, new SpriteAnimation(new List<Subtexture> {
                subtextures[1]
            }) {fps = animationFps});

            animation.addAnimation(Animations.Run, new SpriteAnimation(new List<Subtexture> {
                subtextures[2],
                subtextures[3],
                subtextures[4],
                subtextures[5]
            }) {fps = animationFps});

            animation.addAnimation(Animations.Melee1, new SpriteAnimation(new List<Subtexture> {
                subtextures[6],
                subtextures[7],
                subtextures[8],
                subtextures[9]
            }) {fps = animationFps, loop = false});

            animation.addAnimation(Animations.Melee2, new SpriteAnimation(new List<Subtexture> {
                subtextures[10],
                subtextures[11],
                subtextures[12],
                subtextures[13]
            }) {fps = animationFps, loop = false});

            animation.addAnimation(Animations.Melee3, new SpriteAnimation(new List<Subtexture> {
                subtextures[14],
                subtextures[15],
                subtextures[16],
                subtextures[17]
            }) {fps = animationFps, loop = false});

            animation.addAnimation(Animations.Gun1, new SpriteAnimation(new List<Subtexture> {
                subtextures[18],
                subtextures[19],
                subtextures[20],
                subtextures[21]
            }) {fps = animationFps, loop = false});

            animation.addAnimation(Animations.Hurt1, new SpriteAnimation(new List<Subtexture> {
                subtextures[22],
                subtextures[23],
                subtextures[24],
                subtextures[25]
            }) {fps = animationFps, loop = false});

            // flip X based on facing direction
            setFacingFlip(true, false);

            facing = Direction.Right;

            body = entity.addComponent(new RookieBody {
                bodyCollider = entity.addComponent(new BoxCollider(-4, -8, 10, 24))
            });

            // gear
            entity.addComponent(new Sword(new Vector2(4, -8), new Rectangle(4, -4, 28, 14)));
            entity.addComponent(new Gun(new Vector2(6, -8)) {
                range = 400f
            });
        }

        public class RookieBody : AnimatedCharacterBody {
            public RookieBody() {
                movementSpeed = 240f;
            }
        }
    }
}