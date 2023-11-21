namespace Roguelike.Lib.Tools;

public interface INotificationAdder
{
    Dungeon AddNotification(Dungeon dungeon, string notification);
}