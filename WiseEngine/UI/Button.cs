using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WiseEngine.MonogamePart;

namespace WiseEngine;
/// <summary>
/// UI component which can be clicked and then some action can be done
/// </summary>
public class Button : MessageBox
{
    /// <summary>
    /// Occurs when button is clicked
    /// </summary>
    public event EventHandler<ClickEventArgs>? Clicked;
    public Button(Vector2 pos, SpriteFont font, string text) : base(pos, font, text)
    {
        IsChosen = false;
        IsInteractive = true;
    }
    /// <summary>
    /// Raises the <see cref="Clicked"/> event
    /// </summary>
    public override void OnClicked()
    {
        Clicked?.Invoke(this, new ClickEventArgs());
    }
    
    public override void Render(SpriteBatch spriteBatch)
    {
        _textSize = Font.MeasureString(Text) != Vector2.Zero ?
                Font.MeasureString(Text) :
                Vector2.One;
        if (IsChosen)
        {
            TextColor = Color.DarkSeaGreen;
            
            Graphics2D.FillRectangle(
                (int)Pos.X, (int)Pos.Y, 
                Bounds.Width, Bounds.Height, 
                Color.Black);
            Graphics2D.DrawRectangle((int)Pos.X, (int)Pos.Y,
                Bounds.Width, Bounds.Height,
                Color.Black, 3);
            RenderText(spriteBatch);
        }
        else
        {
            base.Render(spriteBatch);
        }
    }
}

public class ClickEventArgs:EventArgs
{

}
