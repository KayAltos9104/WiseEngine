using Microsoft.Xna.Framework;
using WiseEngine.MonogamePart;
using WiseEngine;
using WiseTestBench.ExampleSceneTriggerWork;
using WiseEngine.MVP;
using System;
using WiseEngine.PhysicsAndCollisions;
using WiseEngine.Models;

namespace WiseTestBench.ExampleSceneShapeProjectileWork;

public class LittleShapeWitch : ShapeWitch
{
    public LittleShapeWitch(Vector2 initPos) : base(initPos)
    {

        Sprites[0].SetSize(Sprites[0].TextureSize.Width * 1.5f, Sprites[0].TextureSize.Height * 1.5f);
        //Sprites[0].Scale = Vector2.One * 2;
        int width = (int)(Sprites[0].Size.Width);
        int height = (int)(Sprites[0].Size.Height);
        Collider = new RectangleCollider(Vector2.Zero, width, height);
    }

    public override void OnCollided(object sender, CollisionEventArgs e)
    {
        if (e.OtherObject is Goblin)
        {
            OnDied();
        }
            
        base.OnCollided(sender, e);
    }
}
