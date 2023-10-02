using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WiseEngine.MonogamePart;

namespace WiseEngine.PhysicsAndCollisions;

public class RectangleCollider : Collider
{
    public Rectangle Area { get; protected set; }

    public RectangleCollider(Vector2 pos, int width, int height)
    {
        Area = new Rectangle(pos.ToPoint(), new Point(width, height));
    }
    public override void Move(Vector2 newPos)
    {
        Area = new Rectangle(newPos.ToPoint(), new Point(Area.Width, Area.Height));
        Position = newPos;
    }

    //public override bool IsIntersects(Collider other)
    //{
    //    if (other is RectangleCollider)
    //    {
    //        return Area.Intersects((other as RectangleCollider).Area);
    //    }            
    //    else
    //    {
    //        GameConsole.WriteLine("Warning: In this version you can't calculate intersection between different collider types");
    //        return false;
    //    }            
    //}

    public override void Draw(SpriteBatch spriteBatch)
    {
        Graphics2D.DrawRectangle(Area.X, Area.Y, Area.Width, Area.Height, Color, 3);
    }
}
