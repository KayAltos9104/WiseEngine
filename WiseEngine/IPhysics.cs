using WiseEngine.MVP;

namespace WiseEngine;

public interface IPhysics
{
    void Update (IObject o1, IObject o2);
    void Update(object sender, ManageCollisionEventArgs e);
}
