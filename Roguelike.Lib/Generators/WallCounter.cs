namespace Roguelike.Lib.Generators;

public class WallCounter : IWallCounter
{
    public int CountWalls(int X, int Y, Grid<Tile> tiles)
    {
        var count = 0;
        
        for (var x = -1; x <= 1; x++)
        {
            for (var y = -1; y <= 1; y++)
            {
                if (tiles[X + x, Y + y].TileType == TileType.Wall)
                {
                    count++;
                }
            }
        }

        return count;
    }
}
