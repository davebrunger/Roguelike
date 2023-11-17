namespace Roguelike.Lib.Tools;

public interface IEntityMover
{
    Dungeon MoveEntity(Dungeon dungeon, int entityIndex, int dx, int dy);
}