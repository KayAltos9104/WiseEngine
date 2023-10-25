using Microsoft.Xna.Framework;
using WiseEngine.MonogamePart;
using WiseEngine.MVP;

namespace WiseEngine.Models;

public class AnimationSingleFrames
{
    private float _currentTime;
    private Sprite [] _frames;
    private bool _isCycled;
    private int _currentFrameIndex;    
    /// <summary>
    /// Animation name for searching
    /// </summary>
    /// <remarks>
    /// If you don't set it, generates name with guid
    /// </remarks>
    public string Name { get; set; }
    /// <summary>
    /// Is animation active or not in current moment
    /// </summary>
    public bool IsActive { get; private set; }
    /// <summary>
    /// Time interval for frames changing
    /// </summary>
    public float SwitchingTime { get; set; }
    /// <summary>
    /// Animation position in relative coordinates
    /// </summary>
    public Vector2 Pos { get; set; }

    public AnimationSingleFrames(Sprite[] frames, float switchTime, bool isCycled = true)
    {
        Name = Guid.NewGuid().ToString();
        Pos = Vector2.Zero;
        _frames = frames;        
        SwitchingTime = switchTime;
        _isCycled = isCycled;

        _currentFrameIndex = 0;
        _currentTime = 0;
        Activate();
    }
    /// <summary>
    /// Activate animation
    /// </summary>
    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void Update()
    {
        if (IsActive)
        {
            if (_currentTime >= SwitchingTime)
            {
                SwitchNextFrame();
                _currentTime = 0;
                GameConsole.WriteLine($"{GetCurrentFrame().TextureName}");
            }
            else
            {
                _currentTime += Globals.Time.ElapsedGameTime.Milliseconds;
            }
        }
    }
    public Sprite GetCurrentFrame()
    {
        //var frame = _frames[_currentFrameIndex];
        //return new Rectangle(frame.FramePos.X, frame.FramePos.Y, frame.FrameWidth, frame.FrameHeight);
        return _frames[_currentFrameIndex];
    }   
    private void SwitchNextFrame()
    {
        _currentFrameIndex++;
        if (_currentFrameIndex >= _frames.Length)
        {
            if (_isCycled)
            {
                _currentFrameIndex = 0;
            }
            else
            {
                IsActive = false;
            }
        }
    }
}
