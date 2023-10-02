using Microsoft.Xna.Framework;

namespace WiseEngine.PhysicsAndCollisions;

public interface ISolid : IShaped
{
    bool IsStatic { get; set; }

    bool IsOnPlatform { get; set; }
    float Mass { get; set; }
    Vector2 PrevPos { get; set; }
    Vector2 Force { get; set; }
}
