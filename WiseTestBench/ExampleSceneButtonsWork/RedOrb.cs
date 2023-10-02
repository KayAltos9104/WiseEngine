using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using WiseEngine;

namespace WiseTestBench.ButtonsWorkExampleScene;

public class RedOrb : IObject, IRenderable
{    
    public Vector2 Pos { get; set; }
    public bool IsDisposed { get; set; } = false;
    public Vector2 Speed { get; set; }
    public List<Sprite> Sprites { get; set; }
    public float Layer { get; set; } = 0;
    public event EventHandler Died;

    public RedOrb(Vector2 initPos)
    {        
        Pos = initPos;
        Speed = Vector2.Zero;
        var redOrbSprite = new Sprite("RedOrb");
        redOrbSprite.Scale = new Vector2(0.1f, 0.1f);
        Sprites = new()
        {
            redOrbSprite
        };
    }
    public void Update()
    {
        Move();
    }

    private void Move ()
    {
        Pos += Speed * Globals.Time.ElapsedGameTime.Milliseconds;
    }
    public void OnDied()
    {
        Died?.Invoke(this, EventArgs.Empty);
        IsDisposed = true;
    }
}
