using Microsoft.Xna.Framework;

namespace WiseEngine;

public interface ITrigger
{  

    event EventHandler<EventArgs>? Triggered;

    void OnTriggered();
}
