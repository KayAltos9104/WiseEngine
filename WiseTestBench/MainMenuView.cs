using Microsoft.Xna.Framework.Input;
using WiseEngine.MonogamePart;
using WiseEngine;
using WiseEngine.MVP;
using WiseEngine.UI;
using Microsoft.Xna.Framework;

namespace WiseTestBench
{
    public class MainMenuView : View
    {
        public int CursorPos { get; set; } = 0;

        public override void Initialize()
        {
            var basePos = new Vector2(Globals.Resolution.Width / 2, Globals.Resolution.Height / 2 - 200);
            MessageBox MbxTest = new MessageBox(
                basePos, 
                LoadableObjects.GetFont("MainFont"),                     
                "Тестовое меню"
                    );
            MbxTest.IsCentered = true;
            MbxTest.ChangeSize(300, 50);
            MbxTest.Center();

            Button BtnLaunchBaseMovementScene = new Button(
                basePos + Vector2.UnitY * 100,
                LoadableObjects.GetFont("MainFont"), 
                "Перейти к сцене базового движения"
                ); 
            BtnLaunchBaseMovementScene.OnClick += BtnTest1_Click;
            BtnLaunchBaseMovementScene.Name = "BtnLaunchBaseMovementScene";
            BtnLaunchBaseMovementScene.ChangeSize(600, 50);
            BtnLaunchBaseMovementScene.Center();

            Button BtnExit = new Button(
                basePos + Vector2.UnitY * 200, 
                LoadableObjects.GetFont("MainFont"), 
                "Выход"
                );
           

            BtnExit.OnClick += BtnExit_Click;
            BtnExit.Name = "BtnExit";
            BtnExit.ChangeSize(200, 50);
            BtnExit.Center();

            _interfaceManager.AddElement(MbxTest);
            _interfaceManager.AddElement(BtnLaunchBaseMovementScene);      
            _interfaceManager.AddElement(BtnExit);
        }

        public override void Update()
        {

            if (InputsManager.IsSinglePressed(Keys.W))
                ((IKeyboardCursor)_interfaceManager).MoveCursor(DiscreteDirection.Up);
            if (InputsManager.IsSinglePressed(Keys.S))
                ((IKeyboardCursor)_interfaceManager).MoveCursor(DiscreteDirection.Down);           
            if (InputsManager.IsSinglePressed(Keys.Space))
                (((IKeyboardCursor)_interfaceManager).GetCurrentElement() as Button).PerformClick();
            GameConsole.Clear();
            GameConsole.WriteLine($"Позиция курсора клавиатуры: {(_interfaceManager as IKeyboardCursor).CursorPos}");
            GameConsole.WriteLine($"Имя выбранного элемента: {(_interfaceManager as IKeyboardCursor).GetCurrentElement().Name}");

            if (InputsManager.MouseStateCurrentFrame.LeftButton == ButtonState.Pressed)
            {
                var chosenElement = _interfaceManager.GetComponent(InputsManager.MouseStateCurrentFrame.Position);
                if (chosenElement != null) 
                {
                    (chosenElement as Button).PerformClick();
                }
            }

            base.Update();
        }       
        private void BtnTest1_Click(object sender, ClickEventArgs e)
        {
            MessageBox MbxTest1 = new MessageBox(new Vector2(
                    250, 30),
                    LoadableObjects.GetFont("MainFont"),
                    "Нажата кнопка 1"
                    );
            MbxTest1.IsCentered = true;

            _interfaceManager.AddElement(MbxTest1);
            OnSceneFinished(new SceneFinishedEventArgs() { NewSceneName = "BaseMovement" });
        }

        private void BtnExit_Click(object sender, ClickEventArgs e)
        {
            OnGameFinished();
        }
    }
}
