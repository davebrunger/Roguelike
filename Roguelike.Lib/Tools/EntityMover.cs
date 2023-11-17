namespace Roguelike.Lib.Tools;

public class EntityMover : IEntityMover
{
    public Entity MoveEntity(Entity entity, int dx, int dy, Grid<Tile> tiles)
    {
        var x = entity.X + dx;
        var y = entity.Y + dy;

        var facing = dx switch
        {
            _ when dx < 0 => Facing.Left,
            _ when dx > 0 => Facing.Right,
            _ => entity.Facing
        };

        (x, y) = x >= 0 && y >= 0 && x < tiles.Width && y < tiles.Height && tiles[x, y].TileType == TileType.Ground
            ? (x, y)
            : (entity.X, entity.Y);

        return entity with { X = x, Y = y, Facing = facing };
    }
}
