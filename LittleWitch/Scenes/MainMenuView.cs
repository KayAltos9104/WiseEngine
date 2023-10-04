using LittleWitch.UI;
using Microsoft.Xna.Framework;
using System;
using WiseEngine;
using WiseEngine.MonogamePart;
using WiseEngine.MVP;

namespace LittleWitch.Scenes;

internal class MainMenuView : MenuViewPrefab
{
    
    public override void Initialize()
    {
        base.Initialize();
        int screenWidth = Globals.Resolution.Width;
        int screenHeight = Globals.Resolution.Height;
        Button BtnStart = new ScrollButton(
            new Vector2(screenWidth/2, (int)(Globals.Resolution.Height * 0.2)),
            LoadableObjects.GetFont("SystemFont"),
            "Start game",
            LoadableObjects.GetTexture("Scroll1")
            );
        BtnStart.Center();
        BtnStart.IsCentered = true;
        BtnStart.ChangeSize(250, 100);

        Button BtnExit = new ScrollButton(
            new Vector2(screenWidth / 2, (int)(Globals.Resolution.Height * 0.35)),
            LoadableObjects.GetFont("SystemFont"),
            "Exit game",
            LoadableObjects.GetTexture("Scroll1")
            );
        BtnExit.Center();
        BtnExit.IsCentered = true;
        BtnExit.ChangeSize(250, 100);

        BtnStart.Clicked += BtnStart_Click;
        BtnExit.Clicked += BtnExit_Click;

        _interfaceManager.AddElement(BtnStart);
        _interfaceManager.AddElement(BtnExit);
    }

    private void BtnStart_Click (object sender, ClickEventArgs e)
    {
        GameConsole.WriteLine("Заглушка!");
    }

    private void BtnExit_Click(object sender, ClickEventArgs e)
    {
        OnGameFinished();
    }
}
