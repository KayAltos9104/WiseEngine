using Microsoft.Xna.Framework.Input;
using WiseEngine.MonogamePart;
using WiseEngine.MVP;
using System.Numerics;
using WiseEngine;

namespace WiseTestBench.BaseMovementScene;

public class BaseMovementView : View
{
    MessageBox mb;
    public override void Initialize()
    {
        _toModelData = new BaseMovementViewModelData();
        mb = new MessageBox(new Vector2(150, 50), LoadableObjects.GetFont("MainFont"),
            "0");
        _interfaceManager.AddElement(mb);
    } 
    public override void Update()
    {
        var m = (BaseMovementModelViewData)_currentModelData;
        mb.Text = _currentModelData != null ? m.PlayerPos.ToString(): "0";

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
