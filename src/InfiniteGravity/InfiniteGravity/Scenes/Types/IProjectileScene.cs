using System.Collections.Generic;
using Nez;

namespace InfiniteGravity.Scenes.Types {
    public interface IProjectileScene {
        List<Entity> projectiles { get; }
    }
}