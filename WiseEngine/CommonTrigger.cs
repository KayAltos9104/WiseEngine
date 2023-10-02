using Microsoft.Xna.Framework;
using WiseEngine;
using WiseEngine.MVP;

namespace WiseEngine;
public class CommonTrigger : ITrigger
{
    public string Name { get; set; }
    public Vector2 Pos { get; set; }
    public RectangleCollider Collider { get; private set; }
   
    public event EventHandler<TriggerEventArgs>? TriggeredInside;
    public event EventHandler<TriggerEventArgs>? TriggeredOutside;

    public CommonTrigger(Vector2 pos, int width, int height)
    {
        
        Pos = pos;    
        Name = Guid.NewGuid().ToString();
        // Collider position is given in relative coordinates
        Collider = new RectangleCollider(Vector2.Zero, width, height);
        Collider.Color = Color.White;
    }
    public virtual void OnTriggeredInside(object sender, TriggerEventArgs e)
    {
        if (e.ActivatedTrigger == this)
        {
            TriggeredInside?.Invoke(this, e);
        } 
    }

    public virtual void OnTriggeredOutside(object sender, TriggerEventArgs e)
    {
        if (e.ActivatedTrigger == this)
        {
            TriggeredOutside?.Invoke(this, e);
        }
    }

    public Collider GetCollider()
    {
        return new RectangleCollider(Pos + Collider.Position,
            Collider.Area.Width,
            Collider.Area.Height)
        { Color = Collider.Color };
    }
}


