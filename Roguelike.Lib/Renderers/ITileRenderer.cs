namespace Roguelike.Lib.Renderers;

public interface ITileRenderer
{
    void Clear();
    void NewLine();
    void Render(int x, int y, Tile tile);
}