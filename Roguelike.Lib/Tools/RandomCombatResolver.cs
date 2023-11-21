namespace Roguelike.Lib.Tools;

public class RandomCombatResolver : ICombatResolver
{
    private static readonly Random random = new();

    private readonly INotificationAdder notificationAdder;

    public RandomCombatResolver(INotificationAdder notificationAdder)
    {
        this.notificationAdder = notificationAdder;
    }

    public (bool hasNewNotifications, Dungeon dungeon) ResolveMeleeAttack(Dungeon dungeon, Entity attacker, Entity defender)
    {
        dungeon = notificationAdder.AddNotification(dungeon, $"{attacker.Name} attacks {defender.Name}.");
        var attack = attacker.Data.MinAttack + (random.Next(attacker.Data.MaxAttack - attacker.Data.MinAttack) + 1);
        var damage = attack - defender.Data.Defence;
        var hasNotifications = false;
        if (damage > 0)
        {
            dungeon = notificationAdder.AddNotification(dungeon, $"{attacker.Name} hits {defender.Name} for {damage} damage.");
            var newHitPoints = Math.Max(0, defender.Data.HitPoints - damage);
            var newDefender = defender with { Data = defender.Data with { HitPoints = newHitPoints }, Dead = newHitPoints == 0 };
            dungeon = dungeon with { Entities = dungeon.Entities.Update(newDefender) };
            if (newDefender.Dead)
            {
                dungeon = notificationAdder.AddNotification(dungeon, $"{defender.Name} dies.");
            }
            hasNotifications = true;
        }
        return (hasNotifications, dungeon);
    }
}
