using Microsoft.Xna.Framework;
using WiseEngine;
using WiseEngine.MonogamePart;
using WiseTestBench.BaseMovementScene;

namespace WiseTestBench.ExampleSceneTriggerWork;

public class ShapeWitch : Witch, IShaped
{
    public RectangleCollider Collider { get; set; }
    public ShapeWitch(Vector2 initPos) : base(initPos)
    {
        int width = (int)(LoadableObjects.GetTexture(Sprites[0].TextureName).Width * Sprites[0].Scale.X);
        int height = (int)(LoadableObjects.GetTexture(Sprites[0].TextureName).Height * Sprites[0].Scale.Y);
        Collider = new RectangleCollider (Vector2.Zero, width, height);
    }
    public Collider GetCollider()
    {
        return new RectangleCollider(Pos + Collider.Position,
            Collider.Area.Width,
            Collider.Area.Height)
        { Color = Collider.Color };
    }
}
