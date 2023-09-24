using Microsoft.Xna.Framework;
using System.Collections.Generic;
using WiseEngine;
using WiseEngine.MonogamePart;
using WiseEngine.MVP;
using WiseTestBench.ExampleSceneTriggerWork;

namespace WiseTestBench.ExampleSceneShapeProjectileWork;

public class ProjectileWorkModel : Model
{
    private LittleShapeWitch _player;
    private CommonTrigger _borders;
    public override void Initialize()
    {       
        base.Initialize();
        _player = new LittleShapeWitch(new Vector2(
            Globals.Resolution.Width / 2,
            Globals.Resolution.Height / 2)
            );
        GameObjects.Add(_player);

        _borders = new CommonTrigger(new Vector2(100, 100), 1400, 700);
        _borders.Name = "Borders";        
        _borders.TriggeredOutside += SwitchOutside;
        TriggerManager.AddTrigger(_borders);

        _outputData = new ProjectileWorkModelViewData();
        _inputData = new ProjectileWorkViewModelData();
    }

    public override void Update(ViewCycleFinishedEventArgs e)
    {
        var inputData = GetInputData<ProjectileWorkViewModelData>();
        _player.Speed += inputData.DeltaSpeedPlayer;
        //List<IObject> disposableObjects = new List<IObject>();
        //_inputData = e.CurrentViewData;
        //foreach (var obj in GameObjects)
        //{
        //    if (obj.IsDisposed)
        //    {
        //        disposableObjects.Add(obj);
        //    }
        //    else
        //    {
        //        obj.Update();
        //    }
        //    if (obj is IShaped)
        //    {
        //        TriggerManager.Update(obj as IShaped);
        //    }
        //}
        //GameObjects.RemoveAll(o => disposableObjects.Contains(o));

        //TODO: 
        // Вынести обновление данных в отдельный метод, потому что иначе цикл завершается до того, как данные 
        // обновятся

        _outputData.CurrentFrameObjects = new List<IObject>(GameObjects);
        _outputData.Triggers = new List<ITrigger>(TriggerManager.GetTriggers());
        
        var outData = GetOutputData<ProjectileWorkModelViewData>();
        outData.Player = _player;
        base.Update(e);
        //OnCycleFinished?.Invoke(this, new ModelCycleFinishedEventArgs() { ModelViewData = _outputData });

        var t = LoadableObjects.GetTexture(_player.Sprites[0].TextureName);
        _player.Pos = new Vector2(
             MathHelper.Clamp(_player.Pos.X,
             _borders.Pos.X, 
             _borders.Pos.X + (_borders.GetCollider() as RectangleCollider).Area.Width - t.Width * _player.Sprites[0].Scale.X),
             MathHelper.Clamp(_player.Pos.Y,
             _borders.Pos.Y, 
             _borders.Pos.Y + (_borders.GetCollider() as RectangleCollider).Area.Height - t.Height * _player.Sprites[0].Scale.Y)
             );
       
    }

    private void SwitchOutside(object sender, TriggerEventArgs e)
    {
        //var t = LoadableObjects.GetTexture(_player.Sprites[0].TextureName);
        //_player.Pos = new Vector2(
        //     MathHelper.Clamp(_player.Pos.X,
        //     _borders.Pos.X, _borders.Pos.X + (_borders.GetCollider() as RectangleCollider).Area.Width - t.Width * _player.Sprites[0].Scale.X),
        //     MathHelper.Clamp(_player.Pos.Y,
        //     _borders.Pos.Y, _borders.Pos.Y + (_borders.GetCollider() as RectangleCollider).Area.Height - t.Height * _player.Sprites[0].Scale.Y)
        //     );
    }
}
