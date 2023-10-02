using WiseEngine.Models;
using WiseEngine.MVP;

namespace WiseEngine.PhysicsAndCollisions;

public interface IPhysics
{
    void Update(IObject objects);
    void Update(List<IObject> objects);
    void SolveCollision(IObject o1, IObject o2);
    void SolveCollision(object sender, ManageCollisionEventArgs e);
}
