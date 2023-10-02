using WiseEngine;
using WiseTestBench.ExampleSceneShapeProjectileWork;
using WiseEngine.MVP;
using Microsoft.Xna.Framework;
using WiseEngine.MonogamePart;
using System;

namespace WiseTestBench.SimplePhysicsExampleScene;

public class PhysicsModel : Model
{
    private const float _shotCooldown = 400.0f;
    private const float _enemyBirthCooldown = 600.0f;
    

    private float _shotCooldownTime = 0;
    private float _enemyBirthCooldownTime = 0;
    private SolidWitch _player;
    private CommonTrigger _borders;
    private bool _isLoosed = false;
    private bool _doGoblins = false;
    private int _score = 0;

    public override void Initialize()
    {
        base.Initialize();
        _player = new SolidWitch(new Vector2(
            Globals.Resolution.Width / 2,
            Globals.Resolution.Height / 2-100)
            );
        GameObjects.Add(_player);

        //var goblin1 = new Goblin(new Vector2(1200, 200), Vector2.Zero);
        //goblin1.Died += ShotProccess;
        //GameObjects.Add(goblin1);
        //var goblin2 = new Goblin(new Vector2(1200, 400), Vector2.Zero);
        //var goblin3 = new Goblin(new Vector2(1200, 600), Vector2.Zero);

        //GameObjects.AddRange(new[] { goblin1, goblin2, goblin3 });

        var platform1 = new Platform(new Vector2(100, 800));
        var platform2 = new Platform(new Vector2(platform1.Pos.X+(platform1.GetCollider() as RectangleCollider).Area.Width, 800));
        var platform3 = new Platform(new Vector2(platform2.Pos.X + (platform2.GetCollider() as RectangleCollider).Area.Width, 800));


        var platform4 = new LongPlatform(new Vector2(800, 800));
        var platform5 = new Platform(new Vector2(platform4.Pos.X, 800 - (platform4.GetCollider() as RectangleCollider).Area.Height));
        GameObjects.AddRange(new[] { platform1, platform2, platform3, platform4, platform5 });

        _borders = new CommonTrigger(new Vector2(-1000, 100), 5400, 2000);
        _borders.Name = "Borders";
        _borders.TriggeredOutside += SwitchOutside;
        TriggerManager.AddTrigger(_borders);

        _outputData = new PhysicsModelViewData();
        _inputData = new PhysicsViewModelData();
        var outData = GetOutputData<PhysicsModelViewData>();
        outData.Player = _player;
        _player.Died += LooseProcess;
        _isLoosed = false;
        _score = 0;
    }

    public override void Update(ViewCycleFinishedEventArgs e)
    {
        if (_isLoosed)
            return;

        var inputData = GetInputData<PhysicsViewModelData>();
        _player.Speed += inputData.DeltaSpeedPlayer;
        if (inputData.DoJump && _player.IsOnPlatform)
        {
            _player.Force += Vector2.UnitY * (-40000);            
        }
        inputData.DoJump = false;

        if (_player.Speed.X > 0)
            Graphics2D.ReflectAllSprites(_player.Sprites);
        else if (_player.Speed.X < 0)
            Graphics2D.ReflectAllSprites(_player.Sprites, true);

        if (inputData.DoPlayerShoot && _shotCooldownTime > _shotCooldown)
        {
            Vector2 orbPos = new Vector2(
                _player.Pos.X + _player.Sprites[0].GetTexture().Width * _player.Sprites[0].Scale.X / 2,
                _player.Pos.Y + _player.Sprites[0].GetTexture().Height * _player.Sprites[0].Scale.Y / 1.8f);
            var projectile = new OrbProjectile(orbPos, _player.Sprites[0].IsReflectedOY ? -Vector2.UnitX / 2 : Vector2.UnitX / 2);
            //var projectile = new OrbProjectile(new Vector2(1200,200), Vector2.Zero);
            GameObjects.Add(projectile);
            _shotCooldownTime = 0;
        }
        else
        {
            _shotCooldownTime += Globals.Time.ElapsedGameTime.Milliseconds;
        }

        var outData = GetOutputData<PhysicsModelViewData>();

        outData.Score = _score;
        var borderArea = (_borders.GetCollider() as RectangleCollider).Area;
        if (_doGoblins && _enemyBirthCooldownTime > _enemyBirthCooldown)
        {
            var pos = new Vector2(1300, Globals.Random.Next(borderArea.Y + 100, borderArea.Y + borderArea.Height - 100));
            var goblin = new Goblin(pos, new Vector2(-(float)(Globals.Random.NextDouble() * 0.4), 0));
            goblin.Died += ShotProccess;
            GameObjects.Add(goblin);
            _enemyBirthCooldownTime = 0;
        }
        else
        {
            _enemyBirthCooldownTime += Globals.Time.ElapsedGameTime.Milliseconds;
        }
        //TODO: 
        // Вынести обновление данных в отдельный метод, потому что иначе цикл завершается до того, как данные 
        // обновятся
        base.Update(e);
        //var t = LoadableObjects.GetTexture(_player.Sprites[0].TextureName);
        //_player.Pos = new Vector2(
        //     MathHelper.Clamp(_player.Pos.X,
        //     _borders.Pos.X,
        //     _borders.Pos.X + borderArea.Width - t.Width * _player.Sprites[0].Scale.X),
        //     MathHelper.Clamp(_player.Pos.Y,
        //     _borders.Pos.Y,
        //     _borders.Pos.Y + borderArea.Height - t.Height * _player.Sprites[0].Scale.Y)
        //     );

    }

    protected void SwitchOutside(object sender, TriggerEventArgs e)
    {
        if (e.ActivatedObject is OrbProjectile)
        {
            e.ActivatedObject.IsDisposed = true;
        }
        if (e.ActivatedObject is Goblin)
        {
            e.ActivatedObject.IsDisposed = true;
            _score--;
        }
        if (e.ActivatedObject is SolidWitch) 
        {
            _player.OnDied();
        }
    }

    private void LooseProcess(object sender, EventArgs e)
    {
        Graphics2D.ReflectAllSprites(_player.Sprites, true, "X");
        _isLoosed = true;
        var outData = GetOutputData<PhysicsModelViewData>();
        outData.IsLoosed = _isLoosed;
    }

    private void ShotProccess(object sender, EventArgs e)
    {
        IncreaseScore(1);
    }

    private void IncreaseScore(byte value)
    {
        //GameConsole.WriteLine($"Shot!");
        _score += value;
    }
}
