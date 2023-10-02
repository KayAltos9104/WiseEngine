using Microsoft.Xna.Framework;
using WiseEngine.MVP;
using WiseEngine.PhysicsAndCollisions;

namespace WiseEngine.Models;

public interface ITrigger
{
    Collider GetCollider();
    string Name { get; }
    Vector2 Pos { get; set; }

    event EventHandler<TriggerEventArgs>? TriggeredInside;
    event EventHandler<TriggerEventArgs>? TriggeredOutside;

    void OnTriggeredInside(object? sender, TriggerEventArgs e);
    void OnTriggeredOutside(object? sender, TriggerEventArgs e);
}
