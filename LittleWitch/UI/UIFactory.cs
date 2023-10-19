using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiseEngine.MonogamePart;
using WiseEngine;
using Microsoft.Xna.Framework;

namespace LittleWitch.UI;

public static class UIFactory
{
    public static Button CreateScrollButton(string text, Vector2 pos, EventHandler<ClickEventArgs> clickMethod)
    {
        Button button = new ScrollButton(
            pos,
            LoadableObjects.GetFont("MainFont"),
            text,
            //LoadableObjects.GetTexture("Scroll1")
            "Scroll1"
            );
        button.IsCentered = true;
        button.ChangeSize(250, 100);
        button.Center();

        button.Clicked += clickMethod;

        return button;
    }
}
