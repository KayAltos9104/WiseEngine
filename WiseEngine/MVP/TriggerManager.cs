using System.Diagnostics.Tracing;

namespace WiseEngine.MVP;

public sealed class TriggerManager
{
    private Dictionary<(IObject obj, ITrigger t), bool> _previousState;


    public event EventHandler<TriggerEventArgs>? TriggerEntered;
    public event EventHandler<TriggerEventArgs>? TriggerExited;
    

    
    /// <value>
    /// Field <c>_triggers</c> contains all triggers in <see cref="Scene">scene</see>
    /// </value>
    private List<ITrigger> _triggers;
    public IReadOnlyList<ITrigger> GetTriggers()
    {
        return _triggers;
    }
    public TriggerManager() 
    {
        _triggers = new List<ITrigger>();
        _previousState = new Dictionary<(IObject obj, ITrigger t), bool>();
    }

    public void Update(List<IShaped> objects)
    {
        _previousState.Clear();
        _triggers.ForEach(t =>
        {
            objects.ForEach(o =>
            {
                if (Collider.IsIntersects(t.GetCollider(), o.GetCollider()))
                {   
                    TriggerEntered?.Invoke(this, new TriggerEventArgs() 
                    { 
                        ActivatedObject = (IObject)o, 
                        ActivatedTrigger = t
                    });
                    _previousState[((IObject)o, t)] = true;
                }
                else
                {
                    TriggerExited?.Invoke(this, new TriggerEventArgs()
                    {
                        ActivatedObject = (IObject)o,
                        ActivatedTrigger = t
                    });
                    _previousState[((IObject)o, t)] = false;
                }
            });
        });
    }

    public void Update (IShaped obj)
    {
        _triggers.ForEach(t =>
        {
            if (Collider.IsIntersects(t.GetCollider(), obj.GetCollider()))
            {                
                TriggerEntered?.Invoke(this, new TriggerEventArgs()
                {
                    ActivatedObject = (IObject)obj,
                    ActivatedTrigger = t
                });
                _previousState[((IObject)obj, t)] = true;
            }
            else
            {
                TriggerExited?.Invoke(this, new TriggerEventArgs()
                {
                    ActivatedObject = (IObject)obj,
                    ActivatedTrigger = t
                });
                _previousState[((IObject)obj, t)] = false;
            }
        });
    }
    public void AddTrigger(ITrigger trigger)
    {
        _triggers.Add(trigger);
        TriggerEntered += trigger.OnTriggeredInside;
        TriggerExited += trigger.OnTriggeredOutside;
    }

    

    public void ClearPreviousStates ()
    {
        _previousState.Clear();
    }
    

}
public class TriggerEventArgs
{
    public ITrigger ActivatedTrigger { get; set; }
    public IObject? ActivatedObject { get; set; }
}
