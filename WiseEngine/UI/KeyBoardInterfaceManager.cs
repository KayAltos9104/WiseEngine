using Microsoft.Xna.Framework;


namespace WiseEngine.UI;

public class KeyBoardInterfaceManager: ICursor
{
    private Point _cursorPos;
    public List<IComponent> InterfaceElements { get; private set; }
    public int firstActiveElementIndex { get; set; }
    public int lastActiveElementIndex { get; set; }
    public Point CursorPos
    {
        get
        {
            return _cursorPos;
        }
        set
        {
            //int x = MathHelper.Clamp(value.X, firstActiveElementIndex, lastActiveElementIndex);
            //int y = value.Y;
            //_cursorPos = new Point(x, y);
            _cursorPos = value;
        }
    }
    public int CursorPosX
    {
        set
        {
            //int x = MathHelper.Clamp(value, firstActiveElementIndex, lastActiveElementIndex);            
            //_cursorPos = new Point(x, _cursorPos.Y);
           
            _cursorPos = new Point(value, _cursorPos.Y);
        }
        get
        {
            return _cursorPos.X;
        }
    }
    public KeyBoardInterfaceManager()
    { 
        InterfaceElements = new List<IComponent>();
        firstActiveElementIndex = InterfaceElements.FindIndex(e => e.IsInteractive == true);
        lastActiveElementIndex = InterfaceElements.FindLastIndex(e => e.IsInteractive == true);
        CursorPos = new Point(0, 0);
        CursorPosX = firstActiveElementIndex;
        Update();       
    }



    //public IComponent? GetComponent (Point mousePos)
    //{
    //    return InterfaceElements.FirstOrDefault(c => c.IsInteractive && c.Bounds.Contains(mousePos));
    //}

    public void AddElement(IComponent component)
    {
        InterfaceElements.Add(component);
        firstActiveElementIndex = InterfaceElements.FindIndex(e => e.IsInteractive == true);
        lastActiveElementIndex = InterfaceElements.FindLastIndex(e => e.IsInteractive == true);
        if (CursorPosX == -1)
        {
            CursorPosX = firstActiveElementIndex;
            Update();
        }
    }
    public IComponent GetCurrentElement()
    {
        return InterfaceElements[CursorPosX];
    }
    public void Update()
    {
        InterfaceElements.ForEach(element => element.IsChosen = false);
        if (CursorPosX != -1)
            InterfaceElements[CursorPosX].IsChosen = true;
    }

    public void TransformCursor(Point newPos)
    {
        var delta = newPos.X - CursorPosX;

        if (CursorPosX == -1 || delta == 0)
            return;
        var shift = delta > 0 ? 1 : -1;
        do
        {
            CursorPosX += shift;
            if (CursorPosX < firstActiveElementIndex)
            {
                CursorPosX = lastActiveElementIndex;
            }
            else if (CursorPosX > lastActiveElementIndex)
            {
                CursorPosX = firstActiveElementIndex;
            }
        } while (GetCurrentElement().IsInteractive == false);
        Update();



        //if (CursorPosX == -1 || newPos.X == 0)
        //    return;
        //var shift = newPos.X > 0 ? 1 : -1;
        //do
        //{
        //    CursorPosX += shift;
        //    if (CursorPosX < firstActiveElementIndex)
        //    {
        //        CursorPosX = lastActiveElementIndex;
        //    }
        //    else if (CursorPosX > lastActiveElementIndex)
        //    {
        //        CursorPosX = firstActiveElementIndex;
        //    }                           
        //} while (GetCurrentElement().IsInteractive == false);
        //Update();
    }
}
