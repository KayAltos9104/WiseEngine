﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
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
            int x = (int)(Pos - (IsCentered ? _textSize / 2 : Vector2.Zero)).X;
            int y = (int)(Pos - (IsCentered ? _textSize / 2 : Vector2.Zero)).Y;
            Graphics2D.FillRectangle(x, y, (int)(_textSize.X + MarginText.X * 2), (int)(_textSize.Y + MarginText.Y * 2), Color.Black);
            Graphics2D.DrawRectangle(x, y, (int)(_textSize.X + MarginText.X * 2), (int)(_textSize.Y + MarginText.Y * 2), Color.Black, 3);
            RenderText(spriteBatch);
        }
        else
        {
            TextColor = Color.Black;
            base.Render(spriteBatch);
        }
    }
}

public class ClickEventArgs:EventArgs
{

}
