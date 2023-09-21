using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace WiseEngine.MonogamePart;
/// <summary>
/// Single class which manages all work with 2D graphics
/// </summary>
public static class Graphics2D
{
    
    /// <summary>
    /// Common Monogame <see cref="SpriteBatch"/> for drawing
    /// </summary>
    public static SpriteBatch SpriteBatch { get; set; }
    /// <summary>
    /// Monogame <see cref="GraphicsDeviceManager"/>
    /// </summary>
    public static GraphicsDeviceManager Graphics { get; set; }
    /// <summary>
    /// Value of objects shifts for drawing - in other words, its view shift for camera
    /// </summary>
    /// <remarks>
    /// In future camera will be implemented with <see cref="Matrix"/> class
    /// </remarks>
    public static Vector2 VisualShift = new Vector2(0, 0);   
    
    
    /// <summary>
    /// Renders <see cref="IObject">game object</see>
    /// </summary>
    /// <param name="obj">Object for drawing</param>
    public static void RenderObject(IObject obj, Camera2D camera)
    {
        DrawRectangle(
            camera.VisionArea.X, 
            camera.VisionArea.Y, 
            camera.VisionArea.Width, 
            camera.VisionArea.Height, Color.Violet, 
            4);

        foreach (var sprite in obj.Sprites)
        {
            //if (sprite.ImageName == "NULL")
            //    continue;
            Vector2 v = obj.Pos + sprite.ImagePos - VisualShift;
            if (camera.IsInVisionArea(v))
            {
                SpriteBatch.Draw(
                texture: LoadableObjects.GetTexture(sprite.ImageName),
                position: obj.Pos - VisualShift + sprite.ImagePos,
                sourceRectangle: null,
                Color.White,
                rotation: 0,
                origin: Vector2.Zero,
                scale: obj.Scale,
                SpriteEffects.None,
                layerDepth: obj.Layer);

                if (Globals.SpriteBordersAreVisible)
                {
                    DrawRectangle((int)v.X, (int)v.Y,
                        (int)(LoadableObjects.GetTexture(sprite.ImageName).Width * obj.Scale.X),
                        (int)(LoadableObjects.GetTexture(sprite.ImageName).Height * obj.Scale.Y),
                        Color.Red, 3);
                }

                if (Globals.CollidersAreVisible && obj is IShaped)
                {                    
                        (obj as IShaped).GetCollider().Draw(SpriteBatch);
                }
            }
        }
    }
    /// <summary>
    /// Renders text
    /// </summary>
    /// <param name="pos">Text position</param>
    /// <param name="text">Text</param>
    /// <param name="font">Font</param>
    /// <param name="textColor">Text color</param>
    public static void OutputText(Vector2 pos, string text, SpriteFont font, Color textColor)
    {
        var textSize = font.MeasureString(text);
        Vector2 textShift = new Vector2(
            (textSize.X - textSize.X) / 2,
            (textSize.Y - textSize.Y) / 2
            );
        SpriteBatch.DrawString(
                    spriteFont: font,
                    text: text,
                    position: pos,
                    color: textColor,
                    rotation: 0,
                    origin: Vector2.Zero,
                    scale: 1,
                    SpriteEffects.None,
                    layerDepth: 0
                    );
    }
    /// <summary>
    /// Renders text with default font and color
    /// </summary>
    /// <param name="pos">Text position</param>
    /// <param name="text">Text</param>
    public static void OutputText (Vector2 pos, string text)
    {
        OutputText(pos, text, LoadableObjects.GetFont("SystemFont"), Color.Yellow);
    }

    //public static void RenderAnimation(IAnimated a)
    //{
    //    if (a.Animation.ActiveAnimation != null &&
    //        a.Animation.ActiveAnimation.IsActive)
    //    {
    //        var centerShift = a.Animation.ActiveAnimation.IsCentered ?
    //            new Vector2(a.Animation.ActiveAnimation.CurrentFrame.Width / 2,
    //            a.Animation.ActiveAnimation.CurrentFrame.Height / 2) :
    //            Vector2.Zero;
    //        Vector2 v = a.Animation.Pos - VisualShift;
    //        if (IsInVisionArea(v))
    //        {
    //            SpriteBatch.Draw(
    //           LoadableObjects.GetTexture(a.Animation.ActiveAnimation.GetPictureName()),
    //            v,
    //            new Rectangle(
    //                a.Animation.ActiveAnimation.CurrentFrame.Point,
    //                new Point(
    //                a.Animation.ActiveAnimation.CurrentFrame.Width,
    //                a.Animation.ActiveAnimation.CurrentFrame.Height)),
    //            Color.White,
    //            0,
    //            centerShift,
    //            1,
    //            SpriteEffects.None, a.Animation.Layer);
    //        }
    //    }
    //}
    /// <summary>
    /// Renders straight line
    /// </summary>
    /// <param name="point1">Line begin</param>
    /// <param name="point2">Line end</param>
    /// <param name="color">Line color</param>
    public static void DrawLine(Vector2 point1, Vector2 point2, Color color)
    {
        DrawLine(point1, point2, color, 1);
    }
    /// <summary>
    /// Renders straight line
    /// </summary>
    /// <param name="point1">Line begin</param>
    /// <param name="point2">Line end</param>
    /// <param name="color">Line color</param>
    /// <param name="width">Line width</param>
    /// <remarks>
    /// Made by ChatGPT
    /// </remarks>
    public static void DrawLine(Vector2 point1, Vector2 point2, Color color, int width)
    {
        Texture2D pixel = new Texture2D(SpriteBatch.GraphicsDevice, 1, width);
        var colorArray = new Color[width];
        for (int i = 0; i < width; i++)
        {
            colorArray[i] = Color.White;
        }
        pixel.SetData(colorArray);
        float angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
        float length = Vector2.Distance(point1, point2);

        SpriteBatch.Draw(pixel, point1, null, color, angle, Vector2.Zero, new Vector2(length, 1), SpriteEffects.None, 0);
    }

    public static void DrawRectangle(int x, int y, int width, int height, Color color)
    {
        DrawRectangle(x, y, width, height, color, 1);
    }
    /// <summary>
    /// Renders rectangle
    /// </summary>
    /// <param name="x">X coordinate for left top rectangle corner</param>
    /// <param name="y">Y coordinate for left top rectangle corner</param>
    /// <param name="width">Rectangle width</param>
    /// <param name="height">Rectangle height</param>
    /// <param name="color">Rectangle contour color</param>
    /// <param name="contourWidth">Rectangle contour width</param>
    /// <remarks>
    /// Made by ChatGPT
    /// </remarks>
    public static void DrawRectangle(int x, int y, int width, int height, Color color, int contourWidth)
    {
        Vector2 leftTop = new Vector2(x, y);
        Vector2 rightTop = new Vector2(x + width, y);
        Vector2 leftBottom = new Vector2(x, y + height);
        Vector2 rightBottom = new Vector2(x + width, y + height);
        DrawLine(leftTop, rightTop, color, contourWidth);
        DrawLine(rightTop, rightBottom, color, contourWidth);
        DrawLine(rightBottom, leftBottom, color, contourWidth);
        DrawLine(leftBottom, leftTop, color, contourWidth);
    }
    /// <summary>
    /// Renders rectangle filled with chosen color
    /// </summary>
    /// <param name="x">X coordinate for left top rectangle corner</param>
    /// <param name="y">Y coordinate for left top rectangle corner</param>
    /// <param name="width">Rectangle width</param>
    /// <param name="height">Rectangle height</param>
    /// <param name="color">Rectangle contour color</param>
    /// <remarks>
    /// Made by ChatGPT
    /// </remarks>
    public static void FillRectangle(int x, int y, int width, int height, Color color)
    {
        Texture2D pixel = new Texture2D(SpriteBatch.GraphicsDevice, width, height);
        var colorArray = new Color[width * height];
        for (int i = 0; i < colorArray.Length; i++)
        {
            colorArray[i] = Color.White;
        }
        pixel.SetData(colorArray);
        SpriteBatch.Draw(pixel, new Rectangle(x, y, width, height), color);
    }
    /// <summary>
    /// Renders circle
    /// </summary>
    /// <param name="center">Circle center coordinate</param>
    /// <param name="radius">Circle radius</param>
    /// <param name="color">Circle color</param>
    /// <remarks>
    /// Made by ChatGPT
    /// </remarks>
    public static void DrawCircle(Vector2 center, float radius, Color color)
    {
        float angle = 0f;
        int segments = 180;
        float angleIncrement = MathHelper.TwoPi / segments;

        Vector2 p1 = new Vector2(radius, 0) + center;
        Vector2 p2;

        for (int i = 0; i < segments; i++)
        {
            angle += angleIncrement;
            p2 = new Vector2(radius * (float)Math.Cos(angle), radius * (float)Math.Sin(angle)) + center;
            DrawLine(p1, p2, color);
            p1 = p2;
        }
    }
    /// <summary>
    /// Renders circle filled with chosen color
    /// </summary>
    /// <param name="center">Circle center coordinate</param>
    /// <param name="radius">Circle radius</param>
    /// <param name="color">Circle color</param>
    /// <remarks>
    /// Made by ChatGPT
    /// </remarks>
    public static void FillCircle(Vector2 center, int radius, Color color)
    {
        Texture2D circleTexture = new Texture2D(SpriteBatch.GraphicsDevice, radius * 2, radius * 2);

        Color[] data = new Color[circleTexture.Width * circleTexture.Height];

        float diameter = radius * 2f;
        float radiusSquared = radius * radius;

        for (int x = 0; x < circleTexture.Width; x++)
        {
            for (int y = 0; y < circleTexture.Height; y++)
            {
                float distanceSquared = (x - radius) * (x - radius) + (y - radius) * (y - radius);
                if (distanceSquared <= radiusSquared)
                {
                    data[x + y * circleTexture.Width] = color;
                }
                else
                {
                    data[x + y * circleTexture.Width] = Color.Transparent;
                }
            }
        }

        circleTexture.SetData(data);
        SpriteBatch.Draw(circleTexture, center, color);
    }
}
