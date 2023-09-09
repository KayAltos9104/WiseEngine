

using Microsoft.Xna.Framework;

namespace WiseEngine.UI;
public interface ICursor
{   
    Point CursorPos { get; }

    void TransformCursor(Point newPos, List<IComponent> interfaceElements);
    void Update(object? sender, InterfaceInputsEventArgs e);
}
