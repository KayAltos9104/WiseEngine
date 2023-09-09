using Microsoft.Xna.Framework;


namespace WiseEngine.UI;

public class InterfaceManager
{
    public List<IComponent> InterfaceElements { get; private set; }
    
    public ICursor Cursor { get; private set; }

    public InterfaceManager(ICursor cursor)
    { 
        InterfaceElements = new List<IComponent>();
        Cursor = cursor;
        Cursor.InterfaceElements = InterfaceElements;
        Update();       
    }

    public void AddElement(IComponent component)
    {
        InterfaceElements.Add(component);
        if (Cursor.CursorPos == Point.Zero)
        {
            Cursor.TransformCursor(Point.Zero);
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
}
