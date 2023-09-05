using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    public static GameTime Time { get; set;}
    /// <value>
    /// Full path to resources folder
    /// </value>
    public static string ResourcesPath { get; set; }

    public static bool SpriteBordersAreVisible { get; set; } = false;
    public static bool CollidersAreVisible { get; set; } = false;
    static Globals()
    {
        //TODO: Потом сделать, чтобы из файла с настройками тягал
        Resolution = (1600, 900);
        IsFullScreen = false;        
    }

}
