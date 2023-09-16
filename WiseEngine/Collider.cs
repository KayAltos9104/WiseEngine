

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WiseEngine;

public abstract class Collider
{ 
    public Vector2 Position { get; protected set; }
    public abstract void Move(Vector2 newPos);

    public abstract bool IsIntersects(Collider other);

    public abstract void Draw (SpriteBatch spriteBatch);

}
