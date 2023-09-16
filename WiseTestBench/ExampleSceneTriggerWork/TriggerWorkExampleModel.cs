using Microsoft.Xna.Framework;
using System;
using WiseEngine;
using WiseEngine.MonogamePart;
using WiseEngine.MVP;

namespace WiseTestBench.ExampleSceneTriggerWork;

public class TriggerWorkExampleModel : Model
{
    private ShapeWitch _player;
    private CommonTrigger _playerTrigger1;
    private CommonTrigger _playerTrigger2;
    public override void Initialize()
    {
        _player = new ShapeWitch(new Vector2(
            Globals.Resolution.Width / 2,
            Globals.Resolution.Height / 2)
            );
        GameObjects.Add(_player);
        CommonTrigger testTrigger = new CommonTrigger(new Vector2(100, 300), 300, 200);
        TriggerManager.AddTrigger(testTrigger);
        _playerTrigger1 = testTrigger;
        _playerTrigger1.Name = "TestTrigger1";
        _playerTrigger1.Triggered += ShowIntersectingMessage;

        testTrigger = new CommonTrigger(new Vector2(1000, 400), 300, 200);
        TriggerManager.AddTrigger(testTrigger);
        _playerTrigger2 = testTrigger;
        _playerTrigger2.Name = "TestTrigger2";
        _playerTrigger2.Triggered += ShowIntersectingMessage;



        _outputData = new TriggerWorkModelViewData();
        _inputData = new TriggerWorkViewModelData();
    }

    public override void Update(ViewCycleFinishedEventArgs e)
    {
        var outData = GetOutputData<TriggerWorkModelViewData>();
        outData.PlayerPos = _player.Pos;
        var inputData = GetInputData<TriggerWorkViewModelData>();
        _player.Speed += inputData.DeltaSpeedPlayer;        

        base.Update(e);

        var t = LoadableObjects.GetTexture(_player.Sprites[0].ImageName);
        _player.Pos = new Vector2(
            MathHelper.Clamp(_player.Pos.X, 0, Globals.Resolution.Width - t.Width * _player.Scale.X),
            MathHelper.Clamp(_player.Pos.Y, 0, Globals.Resolution.Height - t.Height * _player.Scale.Y)
            );
    }

    private void ShowIntersectingMessage(object sender, TriggerEventArgs e)
    {
        GameConsole.Clear();
        
        var p = (ShapeWitch)e.ActivatedObject;
        GameConsole.WriteLine($"Позиция игрока: {p.Pos}");
        GameConsole.WriteLine($"Триггер: {e.ActivatedTrigger.Name}");
        //p.Collider.Color = Color.Red;
    }
}
