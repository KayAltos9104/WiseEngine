using Microsoft.Xna.Framework.Input;

namespace WiseEngine.MonogamePart;

/// <summary>
/// Single static class which reads keyboard and mouse inputs
/// </summary>
public static class InputsManager
{
    /// <value>
    /// Property <c>PressedCurrentFrame</c> stores all keys which was pressed in current frame
    /// </value>
    public static KeyboardState PressedCurrentFrame { get; private set; }
    /// <value>
    /// Property <c>PressedPrevFrame</c> stores all keys which was pressed in previous frame
    /// </value>
    public static KeyboardState PressedPrevFrame { get; private set; }
    /// <value>
    /// Property <c>MouseStateCurrentFrame</c> stores mouse state in current frame
    /// </value>
    public static MouseState MouseStateCurrentFrame { get; private set; }
    /// <value>
    /// Property <c>MouseStatePreviousFrame</c> stores mouse state in previous frame
    /// </value>
    public static MouseState MouseStatePreviousFrame { get; private set; }
    /// <summary>
    /// Loads all inputs from user and saves in <see cref="PressedCurrentFrame"/>
    /// </summary>
    public static void ReadInputs()
    {
        PressedCurrentFrame = Keyboard.GetState();
        MouseStateCurrentFrame = Mouse.GetState();
        //TODO: Потом добавить мышу
    }

    /// <summary>
    /// Save all inputs from user in <see cref="PressedPrevFrame"/> to use on the next frame
    /// </summary>
    public static void SaveInputs()
    {
        PressedPrevFrame = Keyboard.GetState();
        MouseStatePreviousFrame = Mouse.GetState();
    }

    /// <summary>
    /// Checks single pressing of button.
    /// </summary>
    /// <param name="key">
    /// Which single pressing key must be checked.
    /// </param>
    /// <returns>
    /// True if this key was pressed single time.
    /// </returns>
    public static bool IsSinglePressed(Keys key)
    {
        return PressedCurrentFrame.IsKeyUp(key) && PressedPrevFrame.IsKeyDown(key);
    }

    /// <summary>
    /// Checks single click of mouse.
    /// </summary>
    /// <param name="button">
    /// Which single pressing button must be checked.
    /// </param>
    /// <returns>
    /// True if this button was pressed single time.
    /// </returns>
    public static bool IsSingleClicked(MouseButton button)
    {
        switch (button)
        {
            case MouseButton.Left:
                return MouseStateCurrentFrame.LeftButton != ButtonState.Pressed && 
                    MouseStatePreviousFrame.LeftButton == ButtonState.Pressed;
            case MouseButton.Right:
                return MouseStateCurrentFrame.RightButton != ButtonState.Pressed && 
                    MouseStatePreviousFrame.RightButton == ButtonState.Pressed;
            case MouseButton.Middle:
                return MouseStateCurrentFrame.MiddleButton != ButtonState.Pressed && 
                    MouseStatePreviousFrame.MiddleButton == ButtonState.Pressed;
            default: 
                return false;
        }
        
    }

    public enum MouseButton:byte
    {
        Left,
        Right,
        Middle,
    }
}
