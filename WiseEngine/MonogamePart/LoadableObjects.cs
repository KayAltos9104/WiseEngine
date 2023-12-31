﻿using Microsoft.Xna.Framework.Graphics;

namespace WiseEngine.MonogamePart;
/// <summary>
/// Static class which store game resources in xna format
/// </summary>
public static class LoadableObjects
{    
    private static readonly Dictionary<string, Texture2D> Textures = new();
    private static readonly Dictionary<string, SpriteFont> Fonts = new();
    /// <summary>
    /// Adds new texture in storage
    /// </summary>
    /// <param name="texture">
    /// Texture that should be added
    /// </param>
    public static void AddTexture (string name, Texture2D texture)
    {
        Textures.Add(name, texture);
    }
    /// <summary>
    /// Tries to get texture from storage
    /// </summary>
    /// <param name="key">
    /// Texture id for search
    /// </param>
    public static Texture2D? GetTexture (string name)
    {
        if (Textures.ContainsKey(name))
            return Textures[name];
        else
            return null;
    }
    /// <summary>
    /// Adds new font in storage
    /// </summary>
    /// <param name="name">
    /// Name of font
    /// </param>
    /// <param name="font">
    /// Font that should be added
    /// </param>
    public static void AddFont(string name, SpriteFont font)
    {
        Fonts.Add(name, font);        
    }
    /// <summary>
    /// Tries to get font from storage
    /// </summary>
    /// <param name="key">
    /// Font name for search
    /// </param>
    public static SpriteFont? GetFont(string key)
    {
        if (Fonts.ContainsKey(key))
        {
            return Fonts[key];
        }            
        else
        {
            GameConsole.WriteLine($"No font with name {key}");
            return null;
        }            
    }
}
