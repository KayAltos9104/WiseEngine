using Microsoft.Xna.Framework;
using WiseEngine;
using WiseTestBench.ExampleSceneShapeProjectileWork;

namespace WiseTestBench.SimplePhysicsExampleScene;

public class SolidWitch : LittleShapeWitch, ISolid
{
    public SolidWitch(Vector2 initPos) : base(initPos)
    {
    }

    public bool IsStatic { get; set; } = false;
    public float Mass { get; set; }
    public float Force { get; set; }
}
