namespace Roguelike.Lib.Tools
{
    public class NotificationAdder : INotificationAdder
    {
        public Dungeon AddNotification(Dungeon dungeon, string notification) =>
                dungeon with { Notifications = dungeon.Notifications.Add((notification, Constants.NotificationDelay)) };
    }
}

