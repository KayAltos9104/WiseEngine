using WiseEngine.MVP;
using WiseTestBench.ExampleSceneShapeProjectileWork;

namespace WiseTestBench.SimplePhysicsExampleScene;

public class PhysicsModelViewData : ModelViewData
{
    public AnimatedWitch Player { get; set; }
    //public SolidWitch Player { get; set; }
    public bool IsLoosed { get; set; }
    public bool IsWon { get; set; }
    public int Score { get; set; }
}
