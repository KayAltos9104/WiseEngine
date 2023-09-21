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
        base.Initialize();
        _outputData = new BaseMovementViewModelData();
        _inputData = new BaseMovementModelViewData();
        mb = new MessageBox(new Vector2(0, 100), LoadableObjects.GetFont("MainFont"),
            "0");
        mb.ChangeSize(250, 50);
        //mb.Center();

        string instructions = "Нажимайте W, A, S, D для управления";
        MessageBox MbxInstructions = new MessageBox(Vector2.UnitY * 450, LoadableObjects.GetFont("MainFont"), instructions);
        MbxInstructions.BackgroundColor = new Color(0, 120, 120, 120);
        MbxInstructions.ContourWidth = 2;
        MbxInstructions.ChangeSize(600, 70);
        MbxInstructions.IsCentered = false;
        MbxInstructions.MarginText = new Vector2(10, 10);

        Button BtnReturn = new Button(new Vector2(0, 0), LoadableObjects.GetFont("MainFont"), "Обратно");
        BtnReturn.ChangeSize(400, 50);
        //BtnReturn.Center();
        BtnReturn.Clicked += BtnReturn_Click;

        _interfaceManager.AddElement(mb);
        _interfaceManager.AddElement(BtnReturn);
        _interfaceManager.AddElement(MbxInstructions);

      
    } 
    public override void Update()
    {
        var playerPos = GetInputData<BaseMovementModelViewData>().PlayerPos;
        mb.Text = playerPos.ToString();
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
