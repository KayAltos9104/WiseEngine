using Microsoft.Xna.Framework;
using System.Collections.Generic;
using WiseEngine;

namespace WiseTestBench.ButtonsWorkExampleScene;

public class RedOrb : IObject
{
    public float Layer { get; set; }
    public List<(string ImageName, Vector2 ImagePos)> Sprites { get; set; }
    public Vector2 Scale { get; set; }
    public Vector2 Pos { get; set; }
    public bool IsDisposed { get; set; } = false;
    public Vector2 Speed { get; set; }

    public RedOrb(Vector2 initPos)
    {
        Scale = new Vector2 (0.1f, 0.1f);
        Pos = initPos;
        Speed = Vector2.Zero;
        Sprites = new()
        {
            ("RedOrb", Vector2.Zero)
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
}
