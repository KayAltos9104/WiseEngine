using WiseEngine.Models;

namespace WiseEngine.MVP;
/// <summary>
/// Class which contains data that model transfer to <see cref="View"/>
/// </summary>
public class ModelViewData : GameData
{
    /// <value>
    /// The <c>CurrentFrameObjects</c> property represents a list with all objects in scene
    /// </value>
    public List<IObject> CurrentFrameObjects { get; set; }
    public List<ITrigger> Triggers { get; set; }

    public ModelViewData()
    {
        CurrentFrameObjects = new List<IObject>();
        Triggers = new List<ITrigger>();
    }

    public ModelViewData(List<IObject> currentFrameObjects)
    {
        CurrentFrameObjects = currentFrameObjects;
        Triggers = new List<ITrigger>();
    }
}
