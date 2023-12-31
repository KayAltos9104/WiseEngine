﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using WiseEngine.Models;
using WiseEngine.MonogamePart;
using WiseEngine.MVP;
using WiseEngine.PhysicsAndCollisions;
using WiseTestBench.ExampleSceneShapeProjectileWork;

namespace WiseTestBench.SimplePhysicsExampleScene;

public class SolidWitch : IObject, IRenderable, ISolid
{
    public RectangleCollider Collider { get; set; }
    public event EventHandler Died;
    public Vector2 Pos { get; set; }
    public bool IsDisposed { get; set; }
    public List<Sprite> Sprites { get; set; }
    public float Layer { get; set; }
    public bool IsStatic { get; set; }
    public float Mass { get; set; }
    public Vector2 Speed { get; set; }
    public Vector2 PrevPos { get; set; }
    public Vector2 Force { get; set; }
    public bool IsOnPlatform { get; set; }

    public bool IsLeft { get; private set; }

    public event EventHandler<CollisionEventArgs> Collided;

    public SolidWitch(Vector2 initPos)
    {
        Pos = initPos;
        Speed = Vector2.Zero;
        var witchSprite = new Sprite("ExampleWitch");
        witchSprite.SetSize(witchSprite.TextureSize.Width * 2, witchSprite.TextureSize.Height * 2);
        //witchSprite.Scale = Vector2.One * 2;
        Layer = 0;
        Sprites = new()
        {
            witchSprite
        };
        int width = (int)(witchSprite.Size.Width);
        int height = (int)(witchSprite.Size.Height);
        Collider = new RectangleCollider(Vector2.Zero, width, height);

        IsDisposed = false;
        IsStatic = false;
        Mass = 50;
        Force = Vector2.Zero;       
        Speed = Vector2.Zero;

        if (Sprites[0].IsReflectedOY) 
        {
            IsLeft = true;
        }
        else
        {
            IsLeft = false;
        }
    }

    public override string ToString()
    {
        string selfInfo = "";
        selfInfo += $"Спрайт: {Sprites[0].TextureName}\n";
        selfInfo += $"Позиция: {Pos}\n";
        selfInfo += $"Скорость: {Speed}\n";

        return selfInfo;
    }
    public virtual void Update()
    {
        PrevPos = Pos;
        Pos += Speed * Globals.Time.ElapsedGameTime.Milliseconds;

        if (Speed.X > 0)
        {
            Graphics2D.ReflectSprite(Sprites);
            IsLeft = false;
        }
        else if (Speed.X < 0)
            
        {
            Graphics2D.ReflectSprite(Sprites, true);
            IsLeft = true;
        }       
        Speed = Vector2.Zero;
    }

    public Collider GetCollider()
    {
        return new RectangleCollider(Pos + Collider.Position,
            Collider.Area.Width,
            Collider.Area.Height)
        { Color = Collider.Color };
    }
    public void OnDied()
    {
        Died?.Invoke(this, EventArgs.Empty);
        Graphics2D.ReflectSprite(Sprites, true, "X");        
    }
    public virtual void OnCollided(object sender, CollisionEventArgs e)
    {
        if (e.OtherObject is SolidGoblin)
        {
            OnDied();            
        }

        Collided?.Invoke(sender, e);
    }
}

