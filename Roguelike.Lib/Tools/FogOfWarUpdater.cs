namespace Roguelike.Lib.Tools;

public class FogOfWarUpdater : IFogOfWarUpdater
{
    private readonly ILineOfSightFinder lineOfSightFinder;

    public FogOfWarUpdater(ILineOfSightFinder lineOfSightFinder)
    {
        this.lineOfSightFinder = lineOfSightFinder;
    }

    public Dungeon UpdateFogOfWar(Dungeon dungeon)
    {
        var tiles = dungeon.Tiles;

        for (var x = 0; x < tiles.Width; x++)
        {
            for (var y = 0; y < tiles.Height; y++)
            {
                tiles = tiles.SetItem(x, y, tiles[x, y] with { Visible = false });
            }
        }

        var blocksLineOfSight = new Grid<bool>(tiles.Width, tiles.Height, _ => false);
        foreach (var entity in dungeon.Entities)
        {
            if (entity.BlocksLineOfSight)
            {
                blocksLineOfSight = blocksLineOfSight.SetItem(entity.X, entity.Y, true);
            }
        }

        var playerX = dungeon.Entities[0].X;
        var playerY = dungeon.Entities[0].Y;

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
                if (!lineOfSightFinder.HasLineOfSight(playerX, playerY, x, y, tiles, blocksLineOfSight))
                {
                    continue;
                }
                tiles = tiles.SetItem(x, y, tiles[x, y] with { Visible = true, Revealed = true });
            }
        }

        return dungeon with { Tiles = tiles };
    }

    private static int GetDistance(int x1, int y1, int x2, int y2)
    {
        return (int)Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
    }
}
