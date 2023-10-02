using System.Collections.Generic;
using System;
using WiseEngine.Models;
using WiseEngine.MVP;
using WiseEngine.PhysicsAndCollisions;
using WiseTestBench.SimplePhysicsExampleScene;
using Microsoft.Xna.Framework;

namespace WiseTestBench;

public class AnimatedWitch : IObject, IAnimated, ISolid
{
    public RectangleCollider Collider { get; set; }
    public event EventHandler Died;
    public Vector2 Pos { get; set; }
    public bool IsDisposed { get; set; }    
    public float Layer { get; set; }
    public bool IsStatic { get; set; }
    public float Mass { get; set; }
    public Vector2 Speed { get; set; }
    public Vector2 PrevPos { get; set; }
    public Vector2 Force { get; set; }
    public bool IsOnPlatform { get; set; }

    public Dictionary<string, Animation> Animations { get; }

    public Animation CurrentAnimation { get; private set; }

    public event EventHandler<CollisionEventArgs> Collided;

    public AnimatedWitch(Vector2 initPos)
    {
        Pos = initPos;
        Speed = Vector2.Zero;
        var witchSheet = new Sprite("WitchIdle");
        witchSheet.Scale = Vector2.One * 2;
        Animations = new Dictionary<string, Animation>();
        AnimationFrame[] idleFrames = new AnimationFrame[13];
        int width = 25;
        int height = 49;
        Point pos = new Point(20,8);
        for (int i = 0; i < idleFrames.Length; i++)
        {
            AnimationFrame a = new AnimationFrame(width, height, pos);
            pos.X += 85;
            idleFrames[i] = a;
        }

        Animation idle = new Animation(idleFrames, witchSheet, 100);
        Animations.Add("idle", idle);
        SetAnimation("idle");
        CurrentAnimation.Activate();

        Layer = 0;       
        
        Collider = new RectangleCollider(Vector2.Zero, (int)(width*witchSheet.Scale.X), (int)(height * witchSheet.Scale.Y));

        IsDisposed = false;
        IsStatic = false;
        Mass = 50;
        Force = Vector2.Zero;
        Speed = Vector2.Zero;
    }

    public override string ToString()
    {
        string selfInfo = "";
        
        selfInfo += $"Позиция: {Pos}\n";
        selfInfo += $"Скорость: {Speed}\n";

        return selfInfo;
    }
    public virtual void Update()
    {
        PrevPos = Pos;
        Pos += Speed * Globals.Time.ElapsedGameTime.Milliseconds;
        Speed = Vector2.Zero;
        CurrentAnimation.Update();
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
        IsDisposed = true;
    }
    public virtual void OnCollided(object sender, CollisionEventArgs e)
    {
        if (e.OtherObject is SolidGoblin)
        {
            OnDied();
        }

        Collided?.Invoke(sender, e);
    }

    public void SetAnimation(string animationName)
    {
        if (Animations.ContainsKey(animationName))
            CurrentAnimation = Animations[animationName];
    }
}
