namespace Roguelike.Lib.Tools;

public class EntityMover : IEntityMover
{
    private readonly ICombatResolver combatResolver;

    public EntityMover(ICombatResolver combatResolver)
    {
        this.combatResolver = combatResolver;
    }

    public (bool hasNewNotifications, Dungeon dungeon) MoveEntity(Dungeon dungeon, int entityId, int dx, int dy)
    {
        var entity = dungeon.Entities[entityId];

        var x = entity.X + dx;
        var y = entity.Y + dy;

        var facing = dx switch
        {
            _ when dx < 0 => Facing.Left,
            _ when dx > 0 => Facing.Right,
            _ => entity.Facing
        };

        if (x < 0 || y < 0 || x >= dungeon.Tiles.Width || y >= dungeon.Tiles.Height || dungeon.Tiles[x, y].TileType != TileType.Ground)
        {
            return (false, dungeon);
        }

        if (IsBlocked(dungeon, x, y))
        {
            return combatResolver.ResolveMeleeAttack(dungeon, entity, dungeon.Entities.Values.First(e => e.X == x && e.Y == y));
        }

        var newEntity = entity with { X = x, Y = y, Facing = facing };
        return (false, dungeon with { Entities = dungeon.Entities.Update(newEntity) });
    }

    private static bool IsBlocked(Dungeon dungeon, int x, int y)
    {
        return dungeon.Entities.Values.Any(e => e.X == x && e.Y == y && (e.EntityType == EntityType.Player || e.EntityType == EntityType.Monster));
    }
}
