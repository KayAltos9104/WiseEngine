using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using WiseEngine.Models;
using WiseEngine.MonogamePart;

namespace LittleWitch.Prefabs;

public class Sorceress : IObject, IAnimatedSingleFrames
{
    public Vector2 Pos { get; set; }
    public bool IsDisposed { get; set; }
    public float Layer { set; get; }

    public Dictionary<string, AnimationSingleFrames> Animations { set; get; }

    public AnimationSingleFrames CurrentAnimation { set; get; }

    public event EventHandler Died;

    public Sorceress() 
    {
        Pos = Vector2.Zero;
        IsDisposed = false;
        Layer = 0;

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
    }
    public void OnDied()
    {
        throw new NotImplementedException();
    }

    public void SetAnimation(string animationName)
    {
        CurrentAnimation = Animations[animationName];
    }

    public void Update()
    {
        CurrentAnimation.Update();
        
    }
}
