namespace Roguelike.Lib.Inputs;

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
                var player = entityMover.MoveEntity(dungeon.Entities[0], dx, dy, dungeon.Tiles);
                changed = player.X != dungeon.Entities[0].X || player.Y != dungeon.Entities[0].Y;
                var tiles = changed
                    ? fogOfWarUpdater.UpdateFogOfWar(dungeon.Tiles, player.X, player.Y)
                    : dungeon.Tiles;
                dungeon = changed
                    ? dungeon with { Entities = dungeon.Entities.SetItem(0, player), Tiles = tiles }
                    : dungeon;
                delay = Constants.MoveDelay;
            }
        }
        
        return (changed, dungeon, delay);
    }
}
