namespace WiseEngine.Models;

public interface IAnimatedSingleFrames
{
    Dictionary<string, AnimationSingleFrames> Animations { get; }
    AnimationSingleFrames CurrentAnimation { get; }
    float Layer { get; set; }
    void SetAnimation(string animationName);
}
