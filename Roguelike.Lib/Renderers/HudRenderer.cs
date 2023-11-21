namespace Roguelike.Lib.Renderers;

public class HudRenderer
{
    private readonly INotificationRenderer notificationRenderer;

    public HudRenderer(INotificationRenderer notificationRenderer)
    {
        this.notificationRenderer = notificationRenderer;
    }

    public void RenderHud(Dungeon dungeon)
    {
        var notificationsToRender = dungeon.Notifications
            .Reverse()
            .Take(Constants.MaxNotifications)
            .ToList();

        for (int i = 0; i < Constants.MaxNotifications; i++)
        {
            notificationRenderer.Render(i, i < notificationsToRender.Count ? notificationsToRender[i].Notification : string.Empty);
        }
    }
}
