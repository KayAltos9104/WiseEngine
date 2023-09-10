using WiseEngine.MonogamePart;
using static WiseEngine.MVP.Model;

namespace WiseEngine.MVP;
/// <summary>
/// Presenter class which connects <see cref="View"/> and <see cref="Model"/> in <see cref="Scene"/>
/// </summary>
public sealed class Presenter
{
    private readonly GameProcessor _processor;
    private readonly View _view;
    private readonly Model? _model;
    /// <summary>
    /// Constructor for base MVP Presenter
    /// </summary>
    /// <param name="processor"><see cref="GameProcessor"/> of the game</param>
    /// <param name="view"><see cref="View"/> of MVP</param>
    /// <param name="model"><see cref="Model"/> of MVP</param>
    /// <remarks>
    /// Cannot be inherited. Should not be changed in game project
    /// </remarks>
    public Presenter(GameProcessor processor, View view, Model model)
    {
        _processor = processor;
        _view = view;
        _model = model;
        
        _view.CycleFinished += UpdateModel;
        _view.SceneFinished += SwitchScene;
        _view.GameFinished += CallExit;

        if (_model != null) 
        {
            _model.OnCycleFinished += LoadModelDataToView;
        }
        
    }
    /// <summary>
    /// Calls <c>Exit</c> method from GameProcessor
    /// </summary>
    /// <param name="sender"><see cref="_view"/> object which calls the <c>GameFinished</c> event</param>
    /// <param name="e">Empty argument</param>
    private void CallExit(object? sender, EventArgs e)
    {
        _processor.Exit();  
    }

    /// <summary>
    /// Updates <see cref="_model"/> game logic after <see cref="View"/> finished its cycle
    /// </summary>
    /// <param name="sender">Sender of the <c>_view.OnCycleFinished</c> event</param>
    /// <param name="e">Data from <see cref="_view"/></param>
    private void UpdateModel(object? sender, ViewCycleFinishedEventArgs e)
    {
        if (_model != null) 
            _model.Update(e);
    }
    /// <summary>
    /// Loads data from <see cref="_model"/> to <see cref="_view"/> after model finishes its cycle
    /// </summary>
    /// <param name="sender">Sender of the <c>_model.OnCycleFinished</c> event</param>
    /// <param name="e">Data from <see cref="_model"/></param>
    private void LoadModelDataToView (object? sender, ModelCycleFinishedEventArgs e)
    {
        if (_model != null) 
            _view.LoadModelData(e.ModelViewData);
    }
    /// <summary>
    /// Changes current <see cref="Scene"/>
    /// </summary>
    /// <param name="sender">Sender of the <c>_view.SceneFinished</c> event</param>
    /// <param name="e">Data from <see cref="_view"/></param>
    /// <remarks>
    /// Accesses to <see cref="GameProcessor"/>
    /// </remarks>
    private void SwitchScene (object? sender, SceneFinishedEventArgs e)
    {
        _processor.SetCurrentScene(e.NewSceneName);
    }
}
