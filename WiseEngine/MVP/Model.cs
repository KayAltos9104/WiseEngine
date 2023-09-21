using System.Diagnostics;

namespace WiseEngine.MVP;
/// <summary>
/// Abstract class which contains all game logic
/// </summary>
/// <remarks>
/// All game logic of <see cref="Scene">scene</see> should be written here
/// </remarks>
public abstract class Model
{
    /// <value>
    /// Property <c>_outputData</c> contains game data for transfering to view
    /// </value>
    protected ModelViewData _outputData { get; set; }
    /// <value>
    /// Property <c>_inputData</c> contains game data for transfering to view
    /// </value>
    protected ViewModelData _inputData { get; set; }
    /// <value>
    /// Property <c>GameObjects</c> contains all game objects in <see cref="Scene">scene</see>
    /// </value>
    public List<IObject> GameObjects { get; set; }
    /// <value>
    /// Property <c>_triggers</c> contains all triggers in <see cref="Scene">scene</see>
    /// </value>
    public TriggerManager TriggerManager { get; set; }
    /// <value>
    /// Event <c>OnCycleFinished</c> that activates when Model ended cycle processing
    /// </value>
    /// <remarks>
    /// This event sends <see cref="ModelCycleFinishedEventArgs"/> which contains game logic data for view
    /// </remarks>
    public EventHandler<ModelCycleFinishedEventArgs>? OnCycleFinished;

    /// <summary>
    /// Gets <c>_inputData</c> 
    /// </summary>
    /// <typeparam name="T">Correct successor of <see cref="ViewModelData"/></typeparam>
    /// <returns><c>_inputData</c> which should be transferred to model</returns>
    public T? GetInputData<T>() where T : ViewModelData
    {
        return (T)_inputData;
    }
    /// <summary>
    /// Gets <c>_outputData</c> 
    /// </summary>
    /// <typeparam name="T">Correct successor of <see cref="ModelViewData"/></typeparam>
    /// <returns><c>_outputData</c> which should be transferred to model</returns>
    public T? GetOutputData<T>() where T : ModelViewData
    {
        return (T)_outputData;
    }
    /// <summary>
    /// Initializes model - objects, parameters, etc.
    /// </summary>
    /// <remarks>
    /// Should be called after <c>Model</c> creating and <u><b>base method</b></u> should be called
    /// </remarks>
    public virtual void Initialize()
    {
        GameObjects = new List<IObject>();
        TriggerManager = new TriggerManager();
        _outputData = new ModelViewData();
        _inputData = new ViewModelData();
    }
    /// <summary>
    /// Updates game logic for scene
    /// </summary>
    /// <param name="e">Parameters from <see cref="View"/></param>
    /// <remarks>
    /// Should be called every frame
    /// </remarks>
    public virtual void Update(ViewCycleFinishedEventArgs e)
    {
        List<IObject> disposableObjects = new List<IObject>();
        _inputData = e.CurrentViewData;
        foreach (var obj in GameObjects)
        {
            if (obj.IsDisposed)
            {
                disposableObjects.Add(obj);
            }
            else
            {
                obj.Update();
            }
            if (obj is IShaped)
            {
                TriggerManager.Update(obj as IShaped);
            }
        }
        GameObjects.RemoveAll(o => disposableObjects.Contains(o));

        _outputData.CurrentFrameObjects = new List<IObject>(GameObjects);
        _outputData.Triggers = new List<ITrigger>(TriggerManager.GetTriggers());
        OnCycleFinished?.Invoke(this, new ModelCycleFinishedEventArgs() { ModelViewData = _outputData});
    }

    /// <summary>
    /// Class with fields for transfer from model to view after one cycle
    /// </summary>
    public class ModelCycleFinishedEventArgs : EventArgs
    {
        /// <value>
        /// Property that contains all data from game logic which should be sent to <see cref="View"/>
        /// </value>
        public ModelViewData ModelViewData { get; set; }

        //public ModelCycleFinishedEventArgs(List<IObject> currentFrameObjects)
        //{
        //    ModelViewData = new ModelViewData(currentFrameObjects);
        //}
    }
}
