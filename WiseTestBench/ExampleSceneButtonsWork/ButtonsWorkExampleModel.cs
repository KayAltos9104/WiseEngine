using Microsoft.Xna.Framework;
using System;
using WiseEngine.Models;
using WiseEngine.MonogamePart;
using WiseEngine.MVP;
using WiseTestBench.BaseMovementScene;

namespace WiseTestBench.ButtonsWorkExampleScene;

public class ButtonsWorkExampleModel : Model
{
    private Witch _player;
    private bool _doPlayerShot = false;

    private event EventHandler Shooted;
    public override void Initialize()
    {
        base.Initialize();
        _player = new Witch(new Vector2(
            Globals.Resolution.Width / 2,
            Globals.Resolution.Height / 2)
            );
        GameObjects.Add(_player);
        _inputData = new ButtonsWorkExampleViewModelData();
        _outputData = new ButtonsWorkExampleModelViewData();

        Shooted += Shoot;
    }

    public override void Update(ViewCycleFinishedEventArgs e)
    {     
        var outData = GetOutputData<ButtonsWorkExampleModelViewData>();
        outData.PlayerPos = _player.Pos;
        var inputData = GetInputData<ButtonsWorkExampleViewModelData>();
        _player.Speed += inputData.DeltaSpeedPlayer;
        _doPlayerShot = inputData.DoPlayerShoot;

        if (_doPlayerShot)
        {
            _doPlayerShot = false;
            Shooted?.Invoke(this, EventArgs.Empty);
        }

        base.Update(e);

        foreach (var obj in GameObjects)
        {            

            var t = LoadableObjects.GetTexture((obj as IRenderable).Sprites[0].TextureName);
            

            if (obj is RedOrb)
            {
                var rect = new Rectangle(0,0,Globals.Resolution.Width,Globals.Resolution.Height);
                if (rect.Contains(obj.Pos) == false)
                {
                    obj.IsDisposed = true;
                    GameConsole.WriteLine("Сфера удалена");
                }
            }
            else
            {
                obj.Pos = new Vector2(
                MathHelper.Clamp(obj.Pos.X, 0, Globals.Resolution.Width - t.Width * (obj as IRenderable).Sprites[0].Scale.X),
                MathHelper.Clamp(obj.Pos.Y, 0, Globals.Resolution.Height - t.Height * (obj as IRenderable).Sprites[0].Scale.Y)
                );
            }
        }
    }

    private void Shoot (object sender, EventArgs e)
    {
        RedOrb projectile = new RedOrb(Vector2.Zero);
        projectile.Pos = _player.Pos + new Vector2(
            LoadableObjects.GetTexture(projectile.Sprites[0].TextureName).Width * 0.45f, 
            50);
        projectile.Speed = Vector2.UnitX * 0.3f;
        GameObjects.Add(projectile);
        GameConsole.WriteLine("Создана сфера");
    }
}
