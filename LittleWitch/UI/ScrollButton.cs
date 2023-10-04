using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WiseEngine;
using WiseEngine.MonogamePart;

namespace LittleWitch.UI;

internal class ScrollButton : Button
{
    private Texture2D _texture;
    public ScrollButton(Vector2 pos, SpriteFont font, string text, Texture2D texture) : base(pos, font, text)
    {
        _texture = texture;
        MarginText = new Vector2(0, 0);
    }

    public override void Render(SpriteBatch spriteBatch)
    {
        _textSize = Font.MeasureString(Text) != Vector2.Zero ?
                Font.MeasureString(Text) :
                Vector2.One;

        Vector2 scale = new Vector2(Bounds.Width / _texture.Width, Bounds.Height / _texture.Height);

        if (IsChosen)
        {
            TextColor = Color.Red;
            Graphics2D.RenderTexture(Pos, _texture, new Color(200, 200, 200), scale);
            RenderText(spriteBatch);
        }
        else
        {
            TextColor = Color.Black;
            Graphics2D.RenderTexture(Pos, _texture, Color.White, scale);
            RenderText(spriteBatch);
        }
    }
}
