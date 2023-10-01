using Microsoft.Xna.Framework;
using WiseEngine;
using WiseTestBench.ExampleSceneShapeProjectileWork;

namespace WiseTestBench.SimplePhysicsExampleScene;

public class SolidGoblin : Goblin, ISolid
{
    public SolidGoblin(Vector2 initPos, Vector2 speed) : base(initPos, speed)
    {
    }

    public override void Update()
    {
        PrevPos = new Vector2 (Pos.X, Pos.Y);
        base.Update();
    }
    public bool IsStatic { get; set; } = true;
    public float Mass { get; set; }
    public float Force { get; set; }
    public Vector2 PrevPos { get; set; }
}
