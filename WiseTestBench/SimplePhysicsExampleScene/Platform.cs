using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using WiseEngine.Models;
using WiseEngine.MonogamePart;
using WiseEngine.MVP;
using WiseEngine.PhysicsAndCollisions;

namespace WiseTestBench.SimplePhysicsExampleScene;

public class Platform : IObject, ISolid, IRenderable
{
    public RectangleCollider Collider { get; set; }
    public event EventHandler Died;
    public Vector2 Pos { get; set; }
    public bool IsDisposed { get; set; }
    public bool IsStatic { get; set; }
    public float Mass { get; set; }
    public Vector2 Speed { get; set; }
    public Vector2 PrevPos { get; set; }
    public Vector2 Force { get; set; }
    public List<Sprite> Sprites { get; set; }
    public float Layer { get; set; }
    public bool IsOnPlatform { get; set; }

    public event EventHandler<CollisionEventArgs> Collided;

    public Platform(Vector2 initPos) 
    {
        Pos = initPos;
        Speed = Vector2.Zero;
        var platformSprite = new Sprite("Platform3");
        //platformSprite.Scale = Vector2.One * 1;
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

    public Collider GetCollider()
    {
        return new RectangleCollider(Pos + Collider.Position,
            Collider.Area.Width,
            Collider.Area.Height)
        { Color = Collider.Color };
    }

    public void OnCollided(object sender, CollisionEventArgs e)
    {
        Collided?.Invoke(this, e);
    }

    public void Update()
    {
        PrevPos = Pos;
    }

    public void OnDied()
    {
        Died?.Invoke(this, EventArgs.Empty);
        IsDisposed = true;
    }
}
