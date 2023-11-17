namespace Roguelike.Lib.Renderers;

public class ConsoleTileRenderer : ITileRenderer
{
    public void Clear()
    {
        // Console.Clear();
    }

    public void NewLine()
    {
        // Console.WriteLine();
    }

    public void Render(int x, int y, Tile tile, IEnumerable<Entity> entities)
    {
        Console.SetCursorPosition(x, y);
        if (!tile.Revealed || tile.TileType == TileType.Hole)
        {
            Console.Write(' ');
            return;
        }
        var currentColor = Console.ForegroundColor;
        if (!tile.Visible)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
        }
        if (entities.Any(e => e.EntityType == EntityType.Player))
        {
            Console.Write("@");
        }
        else if (entities.Any(e => e.EntityType == EntityType.Monster) && tile.Visible)
        {
            Console.Write("X");
        }
        else if (tile.TileType == TileType.Wall)
        {
            Console.Write('#');
        }
        else
        {
            Console.Write('.');
        }
        Console.ForegroundColor = currentColor;
    }
}
