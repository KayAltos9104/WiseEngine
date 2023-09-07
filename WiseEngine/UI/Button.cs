using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WiseEngine.MonogamePart;

namespace WiseEngine;

public class Button : MessageBox
{
    public event EventHandler<ClickEventArgs>? OnClick;
    public Button(Vector2 pos, SpriteFont font, string text) : base(pos, font, text)
    {
        IsChosen = false;
        IsInteractive = true;
    }

    public void PerformClick()
    {
        OnClick?.Invoke(this, new ClickEventArgs());
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

    //protected override void RenderText(SpriteBatch spriteBatch)
    //{
    //    if (Text == null)
    //        return;
    //    _textSize = Font.MeasureString(Text) != Vector2.Zero ?
    //            Font.MeasureString(Text) :
    //            Vector2.One;
    //    Vector2 textShift = new Vector2(
    //         (IsCentered ? Bounds.Width / 2 : 0) - _textSize.X/2,
    //         (IsCentered ? Bounds.Height / 2 : 0) - _textSize.Y/2
    //        );
    //    spriteBatch.DrawString(
    //                spriteFont: Font,
    //                Text,
    //                position: Pos + textShift,
    //                color: TextColor,
    //                rotation: 0,
    //                origin: Vector2.Zero,
    //                scale: 1,
    //                SpriteEffects.None,
    //                layerDepth: 0
    //                );
    //}
}

public class ClickEventArgs:EventArgs
{

}
