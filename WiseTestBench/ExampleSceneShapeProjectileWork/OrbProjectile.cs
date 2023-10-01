using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using WiseEngine;
using WiseEngine.MonogamePart;
using WiseEngine.MVP;

namespace WiseTestBench.ExampleSceneShapeProjectileWork;

public class OrbProjectile : IObject, IShaped, IRenderable
{
    private Vector2 _speed;

    public event EventHandler<CollisionEventArgs> Collided;

    public RectangleCollider Collider { get; set; }
    public Vector2 Pos { get; set; }
    public bool IsDisposed { get; set; }
    public List<Sprite> Sprites { get; set; }
    public float Layer { get; set; }

    public OrbProjectile(Vector2 initPos, Vector2 speed) 
    {
        Pos = initPos;
        _speed = speed;
        var redOrbSprite = new Sprite("RedOrb");
        redOrbSprite.Scale = new Vector2(0.2f, 0.1f);
        Sprites = new()
        {
            redOrbSprite
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

    public void Update()
    {
        Pos += _speed * Globals.Time.ElapsedGameTime.Milliseconds;
    }

    public void OnCollided(object sender, CollisionEventArgs e)
    {
        Collided?.Invoke(this, e);
    }
}
