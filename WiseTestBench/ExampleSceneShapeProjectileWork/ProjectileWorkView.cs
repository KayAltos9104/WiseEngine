using WiseEngine.MonogamePart;
using WiseEngine;
using WiseEngine.MVP;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace WiseTestBench.ExampleSceneShapeProjectileWork;

public class ProjectileWorkView : View
{
    public override void Initialize()
    {
        base.Initialize();
        _outputData = new ProjectileWorkViewModelData();
        _inputData = new ProjectileWorkModelViewData();

        Button BtnReturn = new Button(new Vector2(0, 0), LoadableObjects.GetFont("MainFont"), "Обратно");
        BtnReturn.ChangeSize(200, 50);
        BtnReturn.Clicked += BtnReturn_Click;

        Button BtnReload = new Button(new Vector2(0, 60), LoadableObjects.GetFont("MainFont"), "Заново");
        BtnReload.ChangeSize(200, 50);
        BtnReload.Clicked += BtnReload_Click;

        _interfaceManager.AddElement(BtnReload);
        _interfaceManager.AddElement(BtnReturn);
    }

    public override void Update()
    {

        base.Update();
        Vector2 sV = Vector2.Zero;
        var data = (ProjectileWorkViewModelData)_outputData;
        if (InputsManager.PressedCurrentFrame.IsKeyDown(Keys.W))
            sV -= Vector2.UnitY;
        if (InputsManager.PressedCurrentFrame.IsKeyDown(Keys.A))
            sV -= Vector2.UnitX;
        if (InputsManager.PressedCurrentFrame.IsKeyDown(Keys.S))
            sV += Vector2.UnitY;
        if (InputsManager.PressedCurrentFrame.IsKeyDown(Keys.D))
            sV += Vector2.UnitX;

        if (InputsManager.PressedCurrentFrame.IsKeyDown(Keys.Space))
            data.DoPlayerShoot = true;
        else 
            data.DoPlayerShoot = false;

        data.DeltaSpeedPlayer = sV;
        
        _interfaceManager.TransformCursor(InputsManager.MouseStateCurrentFrame.Position);

        if (InputsManager.IsSingleClicked(InputsManager.MouseButton.Left))
        {
            _interfaceManager.ClickCurrentElement();
        }
        var inputData = GetInputData<ProjectileWorkModelViewData>();
        

        Camera.Follow(inputData.Player != null ? inputData.Player.Pos: Vector2.Zero);
    }
    public void BtnReturn_Click(object sender, ClickEventArgs e)
    {
        OnSceneFinished(new SceneFinishedEventArgs() { NewSceneName = "MainMenu" });
    }

    public void BtnReload_Click(object sender, ClickEventArgs e)
    {
        OnSceneReloaded();
    }
}
