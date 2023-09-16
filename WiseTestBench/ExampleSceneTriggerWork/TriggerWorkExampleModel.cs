using Microsoft.Xna.Framework;
using WiseEngine;
using WiseEngine.MonogamePart;
using WiseEngine.MVP;
using WiseTestBench.BaseMovementScene;

namespace WiseTestBench.ExampleSceneTriggerWork;

public class TriggerWorkExampleModel : Model
{
    private Witch _player;
    public override void Initialize()
    {
        _player = new Witch(new Vector2(
            Globals.Resolution.Width / 2,
            Globals.Resolution.Height / 2)
            );
        GameObjects.Add(_player);
        TestTrigger testTrigger = new TestTrigger(new Vector2(100, 400), 300, 200);
        Triggers.Add(testTrigger);

        _outputData = new TriggerWorkModelViewData();
        _inputData = new TriggerWorkViewModelData();
    }

    public override void Update(ViewCycleFinishedEventArgs e)
    {
        GameConsole.Clear();
        var outData = GetOutputData<TriggerWorkModelViewData>();
        outData.PlayerPos = _player.Pos;
        var inputData = GetInputData<TriggerWorkViewModelData>();
        _player.Speed += inputData.DeltaSpeedPlayer;
        GameConsole.WriteLine(_player.ToString());


        base.Update(e);
        var t = LoadableObjects.GetTexture(_player.Sprites[0].ImageName);
        _player.Pos = new Vector2(
            MathHelper.Clamp(_player.Pos.X, 0, Globals.Resolution.Width - t.Width * _player.Scale.X),
            MathHelper.Clamp(_player.Pos.Y, 0, Globals.Resolution.Height - t.Height * _player.Scale.Y)
            );
    }
}
