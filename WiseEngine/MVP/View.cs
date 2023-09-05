using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Security.Cryptography.X509Certificates;
using WiseEngine.MonogamePart;
using WiseEngine.UI;

namespace WiseEngine.MVP;

/// <summary>
/// Parent class for all views in game. View draws objects and processes intercation with user
/// </summary>
public abstract class View
{
    /// <value>
    /// Property <c>_outputData</c> contains game data for transfering to view
    /// </value>
    protected ViewModelData _outputData;
    /// <value>
    /// Property <c>_inputData</c> contains game data for transfering to view
    /// </value>
    protected ModelViewData _inputData;
    protected InterfaceManager _interfaceManager;

    /// <value>
    /// Event <c>OnCycleFinished</c> invokes when View ended cycle processing
    /// </value>
    public EventHandler<ViewCycleFinishedEventArgs>? CycleFinished;
    /// <value>
    /// Event <c>SceneFinished</c> invokes when we should finish of pause current scene and switch to another
    /// </value>
    public EventHandler<SceneFinishedEventArgs>? SceneFinished;

  
    
    public View()
    {        
        _interfaceManager = new InterfaceManager(); 
        _outputData = new ViewModelData();
        _inputData = new ModelViewData();
    }
    /// <summary>
    /// Gets <c>_inputData</c> 
    /// </summary>
    /// <typeparam name="T">Correct successor of <see cref="ModelViewData"/></typeparam>
    /// <returns><c>_inputData</c> which should be transferred to model</returns>
    public T? GetInputData<T>() where T : ModelViewData
    {
        return (T)_inputData;
    }
    /// <summary>
    /// Gets <c>_outputData</c> 
    /// </summary>
    /// <typeparam name="T">Correct successor of <see cref="ViewModelData"/></typeparam>
    /// <returns><c>__outputData</c> which should be transferred to model</returns>
    public T? GetOutputData<T>() where T : ViewModelData
    {
        return (T)_outputData;
    }
    /// <summary>
    /// Invokes <see cref="SceneFinished"/> event
    /// </summary>
    /// <param name="e">Object of <see cref="SceneFinishedEventArgs"/> which contains new scene name</param>
    protected void OnSceneFinished(SceneFinishedEventArgs e)
    {
        SceneFinished?.Invoke(this, e);
    }

    /// <summary>
    /// Initialize all view elements. Must be called. 
    /// </summary>
    public abstract void Initialize();

    /// <summary>
    /// Processes inputs, draws objects and invoke event about cycle ending.
    /// </summary>
    public virtual void Update()
    {
        CycleFinished?.Invoke(this, new ViewCycleFinishedEventArgs() { CurrentViewData = _outputData });
    }
    /// <summary>
    /// Loads game model data (list of objects for example).
    /// </summary>
    /// <param name="currentModelData">
    /// Model data which should be loaded in view.
    /// </param>
    public void LoadModelData(ModelViewData currentModelData)
    {
        _inputData = currentModelData;
    }

    /// <summary>
    /// Draws all game objects and interface elements.
    /// </summary>
    public virtual void Draw()
    {        
        if (_inputData != null)
        {
            foreach (var o in _inputData.CurrentFrameObjects)
            {
                Graphics2D.RenderObject(o);
                
                //if (o is IAnimated)
                //    Graphics2D.RenderAnimation(o as IAnimated);
            }
        }
        if (_interfaceManager != null)
        {
            foreach (var ui in _interfaceManager.InterfaceElements)
                ui.Render(Graphics2D.SpriteBatch);
        } 
    }
    /// <summary>
    /// Process all inputs from user (inputs should be taken from <see cref="InputsManager"/>).
    /// </summary>
    //public abstract void ProcessInputs();
    
    
}
/// <summary>
/// Class with fields for transfer from view to model after one cycle
/// </summary>
public class ViewCycleFinishedEventArgs : EventArgs
{
    public ViewModelData CurrentViewData { get; set; } = new ViewModelData();
}
/// <summary>
/// Class that contains name of scene for GameProcessor to switch for
/// </summary>
public class SceneFinishedEventArgs : EventArgs
{
    public string NewSceneName { get; set; } = "";
}

