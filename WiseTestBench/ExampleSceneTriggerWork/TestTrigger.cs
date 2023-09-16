using Microsoft.Xna.Framework;
using System;
using WiseEngine;

namespace WiseTestBench.ExampleSceneTriggerWork;
public class TestTrigger : ITrigger
{
    public Vector2 Pos { get; set; }
    public RectangleCollider Collider { get; private set; }
    public TestTriggerEventArgs TriggerData { get; set; }

    public event EventHandler<EventArgs> Triggered;

    public TestTrigger(Vector2 pos, int width, int height)
    {
        TriggerData = new TestTriggerEventArgs();
        Pos = pos;    
        // Collider position is given in relative coordinates
        Collider = new RectangleCollider(Vector2.Zero, width, height);
    }
    public void OnTriggered()
    {
        Triggered.Invoke(this, TriggerData);
    }

    public Collider GetCollider()
    {
        return new RectangleCollider(Pos+Collider.Position, 
            Collider.Area.Width, 
            Collider.Area.Height);
    }
}

public class TestTriggerEventArgs : EventArgs
{
    Vector2 PlayerPos { get; set; }
}
