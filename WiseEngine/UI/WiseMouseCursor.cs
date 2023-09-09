﻿using Microsoft.Xna.Framework;


namespace WiseEngine.UI;

public class WiseMouseCursor : ICursor
{
    public List<IComponent> InterfaceElements { get; set; }

    public Point CursorPos { get; private set; }

    public WiseMouseCursor()
    {
        CursorPos = new Point(0, 0);
    }

 

    public void TransformCursor(Point newPos, List<IComponent> interfaceElements)
    {
        CursorPos = newPos;       
    }

    public void Update(object sender, InterfaceInputsEventArgs e)
    {
        TransformCursor(e.CursorPoint, e.InterfaceElements);
    }
}
