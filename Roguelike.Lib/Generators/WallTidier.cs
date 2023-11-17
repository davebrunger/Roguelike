namespace Roguelike.Lib.Generators;

public class WallTidier : IWallTidier
{
    private readonly IWallCounter wallCounter;

    public WallTidier(IWallCounter wallCounter)
    {
        this.wallCounter = wallCounter;
    }

    public (Grid<Tile> Tiles, int WallsTidied) TidyWalls(Grid<Tile> tiles)
    {
        int tilesTidied = 0;
        for (var x = 1; x < tiles.Width - 1; x++)
        {
            for (var y = 1; y < tiles.Height - 1; y++)
            {
                if (tiles[x, y].TileType == TileType.Wall && wallCounter.CountWalls(x, y, tiles) < 2)
                {
                    tilesTidied++;
                    tiles = tiles.SetItem(x, y, tiles[x, y] with { TileType = TileType.Ground });
                }
            }
        }
        return (tiles, tilesTidied);
    }
}
