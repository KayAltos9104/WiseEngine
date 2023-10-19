using Microsoft.Xna.Framework;
using System.Collections.Generic;
using WiseEngine.Models;

namespace LittleWitch.Prefabs;

internal class Background : IRenderable
{
    public List<Sprite> Sprites { get; set; }
    public float Layer { get; set; }
    public Vector2 Pos { get; set; }
    
    public Background ()
    {
        Pos = Vector2.Zero;
        var background = new Sprite("ForestBgrnd", Sprite.StretchMode.None);
        //background.Scale = Vector2.One * 1;
        Layer = 0;
        Sprites = new()
        {
            background
        };
    }
}
