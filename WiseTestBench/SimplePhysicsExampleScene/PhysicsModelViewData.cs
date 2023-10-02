using WiseEngine.MVP;
using WiseTestBench.ExampleSceneShapeProjectileWork;

namespace WiseTestBench.SimplePhysicsExampleScene;

public class PhysicsModelViewData : ModelViewData
{
    public LittleShapeWitch Player { get; set; }
    public bool IsLoosed { get; set; }
    public int Score { get; set; }
}
