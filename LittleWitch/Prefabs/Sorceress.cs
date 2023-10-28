using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using WiseEngine.Models;
using WiseEngine.MonogamePart;
using WiseEngine.MVP;
using WiseEngine.PhysicsAndCollisions;

namespace LittleWitch.Prefabs;

public class Sorceress : IObject, IAnimatedSingleFrames, ISolid
{
    private float _xAcceleration;
    public bool IsLeft {  get; private set; }
    public Vector2 Speed { get; set; }
    public Vector2 Pos { get; set; }
    public bool IsDisposed { get; set; }
    public float Layer { set; get; }

    public Dictionary<string, AnimationSingleFrames> Animations { set; get; }

    public AnimationSingleFrames CurrentAnimation { set; get; }
    public bool IsStatic { set; get; }
    public bool IsOnPlatform { set; get; }
    public float Mass { set; get; }
    public Vector2 PrevPos { set; get; }
    public Vector2 Force { set; get; }
    public RectangleCollider Collider { get; set; }

    public event EventHandler Died;
    public event EventHandler<CollisionEventArgs> Collided;

    public Sorceress() 
    {
        Pos = Vector2.Zero;
        IsDisposed = false;
        Layer = 0;
        Force = Vector2.Zero;
        Speed = Vector2.Zero;
        PrevPos = Pos;
        IsStatic = true;
        Mass = 50;        
        _xAcceleration = 0.45f;

        Animations = new Dictionary<string, AnimationSingleFrames>();
        var frames = new Sprite[13];       
        for (int i = 1; i <= 13; i++)
        {
            var frame = new Sprite($"Witch_Idle{i}", Sprite.StretchMode.Stretch);
            frame.SetSize(frame.TextureSize.Width * 2, frame.TextureSize.Height * 2);
            
            frames[i - 1] = frame;
        }

        

        AnimationSingleFrames idle = new AnimationSingleFrames(frames, 100);
        Animations.Add("idle", idle);
        SetAnimation("idle");

        frames = new Sprite[14];
        for (int i = 1; i <= 14; i++)
        {            
            var frame = new Sprite($"Witch_Run{i}", Sprite.StretchMode.Stretch);
            frame.SetSize(frame.TextureSize.Width * 2, frame.TextureSize.Height * 2);

            frames[i - 1] = frame;
        }

        AnimationSingleFrames run = new AnimationSingleFrames(frames, 20);
        Animations.Add("run", run);
        SetAnimation("run");

        Collider = new RectangleCollider(Vector2.Zero, 
            (int)CurrentAnimation.GetCurrentFrame().Size.Width,
            (int)CurrentAnimation.GetCurrentFrame().Size.Height);
    }
    public void OnDied()
    {
        throw new NotImplementedException();
    }

    public void SetAnimation(string animationName)
    {
        CurrentAnimation = Animations[animationName];
    }
    public void Run (Vector2 direction)
    {
        Speed += direction * _xAcceleration;
    }
    public void Update()
    {
        PrevPos = Pos;
        Pos += Speed * Globals.Time.ElapsedGameTime.Milliseconds;  

        if (Speed.X != 0)
        {
            SetAnimation("run");
        }
        else
        {
            SetAnimation("idle");
        }

        CurrentAnimation.Update();
        if (Speed.X > 0)
        {
            IsLeft = false; 
        }
        else if (Speed.X < 0)
        {
            IsLeft = true;
        }

        CurrentAnimation.GetCurrentFrame().IsReflectedOY = IsLeft;

        Speed = Vector2.Zero;
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
        throw new NotImplementedException();
    }
}
