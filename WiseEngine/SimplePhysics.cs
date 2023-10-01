
using Microsoft.Xna.Framework;
using WiseEngine.MVP;

namespace WiseEngine;
public class SimplePhysics : IPhysics
{
    public void Update(IObject o1, IObject o2)
    {
        bool t1 = o1 is ISolid;
        bool t2 = o2 is ISolid;
        if (o1 is ISolid s1 && o2 is ISolid s2)
        {
            var collider1 = s1.GetCollider() as RectangleCollider;
            var collider2 = s2.GetCollider() as RectangleCollider;

            bool isLeft = collider1.Area.Left < collider2.Area.Left;
            bool isBottom = collider1.Area.Top < collider2.Area.Top;

            //int XOffset = isLeft ? collider1.Area.Right - collider2.Area.Left : collider1.Area.Left - collider2.Area.Right;
            //int YOffset = isBottom ? collider2.Area.Bottom - collider1.Area.Top : collider2.Area.Bottom - collider1.Area.Top;
            //int YOffset = 0;
            //Vector2 offset = new Vector2(XOffset, YOffset);
            //if (s1.Speed != Vector2.Zero && s2.Speed != Vector2.Zero)
            //{

            //    o1.Pos -= offset/2;
            //    o2.Pos = offset/2;
            //}
            //else if (s1.Speed != Vector2.Zero && s2.Speed == Vector2.Zero)
            //{
            //    o1.Pos -= offset;
            //}
            //else
            //{
            //     o2.Pos = offset;
            //}
            int tries = 0;
            
            
            
            

            Vector2 bufferPos1 = new Vector2 (s1.PrevPos.X, o1.Pos.Y);
            Vector2 bufferPos2 = new Vector2 (s2.PrevPos.X, o2.Pos.Y);

            Rectangle buffer1 = new Rectangle(
                (int)bufferPos1.X, (int)bufferPos1.Y, collider1.Area.Width, collider1.Area.Height);
            Rectangle buffer2 = new Rectangle(
                (int)bufferPos2.X, (int)bufferPos2.Y, collider2.Area.Width, collider2.Area.Height);
            bool isCollidedX = !buffer1.Intersects(buffer2);

            bufferPos1 = new Vector2(o1.Pos.X, s1.PrevPos.Y);
            bufferPos2 = new Vector2(o2.Pos.X, s2.PrevPos.Y);
            buffer1 = new Rectangle(
                (int)bufferPos1.X, (int)bufferPos1.Y, collider1.Area.Width, collider1.Area.Height);
            buffer2 = new Rectangle(
                (int)bufferPos2.X, (int)bufferPos2.Y, collider2.Area.Width, collider2.Area.Height);
            bool isCollidedY = !buffer1.Intersects(buffer2);

            Vector2 bounceVector1 = Vector2.Zero;
            Vector2 bounceVector2 = Vector2.Zero;

            if (isCollidedX)
            {
                bounceVector1 += Vector2.UnitX * (o1.Pos.X - s1.PrevPos.X);
                bounceVector2 += Vector2.UnitX * (o2.Pos.X - s2.PrevPos.X);

                bounceVector1 += o1.Pos.X != s1.PrevPos.X ? Vector2.UnitX * (o1.Pos.X - s1.PrevPos.X) : Vector2.Zero;
                bounceVector2 += o2.Pos.X != s2.PrevPos.X ? Vector2.UnitX * (o2.Pos.X - s2.PrevPos.X) : Vector2.Zero;
            }
            if (isCollidedY)
            {
                bounceVector1 += o1.Pos.Y != s1.PrevPos.Y ? Vector2.UnitY * (o1.Pos.Y - s1.PrevPos.Y) : Vector2.Zero; 
                bounceVector2 += o2.Pos.Y != s2.PrevPos.Y ? Vector2.UnitY *( o2.Pos.Y - s2.PrevPos.Y) : Vector2.Zero;
            }

            bounceVector1 = bounceVector1!=Vector2.Zero ? Vector2.Normalize(bounceVector1) : Vector2.Zero;
            bounceVector2 = bounceVector2 != Vector2.Zero ? Vector2.Normalize(bounceVector2) : Vector2.Zero;

            do
            {
                o1.Pos -= bounceVector1;
                o2.Pos -= bounceVector2;
                collider1 = s1.GetCollider() as RectangleCollider;
                collider2 = s2.GetCollider() as RectangleCollider;
            } while (Collider.IsIntersects(collider1, collider2));
           
                   
        }
    }
    public void Update (object sender, ManageCollisionEventArgs e)
    {
        Update (e.Object1, e.Object2);
    }
}
