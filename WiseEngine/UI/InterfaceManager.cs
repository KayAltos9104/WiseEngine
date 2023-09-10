using Microsoft.Xna.Framework;


namespace WiseEngine.UI;
/// <summary>
/// Manages interaction between inputs and interface elements
/// </summary>
public class InterfaceManager
{
    /// <summary>
    /// List with all UI components on View
    /// </summary>
    public List<IComponent> InterfaceElements { get; private set; }
    /// <summary>
    /// Current cursor position
    /// </summary>
    public Point CursorPos { get; private set; }
    public InterfaceManager()
    { 
        InterfaceElements = new List<IComponent>();        
        Update();       
    }
    /// <summary>
    /// Moves cursor to the next UI element
    /// </summary>
    /// <param name="step">Direction of cursor moving - next or previous element in list</param>
    public void MoveCursor(CursorStep step)
    {
        if (InterfaceElements.Count > 0 && InterfaceElements.All(e => e.IsInteractive == false))
            return;
        int shift = step == CursorStep.Up ? 1 : -1;
        int currentIndex = InterfaceElements.FindIndex(i => i.Bounds.Center == CursorPos);
        do
        {
            currentIndex += shift;
            if (currentIndex < 0) currentIndex = InterfaceElements.Count - 1;
            else if (currentIndex > InterfaceElements.Count - 1) currentIndex = 0;
        } while (InterfaceElements[currentIndex].IsInteractive == false);

        CursorPos = InterfaceElements[currentIndex].Bounds.Center;
    }

    public void AddElement(IComponent component)
    {
        InterfaceElements.Add(component);
        if (InterfaceElements.FindAll(e => e.IsInteractive).Count() == 1)
        {
            MoveCursor(CursorStep.Down);
        }
    }
    public IComponent? GetCurrentElement()
    {
        return InterfaceElements.FirstOrDefault(c => c.IsInteractive && c.Bounds.Contains(CursorPos));
    }
    public void ClickCurrentElement ()
    {
        var chosenElement = GetCurrentElement();
        if (chosenElement != null)
        {
            chosenElement.PerformClick();
        }
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

    public void TransformCursor(Point cursorPos)
    {
        CursorPos = cursorPos;        
    }

    public enum CursorStep: byte
    {
        Up,
        Down,
    }
}

public class InterfaceInputsEventArgs : EventArgs
{
    public List<IComponent>? InterfaceElements { get; set; } = new List<IComponent>();
    public Point CursorPoint { get; set; }
}
