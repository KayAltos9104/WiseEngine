using Microsoft.Xna.Framework;
using WiseEngine.MVP;

namespace WiseTestBench.ButtonsWorkExampleScene;

public class ButtonsWorkExampleViewModelData : ViewModelData
{
    public Vector2 DeltaSpeedPlayer { get; set; } = Vector2.Zero;
    public Point CursorPosition { get; set; } = Point.Zero;

    public bool DoPlayerShoot { get; set; } = false;
}
