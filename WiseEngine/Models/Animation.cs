using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WiseEngine.MVP;

namespace WiseEngine.Models;

public class Animation
{
    private float _currentTime;
    private AnimationFrame [] _frames;
    private bool _isCycled;
    private int _currentFrameIndex;
    private Sprite _sprite;
    public string Name { get; set; }
    public bool IsActive { get; private set; }
    public float SwitchingTime { get; set; }

    public Vector2 Pos { get; set; }

    public Animation(AnimationFrame[] frames, Sprite sprite, float switchTime, bool isCycled = true)
    {
        Name = Guid.NewGuid().ToString();
        Pos = Vector2.Zero;
        _frames = frames;
        _sprite = sprite;
        SwitchingTime = switchTime;
        _isCycled = isCycled;
        
        _currentFrameIndex = 0;
        _currentTime = 0;
        Activate();
    }

    public void Activate()
    {
        IsActive = true;        
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void Update ()
    {
        if (IsActive)
        {
            if (_currentTime >= SwitchingTime) 
            {
                SwitchNextFrame();
                _currentTime = 0;
            }
            else
            {
                _currentTime += Globals.Time.ElapsedGameTime.Milliseconds;
            }
        }       
    }

    public Rectangle GetCurrentFrame()
    {
        var frame = _frames[_currentFrameIndex];
        return new Rectangle(frame.FramePos.X, frame.FramePos.Y, frame.FrameWidth, frame.FrameHeight);
    }

    public Sprite GetSprite ()
    {
        return _sprite;
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
