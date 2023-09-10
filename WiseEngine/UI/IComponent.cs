using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace WiseEngine;
public interface IComponent
{
    string Name { get; }
    Rectangle Bounds { get; set; }
    Vector2 Pos { get; }
    Vector2 TextPos { get; set; }
    string Text { get; set; }
    bool IsCentered { get; set; }
    float Layer { get; set; }
    bool IsChosen { get; set; }
    bool IsInteractive { get; set; }
    void PerformClick();
    void Render(SpriteBatch spriteBatch);
}
