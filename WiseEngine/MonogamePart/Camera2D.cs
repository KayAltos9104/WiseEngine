using Microsoft.Xna.Framework;

namespace WiseEngine.MonogamePart;

/// <summary>
/// Two-dimensional camera class
/// </summary>
/// <remarks>
/// Formula for translation has been got from Oyyou channel. Thank him very much!
/// </remarks>
public class Camera2D
{
    /// <summary>
    /// <see cref="Rectangle">Area in which objects will be drawing</see>
    /// </summary>
    /// <remarks>
    /// Used for optimization
    /// </remarks>
    public Rectangle VisionArea { get; set; }

    public Vector3 Pos { get; set; }
    public float Rotation { get; set; } 
    public Matrix Transform { get; set; }

    
    public Camera2D()
    {
        Pos = new Vector3 (0, 0, 1);
        Rotation = 0;
        Transform = Matrix.CreateTranslation(Pos);
        UpdateVisionArea((int)Pos.X, (int)Pos.Y);
    }
    public Matrix GetView ()
    {
        return Matrix.CreateTranslation(Pos);
    }

    public void Translate (Vector3 translation)
    {
        Pos += translation;
        UpdateVisionArea(-(int)Pos.X, -(int)Pos.Y);
        
    }

    public void Translate(float x, float y, float z)
    {
       Translate (new Vector3(x, y, z));
    }

    public void Update ()
    {
        Transform = Matrix.CreateTranslation(new Vector3(Pos.X, Pos.Y, 0)) * 
            Matrix.CreateScale(new Vector3(Pos.Z, Pos.Z, 0)) *
            Matrix.CreateRotationZ(Rotation);
    }

    public void Follow (Vector2 position)
    {
        Pos = new Vector3(VisionArea.Width/2 - position.X, VisionArea.Height / 2 - position.Y, Pos.Z);
        //Translate(position.X, position.Y, 0);
        Update();
    }
    public void Follow (object sender, CameraPositionEventArgs e)
    {
        //Translate(e.Position.X, e.Position.Y, 0);
        Pos = new Vector3(e.Position.X, e.Position.Y, Pos.Z);
        Update();
    }
    /// <summary>
    /// Changes <see cref="VisionArea"/>
    /// </summary>
    public void UpdateVisionArea()
    {
        UpdateVisionArea(0,0);        
    }
    /// <summary>
    /// Changes <see cref="VisionArea"/>
    /// </summary>
    /// <param name="x">X coordinate for left top rectangle corner</param>
    /// <param name="y">Y coordinate for left top rectangle corner</param>
    public void UpdateVisionArea(int x, int y)
    {
        VisionArea = new Rectangle(x-100, y-100,
            Globals.Resolution.Width + 100, Globals.Resolution.Height + 100);
    }
    /// <summary>
    /// Changes <see cref="VisionArea"/>
    /// </summary>
    /// <param name="x">X coordinate for left top rectangle corner</param>
    /// <param name="y">Y coordinate for left top rectangle corner</param>
    /// <param name="width">Width of vision area</param>
    /// <param name="height">Height of vision area</param>
    public void UpdateVisionArea(int x, int y, int width, int height)
    {
        VisionArea = new Rectangle(x, y, width, height);
    }
    /// <summary>
    /// Checks if coordinate is in current vision area
    /// </summary>
    /// <param name="pos">Coordinate for checking</param>
    /// <returns><c>True</c> if coordinate is inside the vision area</returns>
    public bool IsInVisionArea(Vector2 pos)
    {
        if (VisionArea.Contains(pos)) return true;
        else return false;
    }
}
public class CameraPositionEventArgs
{
    public Vector2 Position { get; set; }
}
