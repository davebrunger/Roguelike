using Roguelike.Lib;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices(config =>
{
    config.AddScoped<IWallCounter, WallCounter>();
    config.AddScoped<IWallTidier, WallTidier>();
    config.AddScoped<IWalkGenerator, RandomWalkGenerator>();
    config.AddScoped<ITileRenderer, ConsoleTileRenderer>();
    config.AddScoped<IKeyboard, Keyboard>();
    config.AddScoped<IFogOfWarUpdater, FogOfWarUpdater>();
    config.AddScoped<ILineOfSightFinder, LineOfSightFinder>();
    config.AddScoped<IEntityMover, EntityMover>();
    config.AddScoped<PlayerHandler>();
    config.AddScoped<MapGenerator>();
    config.AddScoped<MapRenderer>();
});

var host = builder.Build();

var (tiles, playerPosition) = host.Services.GetRequiredService<MapGenerator>().GenerateMap();

var player = new Entity(0, EntityType.Player, "Player", playerPosition.X, playerPosition.Y, Facing.Right);
var dungeon = new Dungeon(ImmutableList<Entity>.Empty.Add(player), tiles);

var mapRenderer = host.Services.GetRequiredService<MapRenderer>();
var playerHandler = host.Services.GetRequiredService<PlayerHandler>();

var currentTicks = Environment.TickCount64;
var playerDelay = 0L;
var changed = true;

Console.CursorVisible = false;
Console.ForegroundColor = ConsoleColor.White;

while (true)
{
    if (changed)
    {
        mapRenderer.RenderDungeon(dungeon);
    }
    var newTicks = Environment.TickCount64;
    var delta = newTicks - currentTicks;
    currentTicks = newTicks;
    (changed, dungeon, playerDelay) = playerHandler.HandleInput(dungeon, playerDelay, delta);
}
