using Microsoft.Xna.Framework;


namespace WiseEngine.UI;

public class MouseInterfaceManager : ICursor
{
    public List<IComponent> InterfaceElements { get; private set; }

    public Point CursorPos { get; set; }

    public MouseInterfaceManager() 
    {
        InterfaceElements = new List<IComponent>();
        CursorPos = new Point();
    }

    public void AddElement(IComponent component)
    {
        InterfaceElements.Add(component);
        Update();
    }

    public IComponent? GetCurrentElement()
    {
        return InterfaceElements.FirstOrDefault(c => c.IsInteractive && c.Bounds.Contains(CursorPos));
    }

    public void TransformCursor(Point newPos)
    {
        CursorPos = newPos;
        Update();
    }

    public void Update()
    {
        InterfaceElements.ForEach(element => element.IsChosen = false);
        var chosenElement = GetCurrentElement();
        if (chosenElement != null) 
        {
            chosenElement.IsChosen = true;
        }
    }
}
