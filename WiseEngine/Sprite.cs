using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WiseEngine.MonogamePart;

namespace WiseEngine;

public class Sprite
{
    public string TextureName { get; set; }
    public Vector2 Pos { get; set; }

    /// <value>
    /// The <c>Scale</c> property represents scale of sprite for drawing
    /// </value>
    public Vector2 Scale { get; set; }

    public bool IsReflectedOY { get; set; }

    public bool IsReflectedOX { get; set; }

    public Color Color { get; set; }

    public int Alpha { get; set; }

    public float Rotation { get; set; }
    public Sprite (string textureName)
    {
        TextureName = textureName;
        Pos = Vector2.Zero; 
        Scale = Vector2.One; 
        IsReflectedOY = false;
        IsReflectedOX = false;
        Color = Color.White;
        Alpha = 255;
        Rotation = 0;
    }

    public Texture2D GetTexture()
    {
        var texture =  LoadableObjects.GetTexture(TextureName);
        if (texture == null)
            throw new ArgumentNullException($"Texture with {TextureName} was not found");
        else
            return texture;
    }
}
