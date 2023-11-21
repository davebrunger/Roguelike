namespace Roguelike.Lib.Tools;

public interface IEntityMover
{
    (bool hasNewNotifications, Dungeon dungeon) MoveEntity(Dungeon dungeon, int entityId, int dx, int dy);
}