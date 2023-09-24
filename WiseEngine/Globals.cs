using Microsoft.Xna.Framework;

namespace WiseEngine;

public static class Globals
{
    /// <value>
    /// The <c>Resolution</c> property represents a tuple of screen width and height
    /// </value>
    public static (int Width, int Height) Resolution { get; set; }
    public static bool IsFullScreen { get; set; }
    /// <value>
    /// The <c>Time</c> property represents a <see cref="GameTime"/> object in game
    /// </value>
    public static GameTime? Time { get; set;}
    /// <value>
    /// Full path to resources folder
    /// </value>
    public static string? ResourcesPath { get; set; }
    /// <value>
    /// Flag to show/hide sprite borders
    /// </value>
    public static bool SpriteBordersAreVisible { get; set; }
    /// <value>
    /// Flag to show/hide collider borders
    /// </value>
    public static bool CollidersAreVisible { get; set; }
    /// <value>
    /// Flag to show/hide FPS counter
    /// </value>
    public static bool FPSIsVisible { get; set; }

    /// <value>
    /// Flag to show/hide camera area
    /// </value>
    public static bool CameraAreaIsVisible { get; set; }
    static Globals()
    {
        //TODO: Потом сделать, чтобы из файла с настройками тягал
        Resolution = (1600, 900);
        IsFullScreen = false;
        SpriteBordersAreVisible = false;
        CollidersAreVisible = false;
        FPSIsVisible = false;
        CameraAreaIsVisible = false;
    }

}
