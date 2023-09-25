using Microsoft.Xna.Framework;
using WiseEngine.MVP;

namespace WiseTestBench.ExampleSceneShapeProjectileWork;

public class ProjectileWorkModelViewData : ModelViewData
{
    //public Vector2 PlayerPos { get; set; } = Vector2.Zero;
    public LittleShapeWitch Player { get; set; }
    public bool IsLoosed { get; set; }
    public int Score { get; set; }
}
