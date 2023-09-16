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
    public Vector3 Pos { get; set; }
    public float Rotation { get; set; } 
    public Matrix Transform { get; set; }

    public Camera2D()
    {
        Pos = new Vector3 (0, 0, 1);
        Rotation = 0;
        Transform = Matrix.CreateTranslation(Pos);
    }
    public Matrix GetView ()
    {
        return Matrix.CreateTranslation(Pos);
    }

    public void Translate (Vector3 translation)
    {
        Pos += translation;
        
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

    public void Follow (object sender, CameraPositionEventArgs e)
    {
        //Translate(e.Position.X, e.Position.Y, 0);
        Pos = new Vector3(e.Position.X, e.Position.Y, Pos.Z);
    }
}
public class CameraPositionEventArgs
{
    public Vector2 Position { get; set; }
}
