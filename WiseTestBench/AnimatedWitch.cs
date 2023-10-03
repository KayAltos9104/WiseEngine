using System.Collections.Generic;
using System;
using WiseEngine.Models;
using WiseEngine.MVP;
using WiseEngine.PhysicsAndCollisions;
using WiseTestBench.SimplePhysicsExampleScene;
using Microsoft.Xna.Framework;
using WiseEngine.MonogamePart;

namespace WiseTestBench;

public class AnimatedWitch : IObject, IAnimated, ISolid
{
    private const float _shotCooldown = 400.0f;    
    private float _shotCooldownTime = 0;
    private float _fallingCooldownCheck = 100.0f;
    private States _currentState;
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
    public bool IsLeft { get; private set; }
    public Dictionary<string, Animation> Animations { get; }

    public Animation CurrentAnimation { get; private set; }

    public event EventHandler<CollisionEventArgs> Collided;
    public event EventHandler Shooted;

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


        Layer = 0;

        Animation idle = new Animation(idleFrames, witchSheet, 100);
        Animations.Add("idle", idle);
        SetAnimation("idle");  
        Collider = new RectangleCollider(Vector2.Zero, (int)(width*witchSheet.Scale.X), (int)(height * witchSheet.Scale.Y));

        width = 34;
        height = 49;
        pos = new Point(20, 8);
        AnimationFrame[] runFrames = new AnimationFrame[16];
        for (int i = 0; i < runFrames.Length; i++)
        {
            AnimationFrame a = new AnimationFrame(width, height, pos);
            pos.X += 85;
            runFrames[i] = a;
        }

        witchSheet = new Sprite("WitchRun");
        witchSheet.Scale = Vector2.One * 2;
        Animation run = new Animation(runFrames, witchSheet, 50);
        Animations.Add("run", run);

        if (CurrentAnimation.GetSprite().IsReflectedOY)
        {
            IsLeft = true;
        }
        else
        {
            IsLeft = false;
        }

        IsDisposed = false;
        IsStatic = false;
        Mass = 50;
        Force = Vector2.Zero;
        Speed = Vector2.Zero;
        _currentState = States.Idle;
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
        if (_currentState != States.Die)
        {
            if (_shotCooldownTime > 0)
            {
                _currentState = States.Shot;
            }
            else if (Force.Y < 0)
            {
                _currentState = States.Jump;
            }
            else if (Force.Y > 2500)
            {
                _currentState = States.Fall;
            }
            else if (Speed.X != 0)
            {
                _currentState = States.Run;
            }           
            else
            {
                _currentState = States.Idle;
            }
        }       

        switch (_currentState)
        {             
            case States.Run:
                {
                    SetAnimation("run");
                    if (Speed.X > 0)
                    {
                        Graphics2D.ReflectSprite(CurrentAnimation.GetSprite());
                        IsLeft = false;
                    }
                    else if (Speed.X < 0)

                    {
                        Graphics2D.ReflectSprite(CurrentAnimation.GetSprite(), true);
                        IsLeft = true;
                    }
                    break;
                }
            case States.Jump:
                {
                    SetAnimation("jump");
                    break;
                }
            case States.Fall:
                {
                    SetAnimation("fall");
                    break;
                }
            case States.Shot:
                {
                    SetAnimation("shot");
                    _shotCooldownTime -= Globals.Time.ElapsedGameTime.Milliseconds;
                    if (_shotCooldownTime <= 0)
                    {
                        _shotCooldownTime = 0;
                        _currentState = States.Idle;
                    }

                    break;
                }
            case States.Die:
                {
                    SetAnimation("death");
                    break;
                }
            case States.Idle:
            default:
                {
                    SetAnimation("idle");
                    break;
                }
                
        }

        Pos += Speed * Globals.Time.ElapsedGameTime.Milliseconds;

        CurrentAnimation.Update();
        //if (_currentState == States.Shot) GameConsole.WriteLine(_currentState.ToString());
        GameConsole.Clear();
        GameConsole.WriteLine(_shotCooldownTime.ToString());
        GameConsole.WriteLine(_currentState.ToString());
        Speed = Vector2.Zero;
        
    }
    public void DoShoot ()
    {
        if (_currentState != States.Shot)
        {
            _currentState = States.Shot;
            //GameConsole.WriteLine("shot!");
            _shotCooldownTime = _shotCooldown;
            Shooted?.Invoke(this, EventArgs.Empty);
        }        
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
        Graphics2D.ReflectSprite(CurrentAnimation.GetSprite(), true, "X");
        _currentState = States.Die;
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
        //TODO: Сделать, чтобы он не выставлял уже текущую анимацию
        if (Animations.ContainsKey(animationName))
        {            
            CurrentAnimation = Animations[animationName];            
        }            
    }

    public enum States : byte
    {
        Idle,
        Run,
        Jump,
        Fall,
        Shot,
        Die
    }
}
