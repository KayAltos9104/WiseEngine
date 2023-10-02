
namespace WiseEngine.Models;

public interface IAnimated
{
    Dictionary<string, Animation> Animations { get; }
    Animation CurrentAnimation { get; }

    void SetAnimation(string animationName);
}
