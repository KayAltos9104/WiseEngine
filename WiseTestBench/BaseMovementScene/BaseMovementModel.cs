using Microsoft.Xna.Framework;
using WiseEngine;
using WiseEngine.MonogamePart;
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
        _outputData = new BaseMovementModelViewData();
    }
    
    public override void Update(ViewCycleFinishedEventArgs e)
    {
        GameConsole.Clear();
        var outData = (BaseMovementModelViewData)_outputData;
        outData.PlayerPos = _player.Pos;
        var data = (BaseMovementViewModelData)e.CurrentViewData;
        _player.Speed += data.DeltaSpeedPlayer;
        GameConsole.WriteLine($"Позиция игрока: {_player.Pos}");
        GameConsole.WriteLine($"Скорость игрока: {_player.Speed}");
        base.Update(e);
    }
}
