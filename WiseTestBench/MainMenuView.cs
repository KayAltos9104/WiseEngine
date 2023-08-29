using Microsoft.Xna.Framework.Input;
using System;
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
            MessageBox MbxTest = new MessageBox(new Vector2(
                    Globals.Resolution.Width / 2, Globals.Resolution.Height / 2 - 100),
                    LoadableObjects.GetFont("MainFont"),
                    "Тестовое окно\nДля вызова консоли нажмите Ctrl+Q"
                    );
            MbxTest.IsCentered = true;


            Button BtnTest = new Button(new Vector2(
                    Globals.Resolution.Width / 2, Globals.Resolution.Height / 2 + 100),
                    LoadableObjects.GetFont("MainFont"), "Перейти к сцене 2");
            Button BtnTest2 = new Button(new Vector2(
                    Globals.Resolution.Width / 2, Globals.Resolution.Height / 2 + 150),
                    LoadableObjects.GetFont("MainFont"), "Нет, меня!");
            Button BtnTest3 = new Button(new Vector2(
                    Globals.Resolution.Width / 2, Globals.Resolution.Height / 2 + 250),
                    LoadableObjects.GetFont("MainFont"), "Выход");

            BtnTest.Click += BtnTest1_Click;
            BtnTest2.Click += BtnTest2_Click;
            BtnTest3.Click += BtnTest3_Click;

            _interfaceManager.AddElement(MbxTest);
            _interfaceManager.AddElement(BtnTest);
            _interfaceManager.AddElement(BtnTest2);
            _interfaceManager.AddElement(BtnTest3);
        }

        public override void Update()
        {

            if (InputsManager.IsSinglePressed(Keys.W))
                ((IKeyboardCursor)_interfaceManager).MoveCursor(DiscreteDirection.Up);
            if (InputsManager.IsSinglePressed(Keys.S))
                ((IKeyboardCursor)_interfaceManager).MoveCursor(DiscreteDirection.Down);
            if (InputsManager.IsSinglePressed(Keys.F))
                GameConsole.WriteLine("Консоль работает. Для очистки консоли нажмите Ctrl+R");
            if (InputsManager.IsSinglePressed(Keys.Space))
                (((IKeyboardCursor)_interfaceManager).GetCurrentElement() as Button).PerformClick();

            base.Update();
        }

        public override void LoadModelData(ModelViewData currentModelData)
        {
            throw new NotImplementedException();
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
            OnSceneFinished(new SceneFinishedEventArgs() { NewSceneName = "Test2" });
        }
        private void BtnTest2_Click(object sender, ClickEventArgs e)
        {
            MessageBox MbxTest2 = new MessageBox(new Vector2(
                    250, 60),
                    LoadableObjects.GetFont("MainFont"),
                    "Нажата кнопка 2"
                    );
            MbxTest2.IsCentered = true;

            _interfaceManager.AddElement(MbxTest2);
        }
        private void BtnTest3_Click(object sender, ClickEventArgs e)
        {
            MessageBox MbxTest3 = new MessageBox(new Vector2(
                    250, 90),
                    LoadableObjects.GetFont("MainFont"),
                    "Заглушка на выход из программы"
                    );
            MbxTest3.IsCentered = true;
            _interfaceManager.AddElement(MbxTest3);
        }
    }
}
