using Microsoft.Xna.Framework;
using WiseEngine;
using WiseEngine.MVP;

namespace WiseTestBench.BaseMovementScene;

public class BaseMovementModel : Model
{
    private Witch _player;
    public override void Initialize()
    {
        _player = new Witch(new Vector2(
            Globals.Resolution.Width / 2,
            Globals.Resolution.Height / 2)
            );
        GameObjects.Add(_player);
        ToViewData = new BaseMovementModelViewData();
    }
    
    public override void Update(ViewCycleFinishedEventArgs e)
    {
        var outData = (BaseMovementModelViewData)ToViewData;
        outData.PlayerPos = _player.Pos;
        var data = (BaseMovementViewModelData)e.CurrentViewData;
        _player.Speed += data.DeltaSpeedPlayer;

        base.Update(e);
    }
}
