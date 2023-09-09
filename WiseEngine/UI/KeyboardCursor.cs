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
    public void TransformCursor(Point newPos)
    {
        if (InterfaceElements.Count >0 && InterfaceElements.All(e => e.IsInteractive == false))
            return;
        int shift = newPos.X >= 0 ? 1 : -1;
        int currentIndex = InterfaceElements.FindIndex(i => i.Bounds.Center == CursorPos);
        do
        {
            currentIndex += shift;
            if (currentIndex < 0) currentIndex = InterfaceElements.Count - 1;
            else if (currentIndex > InterfaceElements.Count - 1) currentIndex = 0;
        } while (InterfaceElements[currentIndex].IsInteractive == false); 

        CursorPos = InterfaceElements[currentIndex].Bounds.Center;
    }

}
