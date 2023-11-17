namespace Roguelike.Lib.Tools;

public interface IEntityMover
{
    Entity MoveEntity(Entity entity, int dx, int dy, Grid<Tile> tiles);
}