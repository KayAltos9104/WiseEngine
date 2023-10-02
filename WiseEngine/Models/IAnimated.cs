
namespace WiseEngine.Models;

public interface IAnimated
{
    Dictionary<string, Animation> Animations { get; }
    Animation CurrentAnimation { get; }
    float Layer { get; set; }
    void SetAnimation(string animationName);
}
