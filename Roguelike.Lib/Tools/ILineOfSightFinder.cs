namespace Roguelike.Lib.Tools;

public interface ILineOfSightFinder
{
    bool HasLineOfSight(int fromX, int fromY, int toX, int toY, Grid<Tile> tiles, Grid<bool> blocksLineOfSight);
}