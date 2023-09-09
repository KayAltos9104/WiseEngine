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
                _interfaceManager.TransformCursor(new Point(_interfaceManager.CursorPosX-1, 0));                
            if (InputsManager.IsSinglePressed(Keys.S))
                _interfaceManager.TransformCursor(new Point(_interfaceManager.CursorPosX+1, 0));
            if (InputsManager.IsSinglePressed(Keys.Space))
                (_interfaceManager.GetCurrentElement() as Button).PerformClick();
            GameConsole.Clear();
            GameConsole.WriteLine($"Позиция курсора клавиатуры: {(_interfaceManager as ICursor).CursorPos}");
            GameConsole.WriteLine($"Имя выбранного элемента: {_interfaceManager.GetCurrentElement().Name}");

            //if (InputsManager.MouseStateCurrentFrame.LeftButton == ButtonState.Pressed)
            //{
            //    var chosenElement = _interfaceManager.GetCurrentElement();
            //    if (chosenElement != null) 
            //    {
            //        (chosenElement as Button).PerformClick();
            //    }
            //}

            base.Update();
        }       
        private void BtnTest1_Click(object sender, ClickEventArgs e)
        {
            OnSceneFinished(new SceneFinishedEventArgs() { NewSceneName = "BaseMovement" });
        }

        private void BtnExit_Click(object sender, ClickEventArgs e)
        {
            OnGameFinished();
        }
    }
}
