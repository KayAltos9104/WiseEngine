using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using WiseEngine;
using WiseEngine.MonogamePart;
using WiseEngine.MVP;
using WiseEngine.UI;

namespace WiseTestBench.ButtonsWorkExampleScene;

public class ButtonsWorkExampleView : View
{
    public override void Initialize()
    {
        base.Initialize();
        _inputData = new ButtonsWorkExampleModelViewData();
        _outputData = new ButtonsWorkExampleViewModelData();

        Button BtnUp = new Button(new Vector2(150, 100), LoadableObjects.GetFont("MainFont"), "Верх");
        BtnUp.Name = "BtnUp";
        BtnUp.ChangeSize(100, 100);

        Button BtnDown = new Button(new Vector2(150, 320), LoadableObjects.GetFont("MainFont"), "Низ");
        BtnDown.Name = "BtnDown";
        BtnDown.ChangeSize(100, 100);

        Button BtnRight = new Button(new Vector2(260, 210), LoadableObjects.GetFont("MainFont"), "Право");
        BtnRight.Name = "BtnRight";
        BtnRight.ChangeSize(100, 100);

        Button BtnLeft = new Button(new Vector2(40, 210), LoadableObjects.GetFont("MainFont"), "Лево");
        BtnLeft.Name = "BtnLeft";
        BtnLeft.ChangeSize(100, 100);

        Button BtnFire = new Button(new Vector2(150, 210), LoadableObjects.GetFont("MainFont"), "Огонь");
        BtnFire.Name = "BtnFire";
        BtnFire.ChangeSize(100, 100);

        Button BtnReturn = new Button(new Vector2(0, 0), LoadableObjects.GetFont("MainFont"), "Обратно");
        BtnReturn.ChangeSize(180, 50);
        BtnReturn.Clicked += BtnReturn_Click;


        string instructions = "Нажимайте кнопки для управления\nВ консоли выводятся сообщения\nо статусе снаряда";
        MessageBox MbxInstructions = new MessageBox(Vector2.UnitY * 450, LoadableObjects.GetFont("MainFont"), instructions);
        MbxInstructions.BackgroundColor = new Color(0, 120, 120, 120);
        MbxInstructions.ContourWidth = 2;
        MbxInstructions.ChangeSize(560, 150);
        MbxInstructions.IsCentered = false;
        MbxInstructions.MarginText = new Vector2(10, 10);

        BtnUp.Clicked += WitchBtn_Click;
        BtnDown.Clicked += WitchBtn_Click;
        BtnRight.Clicked += WitchBtn_Click;
        BtnLeft.Clicked += WitchBtn_Click;
        BtnFire.Clicked += BtnFire_Click;

        _interfaceManager.AddElement(BtnUp);
        _interfaceManager.AddElement(BtnDown);
        _interfaceManager.AddElement(BtnRight);
        _interfaceManager.AddElement(BtnLeft);
        _interfaceManager.AddElement(BtnFire);
        _interfaceManager.AddElement(BtnReturn);
        _interfaceManager.AddElement(MbxInstructions);
    }

    public override void Update()
    {
        //GameConsole.Clear();
        var data = (ButtonsWorkExampleViewModelData)_outputData;
        data.DeltaSpeedPlayer = Vector2.Zero;
        data.DoPlayerShoot = false;
        _interfaceManager.TransformCursor(InputsManager.MouseStateCurrentFrame.Position);
        //if (InputsManager.IsSingleClicked(InputsManager.MouseButton.Left))
        if (InputsManager.MouseStateCurrentFrame.LeftButton == ButtonState.Pressed&&
            _interfaceManager.GetCurrentElement() != null && _interfaceManager.GetCurrentElement().Name != "BtnFire")
        {
            _interfaceManager.ClickCurrentElement();  
        }

        if (InputsManager.IsSingleClicked(InputsManager.MouseButton.Left) &&
            _interfaceManager.GetCurrentElement() != null && _interfaceManager.GetCurrentElement().Name == "BtnFire")
        {
            _interfaceManager.ClickCurrentElement();
        }

        base.Update();
    }

    private void WitchBtn_Click (object sender, ClickEventArgs e)
    {
        var data = GetOutputData<ButtonsWorkExampleViewModelData>();
        Vector2 speed = Vector2.Zero;
        Button b = (Button)sender;
        switch (b.Name)
        {
            case "BtnUp":
                {
                    speed += -Vector2.UnitY;
                    break;
                }
            case "BtnLeft":
                {
                    speed += -Vector2.UnitX;
                    break;
                }
            case "BtnRight":
                {
                    speed += Vector2.UnitX;
                    break;
                }
            case "BtnDown":
                {
                    speed += Vector2.UnitY;
                    break;
                }            
        }
        GameConsole.WriteLine($"{b.Name} кликнута");
        data.DeltaSpeedPlayer = speed;
    }

    private void BtnFire_Click(object sender, ClickEventArgs e)
    {
        //GameConsole.Clear();
        //GameConsole.WriteLine("Пиф-паф!");
        var data = GetOutputData<ButtonsWorkExampleViewModelData>();
        data.DoPlayerShoot = true;
    }

    public void BtnReturn_Click(object sender, ClickEventArgs e)
    {
        OnSceneFinished(new SceneFinishedEventArgs() { NewSceneName = "MainMenu" });
    }
}
