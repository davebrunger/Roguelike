namespace Roguelike.Lib.Generators;

public class EmptyMapGenerator : IMapGenerator
{
    public (Grid<Tile> tiles, (int X, int Y) playerPosition) GenerateMap()
    {
        var tiles = new Grid<Tile>(Constants.MapWidth, Constants.MapHeight, _ => new Tile(TileType.Wall, false, false));

        for (var x = 1; x < Constants.RenderWidth; x++)
        {
            for (var y = 1; y < Constants.RenderHeight; y++)
            {
                tiles = tiles.SetItem(x, y, tiles[x, y] with { TileType = TileType.Ground, Revealed = true });
            }
        }

        return (tiles, (Constants.RenderWidth / 2, Constants.RenderHeight / 2));
    }
}
