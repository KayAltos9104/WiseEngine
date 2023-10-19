using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WiseEngine.MonogamePart;

namespace WiseEngine.Models;

public class Sprite
{
    public string TextureName { get; set; }
    public Vector2 Pos { get; set; }

    /// <value>
    /// The <c>Scale</c> property represents scale of sprite for drawing
    /// </value>
    public Vector2 Scale { get; private set; }
   

    public (float Width, float Height) Size;
    public (float Width, float Height) TextureSize
    {
        get
        {
            var size = GetTexture().Bounds;
            return (size.Width, size.Height);
        }
    }
    public bool IsReflectedOY { get; set; }

    public bool IsReflectedOX { get; set; }

    public Color Color { get; set; }

    public int Alpha { get; set; }

    public float Rotation { get; set; }

    public StretchMode TextureStretchMode { get; set; }

    private Vector2 _scale;
    public Sprite(string textureName, StretchMode stretchMode)
    {
        TextureName = textureName;
        Pos = Vector2.Zero;
        //Scale = Vector2.One;
        IsReflectedOY = false;
        IsReflectedOX = false;
        Color = Color.White;
        Alpha = 255;
        Rotation = 0;
        TextureStretchMode = stretchMode;
        Size = (GetTexture().Bounds.Width, GetTexture().Height);       
    }
    public void CalculateTextureScale ()
    {
        switch (TextureStretchMode)
        {
            case StretchMode.Stretch:
                {
                    _scale = new Vector2(Size.Width / TextureSize.Width, Size.Height / TextureSize.Height);
                    break;
                }
            case StretchMode.Multiple:
                {
                    //int rowNumber = (int)(Size.Height / TextureSize.Height);
                    //int columnNumber = (int)(Size.Width / TextureSize.Width);
                    //rowNumber = rowNumber == 0 ? 1 : rowNumber;
                    //columnNumber = columnNumber == 0 ? 1 : columnNumber;
                    break;
                }
            case StretchMode.None:
                {
                    _scale = Vector2.One;
                    break;
                }               

        }
    }
    public Texture2D GetTexture()
    {
        var texture = LoadableObjects.GetTexture(TextureName);
        if (texture == null)
            throw new ArgumentNullException($"Texture with {TextureName} was not found");
        else
            return texture;
    }

    public enum StretchMode : byte
    {
        None,
        Stretch,
        Multiple
    }
}
