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
    /// <remarks>
    /// Can be used for cursor position initialization
    /// </remarks>
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
    /// <summary>
    /// Adds new UI component in list
    /// </summary>
    /// <param name="component">Component to add</param>
    public void AddElement(IComponent component)
    {
        InterfaceElements.Add(component);
        if (InterfaceElements.FindAll(e => e.IsInteractive).Count() == 1)
        {
            MoveCursor(CursorStep.Down);
        }
    }
    /// <summary>
    /// Gets interface component on which cursor in current time
    /// </summary>
    /// <returns><see cref="IComponent"/> from list or <c>null</c> if cursor is not in <Bounds> of any component</returns>
    public IComponent? GetCurrentElement()
    {
        return InterfaceElements.FirstOrDefault(c => c.IsInteractive && c.Bounds.Contains(CursorPos));
    }
    /// <summary>
    /// Calls <see cref="IComponent.OnClicked"/> method for element on which cursor is
    /// </summary>
    public void ClickCurrentElement ()
    {
        var chosenElement = GetCurrentElement();
        if (chosenElement != null)
        {
            chosenElement.OnClicked();
        }
    }
    /// <summary>
    /// Updates components state
    /// </summary>
    public void Update()
    {
        InterfaceElements.ForEach(element => element.IsChosen = false);
        IComponent? e;
        if ((e = GetCurrentElement()) != null) 
        {
            e.IsChosen = true;
        }
    }
    /// <summary>
    /// Moves cursor to a given coordinate
    /// </summary>
    /// <param name="cursorPos">New cursor position</param>
    public void TransformCursor(Point cursorPos)
    {
        CursorPos = cursorPos;        
    }
    /// <summary>
    /// Directions in whick cursor can move
    /// </summary>
    public enum CursorStep: byte
    {
        Up,
        Down,
    }
}
