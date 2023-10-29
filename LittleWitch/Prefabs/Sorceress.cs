using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using WiseEngine.Models;
using WiseEngine.MVP;
using WiseEngine.PhysicsAndCollisions;

namespace LittleWitch.Prefabs;

public class Sorceress : IObject, IAnimatedSingleFrames, ISolid
{

    private float _xAcceleration;
    private bool _isLive;
    private bool _doShoot;
    private float _jumpForce;
    private float _jumpDelay;
    private bool _isJumped;
    public State PreviousGameState { get; private set; }
    public State GameState { get; private set; }
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
        Layer = 0.0f;
        Force = Vector2.Zero;
        Speed = Vector2.Zero;
        PrevPos = Pos;
        //IsStatic = true;
        Mass = 50;        
        _xAcceleration = 0.45f;
        _isLive = true;
        _jumpForce = 40000;
        _jumpDelay = 0;
        _isJumped = false;
        GameState = State.Idle;

        AnimationInitialize();

        SetAnimation("idle");
        Collider = new RectangleCollider(Vector2.Zero, 
            (int)CurrentAnimation.GetCurrentFrame().Size.Width,
            (int)CurrentAnimation.GetCurrentFrame().Size.Height);
    }

    private void AnimationInitialize()
    {
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


        frames = new Sprite[14];
        for (int i = 1; i <= 14; i++)
        {
            var frame = new Sprite($"Witch_Run{i}", Sprite.StretchMode.Stretch);
            frame.SetSize(frame.TextureSize.Width * 2, frame.TextureSize.Height * 2);

            frames[i - 1] = frame;
        }

        AnimationSingleFrames run = new AnimationSingleFrames(frames, 10);
        Animations.Add("run", run);

        frames = new Sprite[8];
        for (int i = 1; i <= 8; i++)
        {
            var frame = new Sprite($"Witch_Jump{i}", Sprite.StretchMode.Stretch);
            frame.SetSize(frame.TextureSize.Width * 2, frame.TextureSize.Height * 2);

            frames[i - 1] = frame;
        }

        AnimationSingleFrames jump = new AnimationSingleFrames(frames, 20, isCycled: false);
        
        Animations.Add("jump", jump);
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
    public void Jump ()
    {
        if (IsOnPlatform && _jumpDelay == 0)
        {
            //Force += new Vector2(0, -_jumpForce);
            _jumpDelay = Animations["jump"].SwitchingTime * 3;
        }
    }
    public void Update()
    {
        PreviousGameState = GameState;

        if (_isLive == false) GameState = State.Losing;
        else if (_doShoot) GameState = State.Attack;
        else if (_jumpDelay > 0 || Force.Y < 0) GameState = State.Jump;
        else if (Force.Y > 0) GameState = State.Fall;
        else if (Speed.X != 0) GameState = State.Run;
        else GameState = State.Idle;

        PrevPos = Pos;
        
        
        switch (GameState)
        {
            case State.Losing:
                {
                    throw new NotImplementedException("Пока не сделал");
                    break;
                }
            case State.Attack:
                {
                    throw new NotImplementedException("Пока не сделал");
                    break;
                }
            case State.Jump:
                {
                    if (PreviousGameState != GameState)
                    {
                        SetAnimation("jump");
                        CurrentAnimation.Activate();
                        _isJumped = false;
                    }
                    else
                    {
                        if (_isJumped == false)
                        {
                            _jumpDelay -= Globals.Time.ElapsedGameTime.Milliseconds;
                            if (_jumpDelay < 0)
                            {
                                Force += new Vector2(0, -_jumpForce);
                                _isJumped = true;
                                _jumpDelay = 0;
                            }
                            else
                            {
                                Speed = new Vector2 (0, Speed.Y);
                            }
                        }                       
                        
                    }    
                    break;
                }
            case State.Fall:
                {
                    //throw new NotImplementedException("Пока не сделал");
                    break;
                }
            case State.Run:
                {
                    if (PreviousGameState != GameState)
                    {
                        SetAnimation("run");                       
                    }
                    
                    break;
                }
            case State.Idle:
                {
                    if (PreviousGameState != GameState)
                    {
                        SetAnimation("idle");
                    }
                    break;
                }
        }
         
        if (Speed.X > 0)
        {
            IsLeft = false;
        }
        else if (Speed.X < 0)
        {
            IsLeft = true;
        }
        Pos += Speed * Globals.Time.ElapsedGameTime.Milliseconds;
        CurrentAnimation.Update();  
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
        Collided?.Invoke(sender, e);
    }

    public enum State : byte
    {
        Idle,
        Run,
        Jump,
        Fall,
        Attack,
        TakeDamage,
        Losing
    }
}
