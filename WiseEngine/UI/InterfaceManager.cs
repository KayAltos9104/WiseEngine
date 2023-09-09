using Microsoft.Xna.Framework;


namespace WiseEngine.UI;

public class InterfaceManager
{
    public List<IComponent> InterfaceElements { get; private set; }
    
    public ICursor Cursor { get; private set; }
    public EventHandler<InterfaceInputsEventArgs>? OnInputsChanged { get; set; }
    

    public InterfaceManager(ICursor cursor)
    { 
        InterfaceElements = new List<IComponent>();
        Cursor = cursor;
        OnInputsChanged += Cursor.Update;
        Update();       
    }

    public void AddElement(IComponent component)
    {
        InterfaceElements.Add(component);
        if (InterfaceElements.Count == 1) 
        {
            OnInputsChanged?.Invoke(this, new InterfaceInputsEventArgs() 
            { 
                CursorPoint = Point.Zero, 
                InterfaceElements = InterfaceElements
            });
        }
    }
    public IComponent? GetCurrentElement()
    {
        return InterfaceElements.FirstOrDefault(c => c.IsInteractive && c.Bounds.Contains(Cursor.CursorPos));
    }

    public void Update()
    {
        InterfaceElements.ForEach(element => element.IsChosen = false);
        IComponent? e;
        if ((e = GetCurrentElement()) != null) 
        {
            e.IsChosen = true;
        }
    }

    public void ChangeCursor(Point cursorPos)
    {
        OnInputsChanged?.Invoke(this, new InterfaceInputsEventArgs()
        {
            CursorPoint = cursorPos,
            InterfaceElements = InterfaceElements
        });
    }
}

public class InterfaceInputsEventArgs : EventArgs
{
    public List<IComponent>? InterfaceElements { get; set; } = new List<IComponent>();
    public Point CursorPoint { get; set; }
}
