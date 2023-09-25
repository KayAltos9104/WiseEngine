using Microsoft.Xna.Framework;
using WiseEngine;
using WiseEngine.MonogamePart;
using WiseEngine.MVP;
using WiseTestBench.ExampleSceneTriggerWork;

namespace WiseTestBench.ExampleSceneShapeProjectileWork;

public class ProjectileWorkModel : Model
{
    private const float _shotCooldown = 500.0f;

    private float _shotCooldownTime = 0;
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

        var goblin1 = new Goblin(new Vector2(1300, 200));
        var goblin2 = new Goblin(new Vector2(1300, 400));
        var goblin3 = new Goblin(new Vector2(1300, 600));

        GameObjects.AddRange(new[] { goblin1, goblin2, goblin3 });

        _borders = new CommonTrigger(new Vector2(100, 100), 1400, 700);
        _borders.Name = "Borders";        
        _borders.TriggeredOutside += SwitchOutside;
        TriggerManager.AddTrigger(_borders);

        _outputData = new ProjectileWorkModelViewData();
        _inputData = new ProjectileWorkViewModelData();
        var outData = GetOutputData<ProjectileWorkModelViewData>();
        outData.Player = _player;
    }

    public override void Update(ViewCycleFinishedEventArgs e)
    {
        var inputData = GetInputData<ProjectileWorkViewModelData>();
        _player.Speed += inputData.DeltaSpeedPlayer;

        if (_player.Speed.X > 0)
            Graphics2D.ReflectAllSprites(_player.Sprites);
        else if ( _player.Speed.X < 0)
            Graphics2D.ReflectAllSprites(_player.Sprites, true);

        if (inputData.DoPlayerShoot && _shotCooldownTime > _shotCooldown)
        {
            Vector2 orbPos = new Vector2 (
                _player.Pos.X + _player.Sprites[0].GetTexture().Width * _player.Sprites[0].Scale.X / 2, 
                _player.Pos.Y + _player.Sprites[0].GetTexture().Height * _player.Sprites[0].Scale.Y / 1.8f);
            var projectile = new OrbProjectile(orbPos, _player.Sprites[0].IsReflectedOY ? -Vector2.UnitX/4 : Vector2.UnitX/4);
            GameObjects.Add(projectile);
            _shotCooldownTime = 0;
        }
        else
        {
            _shotCooldownTime += Globals.Time.ElapsedGameTime.Milliseconds;
        }

        //TODO: 
        // Вынести обновление данных в отдельный метод, потому что иначе цикл завершается до того, как данные 
        // обновятся
        
        base.Update(e);

        var borderArea = (_borders.GetCollider() as RectangleCollider).Area;
        var t = LoadableObjects.GetTexture(_player.Sprites[0].TextureName);
        _player.Pos = new Vector2(
             MathHelper.Clamp(_player.Pos.X,
             _borders.Pos.X, 
             _borders.Pos.X + borderArea.Width - t.Width * _player.Sprites[0].Scale.X),
             MathHelper.Clamp(_player.Pos.Y,
             _borders.Pos.Y, 
             _borders.Pos.Y + borderArea.Height - t.Height * _player.Sprites[0].Scale.Y)
             );
       
    }

    private void SwitchOutside(object sender, TriggerEventArgs e)
    {
        if (e.ActivatedObject is OrbProjectile)
        {
            e.ActivatedObject.IsDisposed = true;
        }        
    }
}
