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
    config.AddScoped<IMapGenerator, EmptyMapGenerator>();
    config.AddScoped<IMonsterHandler, MonsterHandler>();
    config.AddScoped<PlayerHandler>();
    config.AddScoped<MapRenderer>();
    config.AddScoped<EntityAdder>();
    config.AddScoped<MonstersHandler>();
});

var host = builder.Build();

var mapGenerator = host.Services.GetRequiredService<IMapGenerator>();
var entityAdder = host.Services.GetRequiredService<EntityAdder>();
var mapRenderer = host.Services.GetRequiredService<MapRenderer>();
var playerHandler = host.Services.GetRequiredService<PlayerHandler>();
var monstersHandler = host.Services.GetRequiredService<MonstersHandler>();
var fogofWarUpdater = host.Services.GetRequiredService<IFogOfWarUpdater>();

var (tiles, playerPosition) = mapGenerator.GenerateMap();

var dungeon = new Dungeon(ImmutableList<Entity>.Empty, tiles);
dungeon = entityAdder.InitPlayer(dungeon, playerPosition.X, playerPosition.Y);
dungeon = entityAdder.InitMonster(dungeon, "Micro Mouse");
dungeon = fogofWarUpdater.UpdateFogOfWar(dungeon);

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
    dungeon = dungeon with { Entities = monstersHandler.HandleMonsters(dungeon.Entities) };
}
