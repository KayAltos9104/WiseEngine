﻿using Microsoft.Xna.Framework.Input;
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
    }
}
