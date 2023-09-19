using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WiseEngine.MonogamePart;

namespace WiseEngine;
/// <summary>
/// Box with boundaries and text inside
/// </summary>
public class MessageBox : InterfaceComponent    
{   
    public Color BackgroundColor { get; set; }
    public Color ContourColor { get; set; }
    public int ContourWidth { get; set; }
    public MessageBox(Vector2 pos, SpriteFont font, string text) : base(pos, font)
    {
        Pos = pos;
        Text = text;
        TextPos = Vector2.Zero;  
        Layer = 1.0f;         
        IsCentered = true;
        BackgroundColor = Color.DarkSeaGreen;
        ContourColor = Color.Black;
        ContourWidth = 3;
    }
    public override void Render(SpriteBatch spriteBatch)
    {        
        TextColor = Color.Black;
        Graphics2D.FillRectangle(
            (int)Pos.X, (int)Pos.Y,
            Bounds.Width, Bounds.Height,
            BackgroundColor);
        Graphics2D.DrawRectangle((int)Pos.X, (int)Pos.Y,
            Bounds.Width, Bounds.Height,
            ContourColor, ContourWidth);
        RenderText(spriteBatch);       
    }
}
