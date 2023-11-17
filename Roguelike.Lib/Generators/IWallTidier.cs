namespace Roguelike.Lib.Generators;

public interface IWallTidier
{
    (Grid<Tile> Tiles, int WallsTidied) TidyWalls(Grid<Tile> tiles);
}