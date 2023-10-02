using WiseEngine.MVP;

namespace WiseEngine.PhysicsAndCollisions;

public interface IShaped
{
    event EventHandler<CollisionEventArgs> Collided;
    Collider GetCollider();

    void OnCollided(object sender, CollisionEventArgs e);
    //{
    //    Collided.Invoke(sender, e);
    //}
}
