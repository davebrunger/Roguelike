namespace Roguelike.Lib.Tools;

public class FogOfWarUpdater : IFogOfWarUpdater
{
    private readonly ILineOfSightFinder lineOfSightFinder;

    public FogOfWarUpdater(ILineOfSightFinder lineOfSightFinder)
    {
        this.lineOfSightFinder = lineOfSightFinder;
    }

    public Grid<Tile> UpdateFogOfWar(Grid<Tile> tiles, int playerX, int playerY)
    {
        for (var x = 0; x < tiles.Width; x++)
        {
            for (var y = 0; y < tiles.Height; y++)
            {
                tiles = tiles.SetItem(x, y, tiles[x, y] with { Visible = false });
            }
        }

        for (var vy = -Constants.VisibleDistance; vy <= Constants.VisibleDistance; vy++)
        {
            for (var vx = -Constants.VisibleDistance; vx <= Constants.VisibleDistance; vx++)
            {
                var x = playerX + vx;
                var y = playerY + vy;
                if (GetDistance(playerX, playerY, x, y) > Constants.VisibleDistance)
                {
                    continue;
                }
                if (x < 0 || y < 0 || x >= tiles.Width || y >= tiles.Height)
                {
                    continue;
                }
                if (!lineOfSightFinder.HasLineOfSight(playerX, playerY, x, y, tiles))
                {
                    continue;
                }
                tiles = tiles.SetItem(x, y, tiles[x, y] with { Visible = true, Revealed = true });
            }
        }

        return tiles;
    }

    private static int GetDistance(int x1, int y1, int x2, int y2)
    {
        return (int)Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
    }
}
