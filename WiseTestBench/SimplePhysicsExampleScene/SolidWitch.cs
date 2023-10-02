using Microsoft.Xna.Framework;
using WiseEngine;
using WiseTestBench.ExampleSceneShapeProjectileWork;

namespace WiseTestBench.SimplePhysicsExampleScene;

public class SolidWitch : LittleShapeWitch, ISolid
{
    public SolidWitch(Vector2 initPos) : base(initPos)
    {
    }
    public override void Update()
    {
        PrevPos = new Vector2(Pos.X, Pos.Y);
        base.Update();
    }
    public bool IsStatic { get; set; } = false;
    public float Mass { get; set; }
    public Vector2 Force { get; set; }
    public Vector2 PrevPos { get; set; }
}
