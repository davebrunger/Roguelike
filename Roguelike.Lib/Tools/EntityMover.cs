namespace Roguelike.Lib.Tools;

public class EntityMover : IEntityMover
{
    public Dungeon MoveEntity(Dungeon dungeon, int entityIndex, int dx, int dy)
    {
        var entity = dungeon.Entities[entityIndex];

        var x = entity.X + dx;
        var y = entity.Y + dy;

        var facing = dx switch
        {
            _ when dx < 0 => Facing.Left,
            _ when dx > 0 => Facing.Right,
            _ => entity.Facing
        };

        (x, y) = x >= 0 && y >= 0 && x < dungeon.Tiles.Width && y < dungeon.Tiles.Height && dungeon.Tiles[x, y].TileType == TileType.Ground && !IsBlocked(dungeon, x, y)
            ? (x, y)
            : (entity.X, entity.Y);

        var newEntity = entity with { X = x, Y = y, Facing = facing };
        return dungeon with { Entities = dungeon.Entities.SetItem(entityIndex, newEntity) };
    }

    private static bool IsBlocked(Dungeon dungeon, int x, int y)
    {
        return dungeon.Entities.Any(e => e.X == x && e.Y == y && (e.EntityType == EntityType.Player || e.EntityType == EntityType.Monster));
    }
}
