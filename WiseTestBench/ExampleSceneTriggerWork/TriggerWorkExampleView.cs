using Microsoft.Xna.Framework.Input;
using WiseEngine.MonogamePart;
using WiseEngine;
using WiseEngine.MVP;
using WiseTestBench.BaseMovementScene;
using Microsoft.Xna.Framework;

namespace WiseTestBench.ExampleSceneTriggerWork;

public class TriggerWorkExampleView : View
{
    public override void Initialize()
    {
        _outputData = new TriggerWorkViewModelData();
        _inputData = new TriggerWorkModelViewData();
        

        Button BtnReturn = new Button(new Vector2(0, 0), LoadableObjects.GetFont("MainFont"), "Обратно");
        BtnReturn.ChangeSize(200, 50);
        BtnReturn.Clicked += BtnReturn_Click;

        _interfaceManager.AddElement(BtnReturn);


    }
    public override void Update()
    {
        var playerPos = GetInputData<TriggerWorkModelViewData>().PlayerPos;
        
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

        _interfaceManager.TransformCursor(InputsManager.MouseStateCurrentFrame.Position);

        if (InputsManager.IsSingleClicked(InputsManager.MouseButton.Left))
        {
            _interfaceManager.ClickCurrentElement();
        }

        data.DeltaSpeedPlayer = sV;

        base.Update();

    }

    public void BtnReturn_Click(object sender, ClickEventArgs e)
    {
        OnSceneFinished(new SceneFinishedEventArgs() { NewSceneName = "MainMenu" });
    }
}
