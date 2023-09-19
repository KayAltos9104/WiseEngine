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
        public override void Initialize()
        {          
            var basePos = new Vector2(Globals.Resolution.Width / 2, Globals.Resolution.Height / 2 - 400);
            MessageBox MbxTest = new MessageBox(
                basePos, 
                LoadableObjects.GetFont("MainFont"),                     
                "Тестовое меню"
                    );
            MbxTest.IsCentered = true;
            MbxTest.ChangeSize(300, 50);
            MbxTest.Center();
            string instructions = "WiseEngine 0.2.2\n\nДанная сцена - главное меню, \n" +
                "из которого можно попасть\nв другие сцены, показывающие\nработу движка WiseEngine. \n\n" +
                "Для вызова консоли\nнажмите ctrl+Q.";
            MessageBox MbxInstructions = new MessageBox(Vector2.Zero, LoadableObjects.GetFont("MainFont"), instructions);
            MbxInstructions.BackgroundColor = new Color(0, 120, 120, 120);
            MbxInstructions.ContourWidth = 2;
            MbxInstructions.ChangeSize(480, 350);
            MbxInstructions.IsCentered = false;
            MbxInstructions.MarginText = new Vector2(10, 10);

            Button BtnChangeResolution = new Button(Vector2.UnitY * 600, LoadableObjects.GetFont("MainFont"),"Поменять разрешение");
            BtnChangeResolution.Clicked += BtnChangeResolution_Click;
            BtnChangeResolution.ChangeSize(400, 50);


            Button BtnLaunchBaseMovementScene = new Button(
                basePos + Vector2.UnitY * 100,
                LoadableObjects.GetFont("MainFont"), 
                "Перейти к сцене базового движения"
                ); 
            BtnLaunchBaseMovementScene.Clicked += BtnTest1_Click;
            BtnLaunchBaseMovementScene.Name = "BtnLaunchBaseMovementScene";
            BtnLaunchBaseMovementScene.ChangeSize(600, 50);
            BtnLaunchBaseMovementScene.Center();

            Button BtnLaunchButtonsWorkExampleScene = new Button(
                basePos + Vector2.UnitY * 200,
                LoadableObjects.GetFont("MainFont"),
                "Перейти к сцене работы с кнопками"
                );
            BtnLaunchButtonsWorkExampleScene.Clicked += BtnLaunchButtonsWorkExampleScene_Click;
            BtnLaunchButtonsWorkExampleScene.Name = "BtnLaunchButtonsWorkExampleScene";
            BtnLaunchButtonsWorkExampleScene.ChangeSize(600, 50);
            BtnLaunchButtonsWorkExampleScene.Center();

            Button BtnLaunchTriggersWorkExampleScene = new Button(
                basePos + Vector2.UnitY * 300,
                LoadableObjects.GetFont("MainFont"),
                "Перейти к сцене работы с триггерами"
                );
            BtnLaunchTriggersWorkExampleScene.Clicked += BtnLaunchTriggerWorkExampleScene_Click;
            BtnLaunchTriggersWorkExampleScene.Name = "BtnLaunchButtonsWorkExampleScene";
            BtnLaunchTriggersWorkExampleScene.ChangeSize(600, 50);
            BtnLaunchTriggersWorkExampleScene.Center();

            

            Button BtnExit = new Button(
                basePos + Vector2.UnitY * 400, 
                LoadableObjects.GetFont("MainFont"), 
                "Выход"
                );
           

            BtnExit.Clicked += BtnExit_Click;
            BtnExit.Name = "BtnExit";
            BtnExit.ChangeSize(200, 50);
            BtnExit.Center();

            _interfaceManager.AddElement(MbxTest);
            _interfaceManager.AddElement(MbxInstructions);
            _interfaceManager.AddElement(BtnChangeResolution);
            _interfaceManager.AddElement(BtnLaunchBaseMovementScene);
            _interfaceManager.AddElement(BtnLaunchButtonsWorkExampleScene);
            _interfaceManager.AddElement(BtnLaunchTriggersWorkExampleScene);
            _interfaceManager.AddElement(BtnExit);
            
        }

        public override void Update()
        {
            bool isMouse = true;
            if (isMouse)
            {
                // Mouse example
                _interfaceManager.TransformCursor(InputsManager.MouseStateCurrentFrame.Position);
                if (InputsManager.IsSingleClicked(InputsManager.MouseButton.Left))
                {
                    _interfaceManager.ClickCurrentElement();
                }
            }
            else
            {
                // Keyboard example
                if (InputsManager.IsSinglePressed(Keys.W))
                    _interfaceManager.MoveCursor(InterfaceManager.CursorStep.Up);
                if (InputsManager.IsSinglePressed(Keys.S))
                    _interfaceManager.MoveCursor(InterfaceManager.CursorStep.Down);
                if (InputsManager.IsSinglePressed(Keys.Space))
                {
                    _interfaceManager.ClickCurrentElement();
                }
            }
            GameConsole.Clear();
            GameConsole.WriteLine($"Позиция курсора: {_interfaceManager.CursorPos}");
            var cursorChoosed = _interfaceManager.GetCurrentElement() != null 
                ? _interfaceManager.GetCurrentElement().Name 
                : "None";
            GameConsole.WriteLine($"Имя выбранного элемента: {cursorChoosed}");
            base.Update();
        }       
        private void BtnTest1_Click(object sender, ClickEventArgs e)
        {
            OnSceneFinished(new SceneFinishedEventArgs() { NewSceneName = "BaseMovement" });
        }

        private void BtnLaunchButtonsWorkExampleScene_Click(object sender, ClickEventArgs e)
        {
            OnSceneFinished(new SceneFinishedEventArgs() { NewSceneName = "BaseButtons" });
        }

        private void BtnLaunchTriggerWorkExampleScene_Click (object sender, ClickEventArgs e) 
        {
            OnSceneFinished(new SceneFinishedEventArgs() { NewSceneName = "Triggers" });
        }
        private void BtnExit_Click(object sender, ClickEventArgs e)
        {
            OnGameFinished();
        }
        private void BtnChangeResolution_Click(object sender, ClickEventArgs e)
        {
            OnSettingsChanged(new SettingsEventArgs((1200, 720), null));
        }
    }
}
