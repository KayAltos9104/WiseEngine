using System.Data;
namespace WiseEngine.MVP;

public sealed class CollisionManager
{
    //private List<IShaped> _shapedObjects;

    public event EventHandler<CollisionEventArgs>? Collided;
    public event EventHandler<CollisionEventArgs>? TriggerRemoved;
    //public void AddShapedObject (IShaped obj)
    //{
    //    _shapedObjects.Add (obj);
    //}
    //public void RemoveShapedObject(IShaped obj)
    //{
    //    _shapedObjects.Remove (obj);        
    //}
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
                    if (o2 is IShaped s2 && o1 != o2 && processed.Contains((o2, o1)) == false)
                    {
                        //Collided?.Invoke(this, new CollisionEventArgs(o1 as IObject, o2 as IObject));
                        s1.OnCollided(this, new CollisionEventArgs(o2));
                        s2.OnCollided(this, new CollisionEventArgs(o2));
                    }
                    processed.Add((o1, o2));
                });
            }            
        });
    }
}

//public class CollisionEventArgs
//{
//    public IObject Object1 { get; set; }
//    public IObject Object2 { get; set; }

//    public CollisionEventArgs (IObject obj1, IObject obj2)
//    {
//        Object1 = obj1;
//        Object2 = obj2;
//    }
//}
public class CollisionEventArgs
{
    public IObject OtherObject { get; set; }
    public CollisionEventArgs(IObject other)
    {
        OtherObject = other;
    }
}
