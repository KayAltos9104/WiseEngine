using Microsoft.Xna.Framework;
using WiseEngine;
using WiseEngine.MVP;

namespace WiseTestBench.BaseMovementScene;

public class BaseMovementModel : Model
{
    public override void Initialize()
    {
        var Player = new Witch(new Vector2(
            Globals.Resolution.Width / 2,
            Globals.Resolution.Height / 2)
            );
        GameObjects.Add(Player);
    }
}
