using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WiseEngine.MonogamePart;

namespace WiseEngine;
/// <summary>
/// 
/// </summary>
public class MessageBox : InterfaceComponent    
{   
    public MessageBox(Vector2 pos, SpriteFont font, string text) : base(pos, font)
    {
        Pos = pos;
        Text = text;
        TextPos = Vector2.Zero;  
        Layer = 1.0f;         
        IsCentered = true;
    }
    public void Move(Vector2 pos)
    {
        Pos = pos;
    }

    public override void Render(SpriteBatch spriteBatch)
    {        
        TextColor = Color.Black;
        Graphics2D.FillRectangle(
            (int)Pos.X, (int)Pos.Y,
            Bounds.Width, Bounds.Height,
            Color.DarkSeaGreen);
        Graphics2D.DrawRectangle((int)Pos.X, (int)Pos.Y,
            Bounds.Width, Bounds.Height,
            Color.Black, 3);
        RenderText(spriteBatch);       
    }
}
