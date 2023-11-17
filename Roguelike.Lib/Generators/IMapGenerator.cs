namespace Roguelike.Lib.Generators;

public interface IMapGenerator
{
    (Grid<Tile> tiles, (int X, int Y) playerPosition) GenerateMap();
}