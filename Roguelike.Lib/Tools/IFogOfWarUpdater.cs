namespace Roguelike.Lib.Tools;

public interface IFogOfWarUpdater
{
    Grid<Tile> UpdateFogOfWar(Grid<Tile> tiles, int playerX, int playerY);
}