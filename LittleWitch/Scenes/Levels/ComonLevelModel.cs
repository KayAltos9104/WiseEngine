using LittleWitch.Prefabs;
using Microsoft.Xna.Framework;
using WiseEngine.MVP;

namespace LittleWitch.Scenes.Levels;
public class ComonLevelModel : LevelModelPrefab
{
    
    public override void Initialize()
    {
        base.Initialize();
        StaticPlatform platform1 = new StaticPlatform("Platform1", 5);
        StaticPlatform platform2 = new StaticPlatform("Platform1", 10);
        platform1.Pos = new Vector2(Globals.Resolution.Width / 2 - 50, Globals.Resolution.Height / 2 + 200);
        GameObjects.Add(platform1);
        platform2.Pos = new Vector2(Globals.Resolution.Width / 2 - 500, Globals.Resolution.Height / 2 + 1000);
        GameObjects.Add(platform2);
    }
    public override void Update(ViewCycleFinishedEventArgs e)
    {
        base.Update(e);
    }
}
