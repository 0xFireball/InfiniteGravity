using System;
using System.Collections.Generic;
using InfiniteGravity.Components.Characters.Gear;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;
using Nez.Textures;

namespace InfiniteGravity.Components.Characters {
    public class Rookie : Character {
        public Rookie(string sprite) : base(sprite) { }
        public Rookie() : this("rookie") { }

        protected override void loadSprites() {
            loadGraphic($"Sprites/Characters/{spriteAsset}", true, 128, 128);

            var animationFps = 10f;

            // create animation
            Animation.addAnimation(Animations.Idle, new SpriteAnimation(new List<Subtexture> {
                _subtextures[0]
            }) {fps = animationFps});

            Animation.addAnimation(Animations.Ready, new SpriteAnimation(new List<Subtexture> {
                _subtextures[1]
            }) {fps = animationFps});

            Animation.addAnimation(Animations.Run, new SpriteAnimation(new List<Subtexture> {
                _subtextures[2],
                _subtextures[3],
                _subtextures[4],
                _subtextures[5]
            }) {fps = animationFps});

            Animation.addAnimation(Animations.Melee1, new SpriteAnimation(new List<Subtexture> {
                _subtextures[6],
                _subtextures[7],
                _subtextures[8],
                _subtextures[9]
            }) {fps = animationFps});

            Animation.addAnimation(Animations.Melee2, new SpriteAnimation(new List<Subtexture> {
                _subtextures[10],
                _subtextures[11],
                _subtextures[12],
                _subtextures[13]
            }) {fps = animationFps});

            Animation.addAnimation(Animations.Melee3, new SpriteAnimation(new List<Subtexture> {
                _subtextures[14],
                _subtextures[15],
                _subtextures[16],
                _subtextures[17]
            }) {fps = animationFps});

            Animation.addAnimation(Animations.Gun1, new SpriteAnimation(new List<Subtexture> {
                _subtextures[18],
                _subtextures[19],
                _subtextures[20],
                _subtextures[21]
            }) {fps = animationFps});
            
            Animation.addAnimation(Animations.Hurt1, new SpriteAnimation(new List<Subtexture> {
                _subtextures[22],
                _subtextures[23],
                _subtextures[24],
                _subtextures[25]
            }) {fps = animationFps});

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
            public override void initialize() {
                base.initialize();

                movementSpeed = 240f;
            }
        }
    }
}