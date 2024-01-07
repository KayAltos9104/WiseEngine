using WiseTestBench.ExampleSceneShapeProjectileWork;
using WiseEngine.MVP;
using Microsoft.Xna.Framework;
using System;
using WiseEngine.PhysicsAndCollisions;
using WiseEngine.Models;

namespace WiseTestBench.SimplePhysicsExampleScene;

// TODO: Глючит, если статические объекты двигать
public class PhysicsModel : Model
{
    private const float _shotCooldown = 400.0f;
    private const float _enemyBirthCooldown = 600.0f;
    

    private float _shotCooldownTime = 0;
    private float _enemyBirthCooldownTime = 0;
    //private SolidWitch _player;
    private AnimatedWitch _player;
    private CommonTrigger _borders;
    private bool _isLoosed = false;
    private bool _isWon = false;
    private bool _doGoblins = false;
    private int _score = 0;
    private bool _loseProcessed = false;
    private bool _doShot = false;
    public override void Initialize()
    {
        base.Initialize();
        //_player = new SolidWitch(new Vector2(
        //    Globals.Resolution.Width / 2,
        //    Globals.Resolution.Height / 2-100)
        //    );
        //_player = new SolidWitch(new Vector2(
        //    200,
        //    400)
        //    );
        _player = new AnimatedWitch(new Vector2(
            200,
            400)
            );
        GameObjects.Add(_player);
        var platform1 = new Platform(new Vector2(2600, 400));
        GameObjects.Add(platform1);

        var gem = new Gem(new Vector2(2690, 270));
        gem.Died += WonProcess;
        GameObjects.Add(gem);

        int tileSize = (platform1.GetCollider() as RectangleCollider).Area.Width;
        bool[,] map = 
        {
            {true, true, true, true },
            {true, false, false, true },
            {false, true, false, true },
            {false, true, false, false },
            {false, false, false, true },
            {false, false, true, true  },
            {false, true, false, false },
            {false, false, false, false },
            {false, false, false, true },
            {true, true, false, true },
            };

       
        for (int i = 0; i < map.GetLength(0); i++)
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (map[i,j])
                {
                    var platform = new Platform(new Vector2(i*tileSize, j*tileSize));
                    GameObjects.Add(platform);
                }                    
            }


        var goblin1 = new SolidGoblin(new Vector2(800, 400));
        var goblin2 = new SolidGoblin(new Vector2(400, 0));
        var goblin3 = new SolidGoblin(new Vector2(600, 0));
        var goblin4 = new SolidGoblin(new Vector2(1200, 0));
        var goblin5 = new SolidGoblin(new Vector2(1750, -200));

        goblin1.Died += ShotProccess;
        goblin2.Died += ShotProccess;
        goblin3.Died += ShotProccess;
        goblin4.Died += ShotProccess;
        goblin5.Died += ShotProccess;

        GameObjects.AddRange(new[] { goblin1, goblin2, goblin3, goblin4, goblin5 });
        _borders = new CommonTrigger(new Vector2(-1000, -400), 5400, 2700);
        _borders.Name = "Borders";
        _borders.TriggeredOutside += SwitchOutside;
        TriggerManager.AddTrigger(_borders);

        _outputData = new PhysicsModelViewData();
        _inputData = new PhysicsViewModelData();
        var outData = GetOutputData<PhysicsModelViewData>();
        outData.Player = _player;
        _player.Died += LooseProcess;
        _isLoosed = false;
        _isWon = false;
        _score = 0;

        if (_player is AnimatedWitch)
        {
            _player.Shooted += (sender, e) => 
            {
                _doShot = true;                
            };
        }
    }

    public override void Update(ViewCycleFinishedEventArgs e)
    {
        if (_isLoosed && _loseProcessed == true)
        {            
            return;
        }
        else if (_isLoosed && _loseProcessed == false)
        {
            _loseProcessed = true;
        }
           

        var inputData = GetInputData<PhysicsViewModelData>();
        _player.Speed += inputData.DeltaSpeedPlayer;
        if (inputData.DoJump && _player.IsOnPlatform)
        {
            _player.Force += Vector2.UnitY * (-45000);            
        }
        inputData.DoJump = false;



        if (_player is SolidWitch)
        {
            if (inputData.DoPlayerShoot && _shotCooldownTime > _shotCooldown)
            {
                Vector2 orbPos = new Vector2(
                    _player.Pos.X + (_player.GetCollider() as RectangleCollider).Area.Width / 2,
                    _player.Pos.Y + (_player.GetCollider() as RectangleCollider).Area.Height / 1.8f);
                var projectile = new OrbProjectile(orbPos, _player.IsLeft ? -Vector2.UnitX / 2 : Vector2.UnitX / 2);

                GameObjects.Add(projectile);
                _shotCooldownTime = 0;
            }
            else
            {                
                _shotCooldownTime += Globals.Time.ElapsedGameTime.Milliseconds;
            }
        }
        else if (_player is AnimatedWitch)
        {
            if (inputData.DoPlayerShoot)
                _player.DoShoot();
            if (_doShot)
            {
                Vector2 orbPos = new Vector2(
                _player.Pos.X + (_player.GetCollider() as RectangleCollider).Area.Width / 2,
                _player.Pos.Y + (_player.GetCollider() as RectangleCollider).Area.Height / 1.8f);
                var projectile = new OrbProjectile(orbPos, _player.IsLeft ? -Vector2.UnitX / 2 : Vector2.UnitX / 2);

                GameObjects.Add(projectile);
                _doShot = false;
            }
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
        if (e.ActivatedObject is SolidWitch || e.ActivatedObject is AnimatedWitch) 
        {
            _player.OnDied();
        }
    }

    private void LooseProcess(object sender, EventArgs e)
    {        
        _isLoosed = true;
        var outData = GetOutputData<PhysicsModelViewData>();
        outData.IsLoosed = _isLoosed;
    }

    private void WonProcess (object sender, EventArgs e)
    {
        _isWon = true;
        var outData = GetOutputData<PhysicsModelViewData>();
        outData.IsWon = _isWon;
    }
    private void ShotProccess(object sender, EventArgs e)
    {
        IncreaseScore(1);
    }

    private void IncreaseScore(byte value)
    {        
        _score += value;
    }
}
