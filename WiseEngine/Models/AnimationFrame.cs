using Microsoft.Xna.Framework;

namespace WiseEngine.Models;

public class AnimationFrame
{
    public int FrameWidth { get; }
    public int FrameHeight { get; }
    public Point FramePos { get; }

    public AnimationFrame (int frameWidth, int frameHeight, Point framePos)
    {
        FrameWidth = frameWidth;
        FrameHeight = frameHeight;
        FramePos = framePos;
    }
}
