

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WiseEngine.MonogamePart;

namespace WiseEngine;

public abstract class Collider
{
    public Color Color { get; set; } = Color.Yellow;
    public Vector2 Position { get; protected set; }
    public abstract void Move(Vector2 newPos);

    public static bool IsIntersects (Collider c1, Collider c2)
    {
        if (c1 is RectangleCollider && c2 is RectangleCollider)
        {
            return (c1 as RectangleCollider).Area.Intersects((c2 as RectangleCollider).Area);
        }
        else
        {
            GameConsole.WriteLine("Warning: In this version you can't calculate intersection between different collider types");
            return false;
        }        
    }

    //Yes, I understand that it breaks a bit division of model and view, but it is more suitable in this case
    public abstract void Draw (SpriteBatch spriteBatch);

}
