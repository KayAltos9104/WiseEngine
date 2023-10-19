using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using WiseEngine.Models;
using WiseEngine.MonogamePart;
using WiseEngine.MVP;
using WiseEngine.PhysicsAndCollisions;

namespace WiseTestBench.SimplePhysicsExampleScene;

public class Gem : IObject, IShaped, IRenderable
{
    public RectangleCollider Collider { get; set; }
    public Vector2 Pos { get; set; }
    public bool IsDisposed { get; set; }
    public List<Sprite> Sprites { get; set; }
    public float Layer { get; set; }

    public event EventHandler Died;
    public event EventHandler<CollisionEventArgs> Collided;

    public Gem (Vector2 initPos)
    {
        Pos = initPos;

        var sprite = new Sprite("Gem1");

        sprite.SetSize(sprite.TextureSize.Width * 0.5f, sprite.TextureSize.Height * 0.5f);
        Sprites = new()
        {
            sprite
        };
        int width = (int)(LoadableObjects.GetTexture(Sprites[0].TextureName).Width * Sprites[0].Scale.X);
        int height = (int)(LoadableObjects.GetTexture(Sprites[0].TextureName).Height * Sprites[0].Scale.Y);
        Collider = new RectangleCollider(Vector2.Zero, width, height);
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
        if (e.OtherObject is SolidWitch || e.OtherObject is AnimatedWitch)
        {
            OnDied();
            Collided?.Invoke(this, e);
        }        
    }

    public void OnDied()
    {
        IsDisposed = true;
        Died?.Invoke(this, EventArgs.Empty);
    }

    public void Update()
    {
        
    }
}
