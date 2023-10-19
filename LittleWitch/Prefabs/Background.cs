using Microsoft.Xna.Framework;
using System.Collections.Generic;
using WiseEngine.Models;

namespace LittleWitch.Prefabs;

internal class Background : IRenderable
{
    public List<Sprite> Sprites { get; set; }
    public float Layer { get; set; }
    public Vector2 Pos { get; set; }
    
    public Background (float width, float height)
    {
        Pos = Vector2.Zero;
        var background = new Sprite("ForestBgrnd", Sprite.StretchMode.Stretch);
        background.SetSize(width, height);        
        Layer = 0;
        Sprites = new()
        {
            background
        };
    }
}
