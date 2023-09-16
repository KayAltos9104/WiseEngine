using Microsoft.Xna.Framework;
using WiseEngine.MVP;

namespace WiseEngine;

public interface ITrigger : IShaped
{  
    string Name { get; }
    Vector2 Pos { get; set; }

    event EventHandler<TriggerEventArgs>? Triggered;

    void OnTriggered(object? sender, TriggerEventArgs e);
}
