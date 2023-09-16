using Microsoft.Xna.Framework;
using System;
using WiseEngine;

namespace WiseTestBench.ExampleSceneTriggerWork;
public class TestTrigger : ITrigger
{
    public string Name { get; set; }
    public Vector2 Pos { get; set; }
    public RectangleCollider Collider { get; private set; }
    public TestTriggerEventArgs TriggerData { get; set; }

    public event EventHandler<EventArgs> Triggered;

    public TestTrigger(Vector2 pos, int width, int height)
    {
        TriggerData = new TestTriggerEventArgs();
        Pos = pos;    
        Name = Guid.NewGuid().ToString();
        // Collider position is given in relative coordinates
        Collider = new RectangleCollider(Vector2.Zero, width, height);
        Collider.Color = Color.White;
    }
    public void OnTriggered(EventArgs e)
    {
        Triggered.Invoke(this, e);
    }

    public Collider GetCollider()
    {
        return new RectangleCollider(Pos + Collider.Position,
            Collider.Area.Width,
            Collider.Area.Height)
        { Color = Collider.Color };
    }
}

public class TestTriggerEventArgs : EventArgs
{
    public IObject ObjIntersected { get; set; }
}
