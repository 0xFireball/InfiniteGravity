using Microsoft.Xna.Framework;

namespace InfiniteGravity.Scenes.Types {
    public interface IAnnouncerScene {
        void announceText(string text, float duration, Color textColor, System.Action onFinish = null);
    }
}