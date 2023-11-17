namespace Roguelike.Lib.Tools;

public class LineOfSightFinder : ILineOfSightFinder
{
    public bool HasLineOfSight(int fromX, int fromY, int toX, int toY, Grid<Tile> tiles)
    {
        var dx = Math.Abs(toX - fromX);
        var dy = Math.Abs(toY - fromY);

        var sx = fromX < toX ? 1 : -1;
        var sy = fromY < toY ? 1 : -1;
        var err = dx - dy;

        while (true)
        {
            var e2 = 2 * err;

            if (e2 > -dy)
            {
                err -= dy;
                fromX += sx;
            }

            if (e2 < dx)
            {
                err += dx;
                fromY += sy;
            }

            if (fromX == toX && fromY == toY)
            {
                return true;
            }

            if (tiles[fromX, fromY].TileType == TileType.Wall)
            {
                return false;
            }
        }
    }
}
