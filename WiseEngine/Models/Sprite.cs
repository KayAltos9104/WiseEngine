﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WiseEngine.MonogamePart;

namespace WiseEngine.Models;

public class Sprite
{
    //public bool IsCenteredPos { get; private set; }
    public string TextureName { get; set; }
    public Vector2 Pos { get; set; }

    /// <value>
    /// The <c>Scale</c> property represents scale of sprite for drawing
    /// </value>
    public Vector2 Scale { get; private set; }


    public (float Width, float Height) Size {get; private set;}
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
    
    public Sprite(string textureName) : this (textureName, StretchMode.Stretch)
    {

    }
    public Sprite(string textureName, StretchMode stretchMode)
    {
        TextureName = textureName;
        
        //Scale = Vector2.One;
        IsReflectedOY = false;
        IsReflectedOX = false;
        Color = Color.White;
        Alpha = 255;
        Rotation = 0;
        TextureStretchMode = stretchMode;
        Size = (GetTexture().Bounds.Width, GetTexture().Height);     
        CalculateTextureScale();
        Pos = Vector2.Zero;
        //IsCenteredPos = true;
        //CalculatePos();
    }

    public void SetSize (float width, float height)
    {
        Size = (width, height);
        CalculateTextureScale ();
        //CalculatePos();
    }
    public void SwitchPositionRelation ()
    {
        //IsCenteredPos = !IsCenteredPos;
        throw new NotImplementedException ();
    }

    public void CalculatePos()
    {
        //Pos = IsCenteredPos
        //    ? Pos = new Vector2(Pos.X - Size.Width / 2, Pos.Y - Size.Height / 2)
        //    : Pos = new Vector2(Pos.X + Size.Width / 2, Pos.X + Size.Height / 2);
        throw new NotImplementedException();
    }
    public void CalculateTextureScale ()
    {
        switch (TextureStretchMode)
        {
            case StretchMode.Stretch:
                {
                    Scale = new Vector2(Size.Width / TextureSize.Width, Size.Height / TextureSize.Height);
                    break;
                }
            case StretchMode.Multiple:                
            case StretchMode.None:
                {
                    Scale = Vector2.One;
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
