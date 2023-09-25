using Microsoft.Xna.Framework;
using System.Collections.Generic;
using WiseEngine;
using WiseEngine.MonogamePart;

namespace WiseTestBench.ExampleSceneShapeProjectileWork;

public class Goblin : IObject, IShaped, IRenderable
{
    public RectangleCollider Collider { get; set; }
    public Vector2 Pos { get; set; }
    public Vector2 Speed { get; set; }
    public bool IsDisposed { get; set; }
    public List<Sprite> Sprites { get; set; }
    public float Layer { get; set; }

    public Goblin (Vector2 initPos, Vector2 speed)
    {
        Pos = initPos;
        var sprite = new Sprite("Goblin");
        //sprite.IsReflectedOY = true;
        sprite.Scale = new Vector2(1.5f, 1.5f);
        Sprites = new()
        {
            sprite
        };
        int width = (int)(LoadableObjects.GetTexture(Sprites[0].TextureName).Width * Sprites[0].Scale.X);
        int height = (int)(LoadableObjects.GetTexture(Sprites[0].TextureName).Height * Sprites[0].Scale.Y);
        Collider = new RectangleCollider(Vector2.Zero, width, height);
        Speed = speed;
    }
    public Collider GetCollider()
    {
        return new RectangleCollider(Pos + Collider.Position,
            Collider.Area.Width,
            Collider.Area.Height)
        { Color = Collider.Color };
    }

    public void Update()
    {
        Pos += Speed * Globals.Time.ElapsedGameTime.Milliseconds;
    }
}
