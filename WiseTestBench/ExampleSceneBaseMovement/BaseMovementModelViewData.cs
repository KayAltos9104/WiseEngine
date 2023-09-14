using Microsoft.Xna.Framework;

using WiseEngine.MVP;

namespace WiseTestBench.BaseMovementScene;

public class BaseMovementModelViewData : ModelViewData
{
    public Vector2 PlayerPos { get; set; } = Vector2.Zero;
}
