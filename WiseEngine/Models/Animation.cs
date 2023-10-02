using Microsoft.Xna.Framework.Graphics;
using System.Drawing;
using WiseEngine.MVP;

namespace WiseEngine.Models;

public class Animation
{
    private float _currentTime;

    private AnimationFrame [] _frames;

    private bool _isActive;
    private bool _isCycled;
    private int _currentFrameIndex;

    private Sprite _sprite;
    public string Name { get; set; }
    public float SwitchingTime { get; set; }

    public Animation(AnimationFrame[] frames, Sprite sprite, float switchTime, bool isCycled = true)
    {
        Name = Guid.NewGuid().ToString();
        _frames = frames;
        _sprite = sprite;
        SwitchingTime = switchTime;
        _isCycled = isCycled;
        _isActive = false;
        _currentFrameIndex = 0;
        _currentTime = 0;
    }

    public void Activate()
    {
        _isActive = true;
        _currentTime = 0;
        _currentFrameIndex = 0;
    }

    public void Update ()
    {
        if (_isActive)
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

    public Texture2D GetTexture ()
    {
        return _sprite.GetTexture();
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
                _isActive = false;
            }            
        }
    }
    
}
