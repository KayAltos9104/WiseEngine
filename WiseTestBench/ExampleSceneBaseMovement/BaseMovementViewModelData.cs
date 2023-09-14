using Microsoft.Xna.Framework;
using WiseEngine.MVP;

namespace WiseTestBench.BaseMovementScene;
public class BaseMovementViewModelData : ViewModelData
{
    public Vector2 DeltaSpeedPlayer { get; set; } = Vector2.Zero;
}
