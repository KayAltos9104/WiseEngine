using Microsoft.Xna.Framework;

namespace WiseEngine;

public interface ITrigger : IShaped
{  
    Vector2 Pos { get; set; }

    event EventHandler<EventArgs>? Triggered;

    void OnTriggered();
}
