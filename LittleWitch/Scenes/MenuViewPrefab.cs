using WiseEngine.MonogamePart;
using WiseEngine;
using WiseEngine.MVP;
using Microsoft.Xna.Framework.Input;
using WiseEngine.UI;

namespace LittleWitch.Scenes;

internal class MenuViewPrefab : View
{
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
