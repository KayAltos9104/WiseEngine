using Microsoft.Xna.Framework;
using System;
using WiseEngine;
using WiseEngine.MonogamePart;
using WiseEngine.MVP;
using WiseTestBench.BaseMovementScene;

namespace WiseTestBench.ExampleSceneTriggerWork;

public class TriggerWorkExampleModel : Model
{
    private ShapeWitch _player;
    private TestTrigger _playerTrigger;
    public override void Initialize()
    {
        _player = new ShapeWitch(new Vector2(
            Globals.Resolution.Width / 2,
            Globals.Resolution.Height / 2)
            );
        GameObjects.Add(_player);
        TestTrigger testTrigger = new TestTrigger(new Vector2(100, 400), 300, 200);
        Triggers.Add(testTrigger);
        _playerTrigger = testTrigger;
        _playerTrigger.Name = "Test trigger 1";
        _playerTrigger.Triggered += ShowIntersectingMessage;

        _outputData = new TriggerWorkModelViewData();
        _inputData = new TriggerWorkViewModelData();
    }

    public override void Update(ViewCycleFinishedEventArgs e)
    {
        //GameConsole.Clear();
        var outData = GetOutputData<TriggerWorkModelViewData>();
        outData.PlayerPos = _player.Pos;
        var inputData = GetInputData<TriggerWorkViewModelData>();
        _player.Speed += inputData.DeltaSpeedPlayer;
        //GameConsole.WriteLine(_player.ToString());

        //if (_playerTrigger.Collider.IsIntersects(_player.GetCollider()))
        if (Collider.IsIntersects(_playerTrigger.GetCollider(), _player.GetCollider()))
        {
            _playerTrigger.OnTriggered(new TestTriggerEventArgs() {ObjIntersected = _player});
        }
        else
        {
            _player.Collider.Color = Color.Yellow;   
        }

        base.Update(e);

        var t = LoadableObjects.GetTexture(_player.Sprites[0].ImageName);
        _player.Pos = new Vector2(
            MathHelper.Clamp(_player.Pos.X, 0, Globals.Resolution.Width - t.Width * _player.Scale.X),
            MathHelper.Clamp(_player.Pos.Y, 0, Globals.Resolution.Height - t.Height * _player.Scale.Y)
            );


    }

    private void ShowIntersectingMessage(object sender, EventArgs e)
    {
        GameConsole.Clear();
        var args = (TestTriggerEventArgs)e;
        var p = (ShapeWitch)args.ObjIntersected;
        GameConsole.WriteLine($"Позиция игрока: {p.Pos}");
        GameConsole.WriteLine($"Триггер: {(sender as TestTrigger).Name}");
        p.Collider.Color = Color.Red;
    }
}
