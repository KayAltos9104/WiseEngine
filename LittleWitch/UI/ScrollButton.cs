using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WiseEngine;
using WiseEngine.Models;
using WiseEngine.MonogamePart;

namespace LittleWitch.UI;

internal class ScrollButton : Button
{
   
    public Sprite Sprite { get; set; }
    public ScrollButton(Vector2 pos, SpriteFont font, string text, string textureName) : base(pos, font, text)
    {        
        Sprite = new Sprite(textureName, Sprite.StretchMode.Stretch);
        Sprite.SetSize(Bounds.Width, Bounds.Height);
        MarginText = new Vector2(0, 10);
    }

    public override void Render(SpriteBatch spriteBatch)
    {
        _textSize = Font.MeasureString(Text) != Vector2.Zero ?
                Font.MeasureString(Text) :
                Vector2.One;
        
        if (IsChosen)
        {            
            TextColor = Color.Yellow;
            Sprite.Color = new Color(200, 200, 200);            
            RenderText(spriteBatch);
        }
        else
        {
            TextColor = Color.Black;
            Sprite.Color = Color.White;   
        }
        Graphics2D.RenderSprite(Pos, Sprite, 0);
        RenderText(spriteBatch);
    }

    public override void ChangeSize(int width, int height)
    {
        base.ChangeSize(width, height);
        Sprite.SetSize(Bounds.Width, Bounds.Height);
    }
}
