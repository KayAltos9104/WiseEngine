

using Microsoft.Xna.Framework;

namespace WiseEngine;

public abstract class Collider
{ 
    public Vector2 Position { get; protected set; }
    public abstract void Move(Vector2 newPos);

}
