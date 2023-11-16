namespace Roguelike.Lib.Models;

public record Dungeon(int NextEntityId, ImmutableList<Entity> Entities, Entity Player, ImmutableList<ImmutableList<Tile>> Tiles, (int X, int Y) Camera)
{
}
