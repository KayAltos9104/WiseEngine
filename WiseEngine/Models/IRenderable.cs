namespace WiseEngine.Models;

public interface IRenderable
{
    /// <value>
    /// The <c>Sprites</c> property represents a list with all object sprites
    /// </value>
    List<Sprite> Sprites { get; set; }
    /// <value>
    /// The <c>Scenes</c> property represents a dictionary with all scenes used in game
    /// </value>
    float Layer { get; set; }
}
