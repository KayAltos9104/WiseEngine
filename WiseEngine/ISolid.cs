using Microsoft.Xna.Framework;

namespace WiseEngine;

public interface ISolid : IShaped
{
    bool IsStatic { get; set; }
    float Mass { get; set; }

    Vector2 Speed { get; set; }
    Vector2 PrevPos { get; set; }
    Vector2 Force { get; set; }    
}
