namespace Roguelike.Lib.Renderers;

public class ConsoleTileRenderer : ITileRenderer
{
    public void Clear()
    {
        Console.Clear();
    }

    public void NewLine()
    {
        Console.WriteLine();
    }

    public void Render(int x, int y, Tile tile)
    {
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
        if (tile.TileType == TileType.Wall)
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
