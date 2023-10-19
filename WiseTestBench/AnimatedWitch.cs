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
    private const float _shotCooldown = 800.0f;    
    private float _shotCooldownTime = 0;
    private float _fallingCooldownCheck = 100.0f;
    private States _currentState;
    private bool _shot = true;
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
        witchSheet.SetSize(witchSheet.TextureSize.Width * 2, witchSheet.TextureSize.Height * 2);
        //witchSheet.Scale = Vector2.One * 2;
        Layer = 0;

        // Animations
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
        witchSheet.SetSize(witchSheet.TextureSize.Width * 2, witchSheet.TextureSize.Height * 2);
        //witchSheet.Scale = Vector2.One * 2;
        Animation run = new Animation(runFrames, witchSheet, 50);
        Animations.Add("run", run);


        width = 76;
        height = 49;
        pos = new Point(20, 8);

        AnimationFrame[] attackFrames = new AnimationFrame[10];
        //for (int i = 0; i < attackFrames.Length; i++)
        //{
        //    AnimationFrame a = new AnimationFrame(width, height, pos);
        //    pos.X += 61;
        //    attackFrames[i] = a;
        //}
        attackFrames[0] = new AnimationFrame(25, height, pos);
        pos.X += 61 + 25;
        attackFrames[1] = new AnimationFrame(25, height, pos);
        pos.X += 60 + 25;
        attackFrames[2] = new AnimationFrame(25, height, pos);
        pos.X += 52 + 25;
        attackFrames[3] = new AnimationFrame(36, height, pos);
        pos.X += 34 + 36;
        attackFrames[4] = new AnimationFrame(51, height, pos);
        pos.X += 39 + 51;
        attackFrames[5] = new AnimationFrame(45, height, pos);
        pos.X += 45 + 46;
        attackFrames[6] = new AnimationFrame(74, height, pos);
        pos.X += 74 + 27;
        attackFrames[7] = new AnimationFrame(58, height, pos);
        pos.X += 58 + 27;
        attackFrames[8] = new AnimationFrame(42, height, pos);
        pos.X += 42 + 43;
        attackFrames[9] = new AnimationFrame(42, height, pos);



        witchSheet = new Sprite("WitchAttack");
        witchSheet.SetSize(witchSheet.TextureSize.Width * 2, witchSheet.TextureSize.Height * 2);
        //witchSheet.Scale = Vector2.One * 2;
        Animation attack = new Animation(attackFrames, witchSheet, 50, true);
        
        Animations.Add("shot", attack);

        width = 30;
        height = 49;
        pos = new Point(20, 8);
        AnimationFrame[] jumpFrames = new AnimationFrame[15];
        for (int i = 0; i < jumpFrames.Length; i++)
        {
            AnimationFrame a = new AnimationFrame(width, height, pos);
            pos.X += 55 + width;
            jumpFrames[i] = a;
        }
        witchSheet = new Sprite("WitchJump");
        witchSheet.SetSize(witchSheet.TextureSize.Width * 2, witchSheet.TextureSize.Height * 2);
        //witchSheet.Scale = Vector2.One * 2;
        Animation jump = new Animation(jumpFrames, witchSheet, 30, true);

        Animations.Add("jump", jump);


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
                        foreach (var a in Animations.Values)
                            Graphics2D.ReflectSprite(a.GetSprite());
                        IsLeft = false;
                    }
                    else if (Speed.X < 0)

                    {
                        foreach (var a in Animations.Values)
                            Graphics2D.ReflectSprite(a.GetSprite(), true);
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
                    if (_shotCooldownTime <= 300 && _shot == false)
                    {
                        Shooted?.Invoke(this, EventArgs.Empty);
                        _shot = true;
                    }
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
            _shot = false;
            //GameConsole.WriteLine("shot!");
            _shotCooldownTime = _shotCooldown;
            
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
