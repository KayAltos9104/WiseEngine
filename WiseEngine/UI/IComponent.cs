using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace WiseEngine;
/// <summary>
/// Interface for UI components
/// </summary>
public interface IComponent
{
    /// <summary>
    /// Component name
    /// </summary>
    string Name { get; }
    /// <summary>
    /// Geometrical area of component
    /// </summary>
    /// <remarks>
    /// Used for click and other actions
    /// </remarks>
    Rectangle Bounds { get; set; }
    /// <summary>
    /// Component position
    /// </summary>
    Vector2 Pos { get; }
    /// <summary>
    /// Component text position
    /// </summary>
    Vector2 TextPos { get; set; }
    /// <summary>
    /// Component text
    /// </summary>
    string Text { get; set; }
    /// <summary>
    /// <c>True</c> if text should be in the center of bounds
    /// </summary>
    bool IsCentered { get; set; }
    /// <summary>
    /// Drawinf layer
    /// </summary>
    /// <remarks>
    /// <c>1.0</c> is in the top
    /// </remarks>
    float Layer { get; set; }
    /// <summary>
    /// <c>True</c> if component was chosen by user
    /// </summary>
    bool IsChosen { get; set; }
    /// <summary>
    /// <c>True</c> if component is interactive (button, for example)
    /// </summary>
    bool IsInteractive { get; set; }
    /// <summary>
    /// Method is called when element was affected by user
    /// </summary>
    void PerformClick();
    /// <summary>
    /// Method contains code to draw element
    /// </summary>
    /// <param name="spriteBatch"><see cref="SpriteBatch"/> of game</param>
    void Render(SpriteBatch spriteBatch);
}
