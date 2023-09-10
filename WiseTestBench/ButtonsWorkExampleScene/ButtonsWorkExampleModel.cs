using Microsoft.Xna.Framework;
using WiseEngine;
using WiseEngine.MonogamePart;
using WiseEngine.MVP;
using WiseTestBench.BaseMovementScene;

namespace WiseTestBench.ButtonsWorkExampleScene;

public class ButtonsWorkExampleModel : Model
{
    private Witch _player;
    public override void Initialize()
    {
        _player = new Witch(new Vector2(
            Globals.Resolution.Width / 2,
            Globals.Resolution.Height / 2)
            );
        GameObjects.Add(_player);
        _inputData = new ButtonsWorkExampleViewModelData();
        _outputData = new ButtonsWorkExampleModelViewData();
    }

    public override void Update(ViewCycleFinishedEventArgs e)
    {     
        var outData = GetOutputData<ButtonsWorkExampleModelViewData>();
        outData.PlayerPos = _player.Pos;
        var inputData = GetInputData<ButtonsWorkExampleViewModelData>();
        _player.Speed += inputData.DeltaSpeedPlayer;

        

        base.Update(e);

        foreach (var obj in GameObjects)
        {
            var t = LoadableObjects.GetTexture(obj.Sprites[0].ImageName);
            obj.Pos = new Vector2(
                MathHelper.Clamp(obj.Pos.X, 0, Globals.Resolution.Width - t.Width * obj.Scale.X),
                MathHelper.Clamp(obj.Pos.Y, 0, Globals.Resolution.Height - t.Height * obj.Scale.Y)
                );
        }
    }
}
