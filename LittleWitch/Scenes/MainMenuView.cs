using LittleWitch.Prefabs;
using LittleWitch.UI;
using Microsoft.Xna.Framework;
using System;
using WiseEngine;
using WiseEngine.MonogamePart;
using WiseEngine.MVP;

namespace LittleWitch.Scenes;

internal class MainMenuView : MenuViewPrefab
{
    private Background _background;
    public override void Initialize()
    {
        base.Initialize();

        

        int screenWidth = Globals.Resolution.Width;
        int screenHeight = Globals.Resolution.Height;

        _background = new Background(screenWidth, screenHeight);
        
        //_background.Sprites[0].Scale = new Vector2(
        //    1.0f*screenWidth / _background.Sprites[0].GetTexture().Width,
        //    1.0f * screenHeight / _background.Sprites[0].GetTexture().Height);

        _interfaceManager.AddElement(
            UIFactory.CreateScrollButton(
                "Start game", 
                new Vector2(screenWidth / 2, (int)(Globals.Resolution.Height * 0.2)), 
                BtnStart_Click));

        _interfaceManager.AddElement(
            UIFactory.CreateScrollButton(
                "Settings",
                new Vector2(screenWidth / 2, (int)(Globals.Resolution.Height * 0.35)),
                BtnStart_Click));

        _interfaceManager.AddElement(
            UIFactory.CreateScrollButton(
                "Exit game",
                new Vector2(screenWidth / 2, (int)(Globals.Resolution.Height * 0.5)),
                BtnExit_Click));        
    }

    private void BtnStart_Click (object sender, ClickEventArgs e)
    {
        GameConsole.WriteLine("Заглушка!");
    }

    private void BtnExit_Click(object sender, ClickEventArgs e)
    {
        OnGameFinished();
    }

    public override void Draw()
    {
        Graphics2D.SpriteBatch.Begin();
        Graphics2D.Render(_background, null);
        Graphics2D.SpriteBatch.End();
        base.Draw();
        
    }
}
