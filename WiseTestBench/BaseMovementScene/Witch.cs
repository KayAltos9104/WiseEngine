using Microsoft.Xna.Framework;
using System.Collections.Generic;
using WiseEngine;

namespace WiseTestBench.BaseMovementScene;

public class Witch : IObject
{
    public float Layer { get; set; }
    public List<(string ImageName, Vector2 ImagePos)> Sprites { get; set; }
    public Vector2 Pos { get; set; }
    public Vector2 Speed { get; set; }
    public Vector2 Scale { get; set; }
    public Witch (Vector2 initPos)
    {
        //Scale = Vector2.One;
        Scale = new Vector2 (3, 3);
        Pos = initPos;
        Speed = Vector2.Zero;
        Sprites = new()
        {
            ("ExampleWitch", Vector2.Zero)
        };
    }

    public void Update()
    {
        Pos += Speed * Globals.Time.ElapsedGameTime.Milliseconds;
        Speed = Vector2.Zero;
    }
}
