

using Microsoft.Xna.Framework;

namespace WiseEngine.UI;
public interface ICursor
{
    List<IComponent> InterfaceElements { get; }
    Point CursorPos { get; }
    void TransformCursor(Point newPos);
    void Update();

    IComponent? GetCurrentElement();

    void AddElement(IComponent component);
}
