namespace Roguelike.Lib.Tools;

public class RandomMonsterPlacementEntityAdder : IEntityAdder
{
    private static readonly Random random = new();
    private static readonly Monster player = new(25, 25, 1, 4, 4);

    public Dungeon InitPlayer(Dungeon dungeon, int x, int y) =>
        dungeon with { Entities = dungeon.Entities.Add(id => new(id, EntityType.Player, "Player", x, y, Facing.Right, true, false, player)) };

    public Dungeon InitMicroMouse(Dungeon dungeon)
    {
        var hitPoints = 1 + random.Next(4);
        var monster = new Monster(hitPoints, hitPoints, 1, 2, 1);
        return InitMonster(dungeon, "Micro Mouse", monster);
    }

    private static Dungeon InitMonster(Dungeon dungeon, string name, Monster monster)
    {
        var x = 0;
        var y = 0;
        var okay = false;
        while (!okay)
        {
            x = 1 + random.Next(dungeon.Tiles.Width - 2);
            y = 1 + random.Next(dungeon.Tiles.Height - 2);

            okay = dungeon.Tiles[x, y].TileType == TileType.Ground &&
                   dungeon.Entities.Values.All(e => e.X != x || e.Y != y);
        }
        return dungeon with { Entities = dungeon.Entities.Add(id => new(id, EntityType.Monster, name, x, y, Facing.Right, true, false, monster)) };
    }
}
