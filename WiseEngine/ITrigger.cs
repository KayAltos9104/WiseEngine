using Microsoft.Xna.Framework;

namespace WiseEngine;

public interface ITrigger : IShaped
{  
    string Name { get; }
    Vector2 Pos { get; set; }

    event EventHandler<EventArgs>? Triggered;

    void OnTriggered(EventArgs e);
}
