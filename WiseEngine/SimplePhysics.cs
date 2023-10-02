
using Microsoft.Xna.Framework;
using WiseEngine.MVP;

namespace WiseEngine;
public class SimplePhysics : IPhysics
{
    private const int collisionSolutionTries = 50;
    //private const float g = 9.81f;
    private const float g = 22f;
    public void Update (IObject obj)
    {
        if (obj is ISolid solid && solid.IsStatic == false)
        {
            Vector2 gravitationalForce = new Vector2(0, g * solid.Mass);
            solid.Force += gravitationalForce;
            Vector2 speed = solid.Force / solid.Mass * Globals.Time.ElapsedGameTime.Milliseconds / 1000.0f;
            speed = new Vector2 ((float)Math.Round(speed.X, 0), (float)Math.Round(speed.Y, 0));
            obj.Pos += speed;
            solid.IsOnPlatform = false;
        }
    }
    public void Update (List<IObject> objects)
    {
        objects.ForEach(o => Update (o));
    }
    public void SolveCollision(IObject o1, IObject o2)
    {        
        if (o1 is ISolid s1 && o2 is ISolid s2)
        {
            if (s1.PrevPos == null || s2.PrevPos == null)
                throw new ArgumentNullException("Missing previous positions");

            var collider1 = s1.GetCollider() as RectangleCollider;
            var collider2 = s2.GetCollider() as RectangleCollider;

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

                if (s1.IsStatic == false && s2.IsStatic == true)
                {
                    s1.IsOnPlatform = true;
                }
                else if (s1.IsStatic == true && s2.IsStatic == false)
                {
                    s2.IsOnPlatform = true;
                }
            }            
            bounceVector1 = bounceVector1!=Vector2.Zero ? Vector2.Normalize(bounceVector1) : Vector2.Zero;
            bounceVector2 = bounceVector2 != Vector2.Zero ? Vector2.Normalize(bounceVector2) : Vector2.Zero;

            //Абсолютно неупругое соударение
            s1.Force = new Vector2(bounceVector1.X == 0 ? s1.Force.X : 0, bounceVector1.Y == 0 ? s1.Force.Y : 0);
            s2.Force = new Vector2(bounceVector2.X == 0 ? s2.Force.X : 0, bounceVector2.Y == 0 ? s2.Force.Y : 0);

            
            int tries = 0;
            do
            {
                o1.Pos -= bounceVector1;
                o2.Pos -= bounceVector2;
                collider1 = s1.GetCollider() as RectangleCollider;
                collider2 = s2.GetCollider() as RectangleCollider;
                tries++;
            } while (Collider.IsIntersects(collider1, collider2) && tries < collisionSolutionTries);  
        }
    }
    public void SolveCollision (object sender, ManageCollisionEventArgs e)
    {
        SolveCollision (e.Object1, e.Object2);
    }
}
