using Microsoft.Xna.Framework.Input;
using WiseEngine.MonogamePart;
using WiseEngine.MVP;
using System.Numerics;

namespace WiseTestBench.BaseMovementScene;

public class BaseMovementView : View
{
    public override void Initialize()
    {
        _toModelData = new BaseMovementViewModelData();
    } 
    public override void Update()
    {
        Vector2 sV = Vector2.Zero;
        var data = (BaseMovementViewModelData)_toModelData;
        if (InputsManager.PressedCurrentFrame.IsKeyDown(Keys.W))
            sV -= Vector2.UnitY;
        if (InputsManager.PressedCurrentFrame.IsKeyDown(Keys.A))
            sV -= Vector2.UnitX;
        if (InputsManager.PressedCurrentFrame.IsKeyDown(Keys.S))
            sV += Vector2.UnitY;
        if (InputsManager.PressedCurrentFrame.IsKeyDown(Keys.D))
            sV += Vector2.UnitX;
        data.DeltaSpeedPlayer = sV;
        base.Update();
    }
}
