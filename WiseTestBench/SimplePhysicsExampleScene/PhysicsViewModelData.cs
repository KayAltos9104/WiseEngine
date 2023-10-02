using Microsoft.Xna.Framework;
using WiseEngine.MVP;

namespace WiseTestBench.SimplePhysicsExampleScene;
public class PhysicsViewModelData : ViewModelData
{
    public Vector2 DeltaSpeedPlayer { get; set; } = Vector2.Zero;
    public bool DoPlayerShoot { get; set; } = false;
    public int Score { get; set; } = 0;

    public bool DoJump { get; set; } = false;
}
