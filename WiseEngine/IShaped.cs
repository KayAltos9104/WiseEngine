using WiseEngine.MVP;

namespace WiseEngine;

public interface IShaped
{
    event EventHandler<CollisionEventArgs> Collided;
    Collider GetCollider();

    void OnCollided(object sender, CollisionEventArgs e);
    //{
    //    Collided.Invoke(sender, e);
    //}
}
