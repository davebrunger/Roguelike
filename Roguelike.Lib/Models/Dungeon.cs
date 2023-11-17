namespace Roguelike.Lib.Models;

public record Dungeon(ImmutableList<Entity> Entities, Grid<Tile> Tiles)
{
}
