using LittleWitch.Prefabs;
using Microsoft.Xna.Framework;
using WiseEngine.MVP;

namespace LittleWitch.Scenes;

public class LevelModelPrefab : Model
{
    private Sorceress _player;
    public override void Initialize()
    {
        base.Initialize();
        _player = new Sorceress();
        _player.Pos = new Vector2(Globals.Resolution.Width / 2, Globals.Resolution.Height / 2);
        //_player.Pos = new Vector2(27, 48);
        GameObjects.Add( _player );

        _inputData = new LevelViewModelData();
        _outputData  = new LevelModelViewData();
    }
    public override void Update(ViewCycleFinishedEventArgs e)
    {
        var inputData = GetInputData<LevelViewModelData>();
        _player.Run(inputData.PlayerDirection);
      

        var outData = GetOutputData<LevelModelViewData>();
        outData.Player = _player;
        base.Update(e);
    }
}
