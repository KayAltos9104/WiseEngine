﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using WiseEngine.Models;
using WiseEngine.MVP;
using WiseEngine.PhysicsAndCollisions;
using static LittleWitch.Prefabs.Sorceress;

namespace LittleWitch.Prefabs;

public class StaticPlatform : IObject, IRenderable, ISolid
{
    public Vector2 Pos { get; set; }
    public bool IsDisposed { get; set; }
    public List<Sprite> Sprites { get; set; }
    public float Layer { get; set; }
    public bool IsStatic { get; set; }
    public bool IsOnPlatform { get; set; }
    public float Mass { get; set; }
    public Vector2 PrevPos { get; set; }
    public Vector2 Force { get; set; }
    public RectangleCollider Collider { get; set; }

    public event EventHandler Died;
    public event EventHandler<CollisionEventArgs> Collided;

    public StaticPlatform (string spriteName, int sectionsNumber)
    {
        Pos = Vector2.Zero;
        IsDisposed = false;
        Layer = 0.1f;
        Force = Vector2.Zero;        
        PrevPos = Pos;
        IsStatic = true;
        Mass = 50;

        var platformSprite = new Sprite(spriteName);
        platformSprite.TextureStretchMode = Sprite.StretchMode.Multiple;
        platformSprite.SetSize(platformSprite.TextureSize.Width * sectionsNumber, platformSprite.TextureSize.Height * 1);
        //platformSprite.Scale = Vector2.One * 3;
        
        
        Layer = 0;
        Sprites = new()
        {
            platformSprite
        };

        Collider = new RectangleCollider(Vector2.Zero + Vector2.UnitY*10,
            (int)Sprites[0].Size.Width,
            (int)Sprites[0].Size.Height);
        
        Mass = 500;        
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
        Collided?.Invoke(sender, e);
    }

    public void OnDied()
    {
        throw new NotImplementedException();
    }

    public void Update()
    {
        PrevPos = Pos;
        //Pos -= Vector2.UnitY;
    }
}
