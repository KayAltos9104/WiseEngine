using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using WiseEngine.MonogamePart;
using WiseEngine.MVP;


namespace LittleWitch.Scenes;

public class LevelViewPrefab : View
{
    public override void Initialize()
    {
        base.Initialize();
        _outputData = new LevelViewModelData();
        _inputData = new LevelModelViewData();
    }

    public override void Update()
    {
        var player = GetInputData<LevelModelViewData>().Player;
        
        Vector2 sV = Vector2.Zero;
        var outData = GetOutputData<LevelViewModelData>();
        if (InputsManager.PressedCurrentFrame.IsKeyDown(Keys.W))
            sV -= Vector2.UnitY;
        if (InputsManager.PressedCurrentFrame.IsKeyDown(Keys.A))
            sV -= Vector2.UnitX;
        if (InputsManager.PressedCurrentFrame.IsKeyDown(Keys.S))
            sV += Vector2.UnitY;
        if (InputsManager.PressedCurrentFrame.IsKeyDown(Keys.D))
            sV += Vector2.UnitX;

        _interfaceManager.TransformCursor(InputsManager.MouseStateCurrentFrame.Position);

        if (InputsManager.IsSingleClicked(InputsManager.MouseButton.Left))
        {
            _interfaceManager.ClickCurrentElement();
        }

        outData.PlayerSpeed = sV;
        base.Update();       
    }

}
