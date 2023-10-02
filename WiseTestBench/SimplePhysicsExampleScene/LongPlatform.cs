

using WiseEngine.MonogamePart;
using WiseEngine;
using Microsoft.Xna.Framework;
using WiseEngine.Models;
using WiseEngine.PhysicsAndCollisions;

namespace WiseTestBench.SimplePhysicsExampleScene;

public class LongPlatform : Platform
{
    public LongPlatform(Vector2 initPos) : base(initPos)
    {
        Pos = initPos;
        Speed = Vector2.Zero;
        var platformSprite = new Sprite("Platform3");
        platformSprite.Scale = Vector2.One * 1;
        platformSprite.Scale *= new Vector2(2, 1);
        Layer = 0;
        Sprites = new()
        {
            platformSprite
        };
        int width = (int)(LoadableObjects.GetTexture(Sprites[0].TextureName).Width * Sprites[0].Scale.X);
        int height = (int)(LoadableObjects.GetTexture(Sprites[0].TextureName).Height * Sprites[0].Scale.Y);
        Collider = new RectangleCollider(Vector2.Zero, width, height);
        IsStatic = true;
        Mass = 500;
        Speed = Vector2.Zero;
        PrevPos = Vector2.Zero;
        Force = Vector2.Zero;
    }
}
