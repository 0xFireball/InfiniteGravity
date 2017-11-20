using System;
using Microsoft.Xna.Framework;

namespace InfiniteGravity.Util {
    public class MathUtils {
        public static float verticalRefAngle(float angle) {
            return wrapAngle((float) ((Math.PI / 2) - angle));
        }


        /// <summary>
        /// compute the angle to rotate an object relative to facing PI/2
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static float verticalRefRotation(float angle) {
            return wrapAngle((float) (angle - (Math.PI / 2)));
        }

        public static float wrapAngle(float angle) {
            return (float) (angle % (Math.PI * 2));
        }

        public static Vector2 cartesianToScreenSpace(Vector2 cartesian) {
            return new Vector2(cartesian.X, -cartesian.Y);
        }
    }
}