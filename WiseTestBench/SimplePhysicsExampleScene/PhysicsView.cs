

using Microsoft.Xna.Framework.Input;
using WiseEngine.MonogamePart;
using WiseEngine;
using WiseEngine.MVP;
using WiseTestBench.ExampleSceneShapeProjectileWork;
using Microsoft.Xna.Framework;

namespace WiseTestBench.SimplePhysicsExampleScene;

public class PhysicsView : View
{
    private bool _gameLoosed;
    private int _score;
    private MessageBox _mbxScore;
    public override void Initialize()
    {
        base.Initialize();
        _outputData = new PhysicsViewModelData();
        _inputData = new PhysicsModelViewData();
        _gameLoosed = false;
        _score = 0;
        Button BtnReturn = new Button(new Vector2(0, 0), LoadableObjects.GetFont("MainFont"), "Обратно");
        BtnReturn.ChangeSize(200, 50);
        BtnReturn.Clicked += BtnReturn_Click;

        Button BtnReload = new Button(new Vector2(0, 60), LoadableObjects.GetFont("MainFont"), "Заново");
        BtnReload.ChangeSize(200, 50);
        BtnReload.Clicked += BtnReload_Click;

        //_mbxScore = new MessageBox(new Vector2(Globals.Resolution.Width / 2, 70),
        //    LoadableObjects.GetFont("MainFont"), $"Очки: {_score}");
        //_mbxScore.BackgroundColor = new Color(0, 255, 0);
        //_mbxScore.ContourWidth = 2;
        //_mbxScore.ChangeSize(200, 70);
        //_mbxScore.IsCentered = true;
        //_mbxScore.Center();
        //_mbxScore.MarginText = new Vector2(0, 0);

        //_interfaceManager.AddElement(_mbxScore);

        _interfaceManager.AddElement(BtnReload);
        _interfaceManager.AddElement(BtnReturn);
    }

    public override void Update()
    {

        base.Update();
        Vector2 sV = Vector2.Zero;
        var data = (PhysicsViewModelData)_outputData;
        if (InputsManager.PressedCurrentFrame.IsKeyDown(Keys.W))
            sV -= Vector2.UnitY;
        if (InputsManager.PressedCurrentFrame.IsKeyDown(Keys.A))
            sV -= Vector2.UnitX;
        if (InputsManager.PressedCurrentFrame.IsKeyDown(Keys.S))
            sV += Vector2.UnitY;
        if (InputsManager.PressedCurrentFrame.IsKeyDown(Keys.D))
            sV += Vector2.UnitX;

        if (InputsManager.PressedCurrentFrame.IsKeyDown(Keys.LeftShift))
            data.DoPlayerShoot = true;
        else
            data.DoPlayerShoot = false;

        if (InputsManager.PressedCurrentFrame.IsKeyDown(Keys.Space))
            data.DoJump = true;
        else
            data.DoJump = false;

        data.DeltaSpeedPlayer = sV;

        _interfaceManager.TransformCursor(InputsManager.MouseStateCurrentFrame.Position);

        if (InputsManager.IsSingleClicked(InputsManager.MouseButton.Left))
        {
            _interfaceManager.ClickCurrentElement();
        }
        var inputData = GetInputData<PhysicsModelViewData>();
        _score = inputData.Score;
        //_mbxScore.Text = $"Очки: {_score}";
        //if (_score >= 0)
        //    _mbxScore.BackgroundColor = new Color(0, 255, 0);
        //else
        //    _mbxScore.BackgroundColor = Color.Red;

        if (_gameLoosed == false && inputData.IsLoosed == true)
        {
            MessageBox MbxLoose = new MessageBox(new Vector2(Globals.Resolution.Width / 2, Globals.Resolution.Height / 2 - 100),
            LoadableObjects.GetFont("MainFont"), "Вы проиграли!");
            MbxLoose.BackgroundColor = new Color(255, 0, 0, 120);
            MbxLoose.ContourWidth = 2;
            MbxLoose.ChangeSize(400, 70);
            MbxLoose.IsCentered = true;
            MbxLoose.Center();
            MbxLoose.MarginText = new Vector2(0, 0);
            _interfaceManager.AddElement(MbxLoose);
            _gameLoosed = true;
        }

        Camera.Follow(inputData.Player != null ? inputData.Player.Pos : Vector2.Zero);
    }
    public void BtnReturn_Click(object sender, ClickEventArgs e)
    {
        OnSceneFinished(new SceneFinishedEventArgs() { NewSceneName = "MainMenu" });
    }

    public void BtnReload_Click(object sender, ClickEventArgs e)
    {
        OnSceneReloaded();
        _gameLoosed = false;
    }
}
