namespace Roguelike.Lib.Tools;

public class EntityAdder
{
    private static readonly Random Random = new();

    public Dungeon InitPlayer(Dungeon dungeon, int x, int y) =>
        dungeon with { Entities = dungeon.Entities.Add(new(0, EntityType.Player, "Player", x, y, Facing.Right, true)) };

    public Dungeon InitMonster(Dungeon dungeon, string name)
    {
        var x = 0;
        var y = 0;
        var okay = false;
        while (!okay)
        {
            x = 1 + Random.Next(dungeon.Tiles.Width - 2);
            y = 1 + Random.Next(dungeon.Tiles.Height - 2);

            okay = dungeon.Tiles[x, y].TileType == TileType.Ground &&
                   dungeon.Entities.All(e => e.X != x || e.Y != y);
        }
        return dungeon with { Entities = dungeon.Entities.Add(new(dungeon.Entities.Count, EntityType.Monster, name, x, y, Facing.Right, true)) };
    }
}
