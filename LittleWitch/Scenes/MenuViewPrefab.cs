using WiseEngine.MonogamePart;
using WiseEngine;
using WiseEngine.MVP;
using LittleWitch.UI;
using System.Numerics;
using System;

namespace LittleWitch.Scenes;

public class MenuViewPrefab : View
{
    delegate void ButtonClickDelegate(object sender, ClickEventArgs e);
    public override void Update()
    {
        _interfaceManager.TransformCursor(InputsManager.MouseStateCurrentFrame.Position);
        if (InputsManager.IsSingleClicked(InputsManager.MouseButton.Left))
        {
            _interfaceManager.ClickCurrentElement();
        }
        base.Update();
    }

    
}
