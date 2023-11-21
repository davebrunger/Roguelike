namespace Roguelike.Lib.Models;

public record Dungeon(Grid<Tile> Tiles, Roster<Entity> Entities, ImmutableList<Entity> Dead, ImmutableList<(string Notification, long Delay)> Notifications)
{
    public Dungeon(Grid<Tile> tiles) : this(tiles, new Roster<Entity>(), ImmutableList<Entity>.Empty, ImmutableList<(string Notification, long Delay)>.Empty)
    {
    }
}
