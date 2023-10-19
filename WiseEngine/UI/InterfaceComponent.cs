using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WiseEngine.MonogamePart;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace WiseEngine;
/// <summary>
/// Basic clas for interface components
/// </summary>
public abstract class InterfaceComponent : IComponent
{
    protected Vector2 _textSize;
    public string Name { get; set; }
    
    public Rectangle Bounds { get; set; }
    /// <summary>
    /// Text margin
    /// </summary>
    public Vector2 MarginText { get; set; }
    /// <summary>
    /// List of sprites which are used in component drawing
    /// </summary>
    public List<(string ImageName, Vector2 ImagePos)> Sprites { get; set; }    
    public Vector2 Pos { get; protected set; }
    public Vector2 TextPos { get; set; }
    public string Text { get; set; }
    public bool IsCentered { get; set; }
    public float Layer { get; set; }
    /// <summary>
    /// Component text color
    /// </summary>
    public Color TextColor { get; set; }
    /// <summary>
    /// Component font for text
    /// </summary>
    public SpriteFont Font { get; set; }
    public bool IsChosen { get; set; }
    public bool IsInteractive { get; set; }

    public InterfaceComponent(Vector2 pos, SpriteFont font) 
    {
        Pos = pos;        
        Sprites = new List<(string ImageName, Vector2 ImagePos)>();       
        Layer = 1.0f;
        MarginText = Vector2.Zero;
        TextColor = Color.Black;
        Text = "";
        Name = Guid.NewGuid().ToString();
        Font = font;  
        IsInteractive = false;
        IsChosen = false;
        Bounds = new Rectangle((int)pos.X, (int)pos.Y, 100, 50);
    }
    public virtual void OnClicked()
    {

    }
    /// <summary>
    /// Loads component sprite
    /// </summary>
    /// <param name="spriteName">Sprite name from <see cref="LoadableObjects"/></param>
    /// <param name="pos">Sprite ralative position</param>
    public void LoadSprite(string spriteName, Vector2 pos)
    {
        Sprites.Add((spriteName, pos));
    }
    public abstract void Render(SpriteBatch spriteBatch);  
    /// <summary>
    /// Method renders component sprites
    /// </summary>
    /// <param name="spriteBatch">Monogame <see cref="SpriteBatch"/></param>
    protected void RenderSprites(SpriteBatch spriteBatch)
    {
        foreach (var sprite in Sprites)
        {
            spriteBatch.Draw(
                   texture: LoadableObjects.GetTexture(sprite.ImageName),
                   position: Pos + sprite.ImagePos,
                   sourceRectangle: null,
                   Color.White,
                   rotation: 0,
                   origin: Vector2.Zero,
                   scale: Vector2.One,
                   SpriteEffects.None,
                   layerDepth: Layer);
        }
    }
    /// <summary>
    /// Moves component to the new position
    /// </summary>
    /// <param name="newPos">New component coordinate</param>
    /// <remarks>
    /// Moves both <see cref="Pos"/> and <see cref="Bounds"/>
    /// </remarks>
    public void Transform (Vector2 newPos)
    {
        Pos = newPos;
        var bounds = Bounds.Size;
        Bounds = new Rectangle((int)newPos.X, (int)newPos.Y, bounds.X, bounds.Y);
       
    }
    /// <summary>
    /// Shifts component to make current <see cref="Pos"/> lie in the center of the component
    /// </summary>
    public void Center()
    {
        var newPos = Pos - new Vector2(Bounds.Width / 2, Bounds.Height / 2);
        Transform(newPos);
    }
    /// <summary>
    /// Change <see cref="Bounds"/> of component
    /// </summary>
    /// <param name="width">New width</param>
    /// <param name="height">New height</param>
    public virtual void ChangeSize(int width, int height)
    {
        Bounds = new Rectangle((int)Pos.X, (int)Pos.Y, width, height);
    }
    /// <summary>
    /// Renders component text
    /// </summary>
    /// <param name="spriteBatch">Monogame <see cref="SpriteBatch"/></param>
    protected virtual void RenderText (SpriteBatch spriteBatch) 
    {
        if (Text == null)
            return;
        _textSize = Font.MeasureString(Text) != Vector2.Zero ?
                Font.MeasureString(Text) :
                Vector2.One;
        Vector2 textShift = new Vector2(
             IsCentered ? Bounds.Width / 2 - _textSize.X / 2 : 0 ,
             IsCentered ? Bounds.Height / 2 - _textSize.Y / 2 : 0
            );
        spriteBatch.DrawString(
                    spriteFont: Font,
                    Text,
                    position: Pos + MarginText + textShift,
                    color: TextColor,
                    rotation: 0,
                    origin: Vector2.Zero,
                    scale: 1,
                    SpriteEffects.None,
                    layerDepth: 0
                    );
    }
}
