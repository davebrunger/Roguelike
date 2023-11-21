using Roguelike.Lib.Inputs;

namespace Roguelike.Lib.Tools;

public class PlayerHandler
{
    private readonly IKeyboard keyboard;
    private readonly IEntityMover entityMover;

    public PlayerHandler(IKeyboard keyboard, IEntityMover entityMover)
    {
        this.keyboard = keyboard;
        this.entityMover = entityMover;
    }

    public (bool changed, bool hasNewNotifications, Dungeon dungeon, long delay) HandleInput(Dungeon dungeon, long delay, long delta)
    {
        var changed = false;
        delay = Math.Max(0, delay - delta);
        
        var hasNewNotifications = false;

        if (delay == 0)
        {
            var dx = 0;
            var dy = 0;

            if (keyboard.IsKeyDown(ConsoleKey.W))
            {
                dy = -1;
            }
            if (keyboard.IsKeyDown(ConsoleKey.S))
            {
                dy = 1;
            }
            if (keyboard.IsKeyDown(ConsoleKey.A))
            {
                dx = -1;
            }
            if (keyboard.IsKeyDown(ConsoleKey.D))
            {
                dx = 1;
            }
            if (dx != 0 || dy != 0)
            {
                (hasNewNotifications, var newDungeon) = entityMover.MoveEntity(dungeon, 0, dx, dy);
                changed = newDungeon.Entities[0].X != dungeon.Entities[0].X || newDungeon.Entities[0].Y != dungeon.Entities[0].Y;
                dungeon = newDungeon;
                delay = Constants.MoveDelay;
            }
        }

        return (changed, hasNewNotifications, dungeon, delay);
    }
}
