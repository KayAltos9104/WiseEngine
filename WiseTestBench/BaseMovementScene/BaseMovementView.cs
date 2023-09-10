using Microsoft.Xna.Framework.Input;
using WiseEngine.MonogamePart;
using WiseEngine.MVP;
using WiseEngine;
using Microsoft.Xna.Framework;

namespace WiseTestBench.BaseMovementScene;

public class BaseMovementView : View
{
    MessageBox mb;
    public override void Initialize()
    {        
        _outputData = new BaseMovementViewModelData();
        mb = new MessageBox(new Vector2(150, 50), LoadableObjects.GetFont("MainFont"),
            "0");
        mb.ChangeSize(250, 50);
        //mb.Center();

        Button BtnReturn = new Button(new Vector2(150, 200), LoadableObjects.GetFont("MainFont"), "Вернуться в главное меню");
        BtnReturn.ChangeSize(400, 50);
        //BtnReturn.Center();
        BtnReturn.OnClick += BtnReturn_Click;

        _interfaceManager.AddElement(mb);
        _interfaceManager.AddElement(BtnReturn);

        _outputData = new BaseMovementViewModelData(); 
        _inputData = new BaseMovementModelViewData();
    } 
    public override void Update()
    {
        
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

        _interfaceManager.TransformCursor(InputsManager.MouseStateCurrentFrame.Position);

        if (InputsManager.IsSingleClicked(InputsManager.MouseButton.Left))
        {
            _interfaceManager.ClickCurrentElement();
        }

        data.DeltaSpeedPlayer = sV;
        base.Update();
    }   
    
    public void BtnReturn_Click (object sender, ClickEventArgs e)
    {
        OnSceneFinished(new SceneFinishedEventArgs() { NewSceneName = "MainMenu" });
    }
}
