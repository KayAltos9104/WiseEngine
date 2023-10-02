using WiseEngine.MVP;

namespace WiseEngine;

public interface IPhysics
{
    void Update (List<IObject> objects);
    void SolveCollision (IObject o1, IObject o2);
    void SolveCollision(object sender, ManageCollisionEventArgs e);
}
