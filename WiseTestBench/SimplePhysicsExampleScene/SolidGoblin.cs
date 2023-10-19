using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using WiseEngine.Models;
using WiseEngine.MonogamePart;
using WiseEngine.MVP;
using WiseEngine.PhysicsAndCollisions;
using WiseTestBench.ExampleSceneShapeProjectileWork;

namespace WiseTestBench.SimplePhysicsExampleScene;

public class SolidGoblin : IObject, ISolid, IRenderable
{
    public RectangleCollider Collider { get; set; }
    public Vector2 Pos { get; set; }
    public bool IsDisposed { get; set; }
    public bool IsStatic { get; set; }
    public bool IsOnPlatform { get; set; }
    public float Mass { get; set; }
    public Vector2 Speed { get; set; }
    public Vector2 PrevPos { get; set; }
    public Vector2 Force { get; set; }
    public List<Sprite> Sprites { get; set; }
    public float Layer { get; set; }

    public event EventHandler Died;
    public event EventHandler<CollisionEventArgs> Collided;

    public SolidGoblin(Vector2 initPos) 
    {
        Pos = initPos;
        var sprite = new Sprite("Goblin");
        
        sprite.SetSize(sprite.TextureSize.Width * 1.5f, sprite.TextureSize.Height * 1.5f);
        Sprites = new()
        {
            sprite
        };
        int width = (int)(LoadableObjects.GetTexture(Sprites[0].TextureName).Width * Sprites[0].Scale.X);
        int height = (int)(LoadableObjects.GetTexture(Sprites[0].TextureName).Height * Sprites[0].Scale.Y);
        Collider = new RectangleCollider(Vector2.Zero, width, height);
        IsDisposed = false;
        IsStatic = false;
        Mass = 100;
        Force = Vector2.Zero;        
        Speed = Vector2.Zero;
    }
    public Collider GetCollider()
    {
        return new RectangleCollider(Pos + Collider.Position,
            Collider.Area.Width,
            Collider.Area.Height)
        { Color = Collider.Color };
    }

    public virtual void Update()
    {
        PrevPos = Pos;
        Pos += Speed * Globals.Time.ElapsedGameTime.Milliseconds;
    }

    public void OnCollided(object sender, CollisionEventArgs e)
    {
        if (e.OtherObject is OrbProjectile && !IsDisposed)
        {
            IsDisposed = true;
            Died?.Invoke(this, EventArgs.Empty);            
        }
        Collided?.Invoke(this, e);
    }

    public void OnDied()
    {
        Died?.Invoke(this, EventArgs.Empty);
        IsDisposed = true;
    }
}
