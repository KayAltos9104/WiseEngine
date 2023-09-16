using Microsoft.Xna.Framework;
using System;
using WiseEngine;

namespace WiseTestBench.ExampleSceneTriggerWork;
public class TestTrigger : RectangleCollider, ITrigger
{
    public TestTriggerEventArgs TriggerData { get; set; }

    public event EventHandler<EventArgs> Triggered;

    public TestTrigger(Vector2 pos, int width, int height) : base(pos, width, height)
    {
        TriggerData = new TestTriggerEventArgs();
    }
    public void OnTriggered()
    {
        Triggered.Invoke(this, TriggerData);
    }
}

public class TestTriggerEventArgs : EventArgs
{
    Vector2 PlayerPos { get; set; }
}
