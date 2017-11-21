using System;

namespace InfiniteGravity.Components.Characters.Base {
    public abstract class AnimatedCharacterBody : CharacterBody {
        public override void update() {
            base.update();

            var sprite = entity.getComponent<Character>();
            var animation = Character.Animations.Idle;

            if (Math.Abs(angularVelocity) > 0) {
                animation = Character.Animations.Ready;
            }

            // TODO: Directions

            if (!sprite.Animation.isAnimationPlaying(animation)) {
                sprite.Animation.play(animation);
            }
        }
    }
}