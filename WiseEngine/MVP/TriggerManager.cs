namespace WiseEngine.MVP;

public sealed class TriggerManager
{
    public event EventHandler<TriggerEventArgs> TriggerEntered;
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
    }

    public void Update(List<IShaped> objects)
    {
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
            }
        });
    }
    public void AddTrigger(ITrigger trigger)
    {
        _triggers.Add(trigger);
        TriggerEntered += trigger.OnTriggered;
    }
    

}

public class TriggerEventArgs
{
    public ITrigger ActivatedTrigger { get; set;}
    public IObject ActivatedObject { get; set;}
}
