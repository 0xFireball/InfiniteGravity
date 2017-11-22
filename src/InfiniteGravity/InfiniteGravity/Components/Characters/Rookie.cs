using System;
using System.Collections.Generic;
using InfiniteGravity.Components.Characters.Base;
using Nez;
using Nez.Sprites;
using Nez.Textures;

namespace InfiniteGravity.Components.Characters {
    public class Rookie : Character {
        public Rookie() : base("rookie") { }

        protected override void loadSprites() {
            loadGraphic($"Sprites/Characters/{spriteAsset}", true, 128, 128);

            // create animation
            Animation.addAnimation(Animations.Idle, new SpriteAnimation(new List<Subtexture> {
                _subtextures[0]
            }) {fps = 10f});

            Animation.addAnimation(Animations.Ready, new SpriteAnimation(new List<Subtexture> {
                _subtextures[1]
            }) {fps = 10f});

            Animation.addAnimation(Animations.Run, new SpriteAnimation(new List<Subtexture> {
                _subtextures[2],
                _subtextures[3],
                _subtextures[4],
                _subtextures[5]
            }) {fps = 10f});
            
            // flip X based on facing direction
            setFacingFlip(true, false);

            facing = Direction.Right;
            
            // TODO: Collision
            entity.addComponent(new BoxCollider(-4, -8, 10, 24));
        }

        public override void initialize() {
            base.initialize();

            body = entity.addComponent<RookieBody>();
        }

        public class RookieBody : AnimatedCharacterBody {
            public override void initialize() {
                base.initialize();

                movementSpeed = 210f;
            }
        }
    }
}