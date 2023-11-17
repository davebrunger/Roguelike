namespace Roguelike.Lib.Generators;

public class MapGenerator
{
    private readonly IWalkGenerator walkGenerator;
    private readonly IWallTidier wallTidier;
    private readonly IFogOfWarUpdater fogOfWarUpdater;

    public MapGenerator(IWalkGenerator walkGenerator, IWallTidier wallTidier, IFogOfWarUpdater fogOfWarUpdater)
    {
        this.walkGenerator = walkGenerator;
        this.wallTidier = wallTidier;
        this.fogOfWarUpdater = fogOfWarUpdater;
    }

    public (Grid<Tile> tiles, (int X, int Y) playerPosition) GenerateMap()
    {
        var tiles = new Grid<Tile>(Constants.MapWidth, Constants.MapHeight, _ => new Tile(TileType.Wall, false, false));
        (int X, int Y) playerPosition = (0, 0);

        foreach (var (x, y) in walkGenerator.GenerateWalk())
        {
            tiles = tiles.SetItem(x, y, tiles[x, y] with { TileType = TileType.Ground });
            playerPosition = (x, y);
        }

        var wallsTidied = int.MaxValue;
        while (wallsTidied > 0)
        {
            (tiles, wallsTidied) = wallTidier.TidyWalls(tiles);
        }

        tiles = fogOfWarUpdater.UpdateFogOfWar(tiles, playerPosition.X, playerPosition.Y);

        return (tiles, playerPosition);
    }
}
