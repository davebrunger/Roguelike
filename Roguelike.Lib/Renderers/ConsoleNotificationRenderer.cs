namespace Roguelike.Lib.Renderers;

public class ConsoleNotificationRenderer : INotificationRenderer
{
    public void Render(int line, string notification)
    {
        Console.SetCursorPosition(0, Constants.RenderHeight + line + 1);
        Console.Write(notification.PadRight(Constants.MaxNotificationWidth)[..Constants.MaxNotificationWidth]);
    }
}
