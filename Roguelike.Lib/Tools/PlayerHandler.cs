using Roguelike.Lib.Inputs;

namespace Roguelike.Lib.Tools;

public class PlayerHandler
{
    private readonly IKeyboard keyboard;
    private readonly IEntityMover entityMover;
    private readonly IFogOfWarUpdater fogOfWarUpdater;

    public PlayerHandler(IKeyboard keyboard, IEntityMover entityMover, IFogOfWarUpdater fogOfWarUpdater)
    {
        this.keyboard = keyboard;
        this.entityMover = entityMover;
        this.fogOfWarUpdater = fogOfWarUpdater;
    }

    public (bool changed, Dungeon dungeon, long delay) HandleInput(Dungeon dungeon, long delay, long delta)
    {
        var changed = false;
        delay = Math.Max(0, delay - delta);
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
                var newDungeon = entityMover.MoveEntity(dungeon, 0, dx, dy);
                changed = newDungeon.Entities[0].X != dungeon.Entities[0].X || newDungeon.Entities[0].Y != dungeon.Entities[0].Y;
                if (changed)
                {
                    dungeon = fogOfWarUpdater.UpdateFogOfWar(newDungeon);
                }
                delay = Constants.MoveDelay;
            }
        }

        return (changed, dungeon, delay);
    }
}
