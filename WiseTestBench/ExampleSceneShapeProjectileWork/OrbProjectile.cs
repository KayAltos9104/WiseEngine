using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using WiseEngine.Models;
using WiseEngine.MonogamePart;
using WiseEngine.MVP;
using WiseEngine.PhysicsAndCollisions;
using WiseTestBench.SimplePhysicsExampleScene;

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
    public event EventHandler Died;

    public OrbProjectile(Vector2 initPos, Vector2 speed) 
    {
        Pos = initPos;
        _speed = speed;
        var redOrbSprite = new Sprite("RedOrb");
        redOrbSprite.SetSize(redOrbSprite.TextureSize.Width * 0.2f, redOrbSprite.TextureSize.Height * 0.1f);
        //redOrbSprite.Scale = new Vector2(0.2f, 0.1f);
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
        if (e.OtherObject is Goblin || e.OtherObject is SolidGoblin || e.OtherObject is Platform)
            OnDied();
        Collided?.Invoke(this, e);
    }

    public void OnDied()
    {
        Died?.Invoke(this, EventArgs.Empty);
        IsDisposed = true;
    }
}
