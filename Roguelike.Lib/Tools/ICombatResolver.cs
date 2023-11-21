namespace Roguelike.Lib.Tools;

public interface ICombatResolver
{
    (bool hasNewNotifications, Dungeon dungeon) ResolveMeleeAttack(Dungeon dungeon, Entity attacker, Entity defender);
}