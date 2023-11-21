namespace Roguelike.Lib.Models;

public record Entity(int Id, EntityType EntityType, string Name, int X, int Y, Facing Facing, bool BlocksLineOfSight, bool Dead, Monster Data) : IEntity
{
}
