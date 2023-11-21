namespace Roguelike.Lib.Tools
{
    public class NotificationUpdater
    {
        public (bool Changed, Dungeon dungeon) UpdateNotifications(Dungeon dungeon, long delta)
        {
            var newDungeon = dungeon with
            {
                Notifications = dungeon.Notifications
                    .Select(n => (n.Notification, Delay : n.Delay - delta))
                    .Where(n => n.Delay > 0)
                    .ToImmutableList()
            };

            return (newDungeon.Notifications.Count != dungeon.Notifications.Count, newDungeon);
        }
    }
}
