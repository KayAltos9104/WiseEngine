using System.Numerics;
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

            int XOffset = isLeft ? collider1.Area.Right - collider2.Area.Left : collider1.Area.Left - collider2.Area.Right;
            int YOffset = isBottom ? collider2.Area.Bottom - collider1.Area.Top : collider2.Area.Bottom - collider1.Area.Top;
            Vector2 offset = new Vector2(XOffset, YOffset);
            if (s1.Speed != Vector2.Zero && s2.Speed != Vector2.Zero)
            {
                
                o1.Pos -= offset/2;
                o2.Pos = offset/2;
            }
            else if (s1.Speed != Vector2.Zero && s2.Speed == Vector2.Zero)
            {
                o1.Pos -= offset;
            }
            else
            {
                 o2.Pos = offset;
            }
        }
    }
    public void Update (object sender, ManageCollisionEventArgs e)
    {
        Update (e.Object1, e.Object2);
    }
}
