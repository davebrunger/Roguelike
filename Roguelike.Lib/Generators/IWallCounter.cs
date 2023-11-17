namespace Roguelike.Lib.Generators;

public interface IWallCounter
{
    int CountWalls(int X, int Y, Grid<Tile> tiles);
}