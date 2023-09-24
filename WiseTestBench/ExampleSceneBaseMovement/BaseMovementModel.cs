using Microsoft.Xna.Framework;
using WiseEngine;
using WiseEngine.MonogamePart;
using WiseEngine.MVP;
using WiseTestBench.ExampleSceneTriggerWork;

namespace WiseTestBench.BaseMovementScene;

public class BaseMovementModel : Model
{
    private Witch _player;
    public override void Initialize()
    {
        base.Initialize();
        _player = new Witch(new Vector2(
            Globals.Resolution.Width / 2,
            Globals.Resolution.Height / 2)
            );
        GameObjects.Add(_player);

        
        _outputData = new BaseMovementModelViewData();
        _inputData = new BaseMovementViewModelData();
    }
    
    public override void Update(ViewCycleFinishedEventArgs e)
    {
        GameConsole.Clear();
        var outData = GetOutputData<BaseMovementModelViewData>();
        outData.PlayerPos = _player.Pos;
        var inputData = GetInputData<BaseMovementViewModelData>();
        _player.Speed += inputData.DeltaSpeedPlayer;
        GameConsole.WriteLine(_player.ToString());        
        

        base.Update(e);
        var t = LoadableObjects.GetTexture(_player.Sprites[0].TextureName);
        _player.Pos = new Vector2(
            MathHelper.Clamp(_player.Pos.X, 0, Globals.Resolution.Width - t.Width * _player.Scale.X),
            MathHelper.Clamp(_player.Pos.Y, 0, Globals.Resolution.Height - t.Height * _player.Scale.Y)
            );
    }
}
