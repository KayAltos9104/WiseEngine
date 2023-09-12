

using Microsoft.Xna.Framework;

namespace WiseEngine;

public class RectangleCollider : Collider
{
    public Rectangle Area { get; protected set; }

    public RectangleCollider (Vector2 pos, int width, int height)
    {
        Area = new Rectangle(pos.ToPoint(), new Point (width, height));
    }
    public override void Move(Vector2 newPos)
    {
        Area = new Rectangle (newPos.ToPoint(), new Point (Area.Width, Area.Height));
        Position = newPos;
    }
}
