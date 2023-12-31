﻿using WiseEngine.Models;
using WiseEngine.PhysicsAndCollisions;

namespace WiseEngine.MVP;

public sealed class CollisionManager
{
    public event EventHandler<ManageCollisionEventArgs>? Collided;

    public IPhysics PhysicsManager;
    
    public CollisionManager ()
    {
        PhysicsManager = new SimplePhysics();
        Collided += PhysicsManager.SolveCollision;
    }
    public void Update (List<IObject> objects)
    {
        List<(IObject o1, IObject o2)> processed = new();        
        objects.ForEach
        (o1 =>
        {
            if (o1 is IShaped s1) 
            {
                objects.ForEach
                (o2 =>
                {
                    if (
                    o2 is IShaped s2 
                    && o1 != o2 
                    && processed.Contains((o2, o1)) == false
                    )
                    {
                        if (Collider.IsIntersects(s1.GetCollider(), s2.GetCollider()))
                        {
                            Collided?.Invoke(this, new ManageCollisionEventArgs(o1, o2));
                            s1.OnCollided(this, new CollisionEventArgs(o2));
                            s2.OnCollided(this, new CollisionEventArgs(o1));
                        }
                    }
                    processed.Add((o1, o2));
                });
            }            
        });
    }

    
}

public class ManageCollisionEventArgs
{
    public IObject Object1 { get; set; }
    public IObject Object2 { get; set; }

    public ManageCollisionEventArgs(IObject obj1, IObject obj2)
    {
        Object1 = obj1;
        Object2 = obj2;
    }
}
public class CollisionEventArgs
{
    public IObject OtherObject { get; set; }
    public CollisionEventArgs(IObject other)
    {
        OtherObject = other;
    }
}
