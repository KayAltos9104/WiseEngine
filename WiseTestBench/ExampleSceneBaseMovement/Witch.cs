using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using WiseEngine.Models;
using WiseEngine.MonogamePart;
using WiseEngine.MVP;
using IRenderable = WiseEngine.Models.IRenderable;

namespace WiseTestBench.BaseMovementScene;

public class Witch : IObject, IRenderable
{
    public float Layer { get; set; }
    
    public Vector2 Pos { get; set; }
    public Vector2 Speed { get; set; }

    public bool IsDisposed { get; set; } = false;
    public List<Sprite> Sprites { get; set; }
    public event EventHandler Died;

    public Witch (Vector2 initPos)
    {        
        Pos = initPos;
        Speed = Vector2.Zero;
        var witchSprite = new Sprite("ExampleWitch");
        witchSprite.SetSize(witchSprite.TextureSize.Width * 3, witchSprite.TextureSize.Height * 3);
        //witchSprite.Scale = Vector2.One * 3;
        Layer = 0;
        Sprites = new()
        {
            witchSprite
        };
       
    }
    public void OnDied()
    {
        Died?.Invoke(this, EventArgs.Empty);
        IsDisposed = true;
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
        Pos += Speed * Globals.Time.ElapsedGameTime.Milliseconds;
        Speed = Vector2.Zero;
    }
}
