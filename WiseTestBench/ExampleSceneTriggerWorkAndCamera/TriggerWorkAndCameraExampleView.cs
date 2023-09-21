using Microsoft.Xna.Framework.Input;
using WiseEngine.MonogamePart;
using WiseEngine;
using WiseEngine.MVP;
using WiseTestBench.BaseMovementScene;
using Microsoft.Xna.Framework;
using System;

namespace WiseTestBench.ExampleSceneTriggerWork;

public class TriggerWorkAndCameraExampleView : View
{    
    public event EventHandler<CameraPositionEventArgs> CameraPositionChanged;
    public override void Initialize()
    {
        base.Initialize();
        _outputData = new TriggerWorkViewModelData();
        _inputData = new TriggerWorkModelViewData();

        
        
        Button BtnReturn = new Button(new Vector2(0, 0), LoadableObjects.GetFont("MainFont"), "Обратно");
        BtnReturn.ChangeSize(200, 50);
        BtnReturn.Clicked += BtnReturn_Click;

        Button BtnReload = new Button(new Vector2(0, 60), LoadableObjects.GetFont("MainFont"), "Заново");
        BtnReload.ChangeSize(200, 50);
        BtnReload.Clicked += BtnReload_Click;

        string instructions = "Нажимайте W, A, S, D для управления\n" +
            "PgUp, PgDown - масштабирование\n" +
            "Стрелки - позиция камеры\n"+
            "V - камеру в исходное положение\n"+
            "R, T - поворот камеры";
            
        MessageBox MbxInstructions = new MessageBox(Vector2.UnitY * 450, LoadableObjects.GetFont("MainFont"), instructions);
        MbxInstructions.BackgroundColor = new Color(0, 120, 120, 120);
        MbxInstructions.ContourWidth = 2;
        MbxInstructions.ChangeSize(600, 200);
        MbxInstructions.IsCentered = false;
        MbxInstructions.MarginText = new Vector2(10, 10);

        _interfaceManager.AddElement(BtnReturn);
        _interfaceManager.AddElement(MbxInstructions);
        _interfaceManager.AddElement(BtnReload);

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

        if (InputsManager.PressedCurrentFrame.IsKeyDown(Keys.Right))
        {
            Camera.Translate(-10, 0, 0);
            GameConsole.WriteLine($"{Camera.Transform.Translation}");
        }
        else if (InputsManager.PressedCurrentFrame.IsKeyDown(Keys.Left))
        {
            Camera.Translate(10, 0, 0);
            GameConsole.WriteLine($"{Camera.Transform.Translation}");
        }

        if (InputsManager.PressedCurrentFrame.IsKeyDown(Keys.Up))
        {
            Camera.Translate(0, 10, 0);
            GameConsole.WriteLine($"{Camera.Transform.Translation}");
        }
        else if (InputsManager.PressedCurrentFrame.IsKeyDown(Keys.Down))
        {
            Camera.Translate(0, -10, 0);
            GameConsole.WriteLine($"{Camera.Transform.Translation}");
        }

        if (InputsManager.PressedCurrentFrame.IsKeyDown(Keys.PageUp))
        {
            Camera.Translate(0, 0, 0.01f);
            GameConsole.WriteLine($"{Camera.Transform.Translation}");
        }
        else if (InputsManager.PressedCurrentFrame.IsKeyDown(Keys.PageDown))
        {
            Camera.Translate(0, 0, -0.01f);
            GameConsole.WriteLine($"{Camera.Transform.Translation}");
        }

        if (InputsManager.PressedCurrentFrame.IsKeyDown(Keys.R))
        {
            Camera.Rotation += 0.01f;
        }
        else if (InputsManager.PressedCurrentFrame.IsKeyDown(Keys.T))
        {
            Camera.Rotation -= 0.01f;
        }

        if (InputsManager.PressedCurrentFrame.IsKeyDown(Keys.V))
        {
            Camera = new Camera2D();
        }

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

    public void BtnReload_Click (object sender, ClickEventArgs e)
    {
        OnSceneReloaded();
    }
}
