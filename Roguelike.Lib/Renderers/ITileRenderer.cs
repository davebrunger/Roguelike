namespace Roguelike.Lib.Renderers;

public interface ITileRenderer
{
    void Render(int x, int y, Tile tile, IEnumerable<Entity> entities);
}