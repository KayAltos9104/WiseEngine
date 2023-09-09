using Microsoft.Xna.Framework;


namespace WiseEngine.UI;

public class KeyboardCursor : ICursor
{
    public List<IComponent> InterfaceElements { get; set; }

    public Point CursorPos { get; private set; }


    public KeyboardCursor()
    {
        CursorPos = new Point(0, 0);
    }
    public void TransformCursor(Point newPos, List<IComponent> interfaceElements)
    {
        if (interfaceElements.Count > 0 && interfaceElements.All(e => e.IsInteractive == false))
            return;
        int shift = newPos.X >= 0 ? 1 : -1;
        int currentIndex = interfaceElements.FindIndex(i => i.Bounds.Center == CursorPos);
        do
        {
            currentIndex += shift;
            if (currentIndex < 0) currentIndex = interfaceElements.Count - 1;
            else if (currentIndex > interfaceElements.Count - 1) currentIndex = 0;
        } while (interfaceElements[currentIndex].IsInteractive == false); 

        CursorPos = interfaceElements[currentIndex].Bounds.Center;
    }

    public void Update (object? sender, InterfaceInputsEventArgs e)
    {
        TransformCursor(e.CursorPoint, e.InterfaceElements);
    }
}
