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
        _outputData = new BaseMovementViewModelData();
        mb = new MessageBox(new Vector2(150, 50), LoadableObjects.GetFont("MainFont"),
            "0");
        _interfaceManager.AddElement(mb);
        _outputData = new BaseMovementViewModelData(); 
        _inputData = new BaseMovementModelViewData();
    } 
    public override void Update()
    {
        //var m = GetInputData<BaseMovementModelViewData>();
        //mb.Text = _inputData != null ? GetInputData<BaseMovementModelViewData>().PlayerPos.ToString(): "0";
        mb.Text = GetInputData<BaseMovementModelViewData>().PlayerPos.ToString();

        Vector2 sV = Vector2.Zero;
        var data = (BaseMovementViewModelData)_outputData;
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
