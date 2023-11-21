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
    config.AddScoped<ICombatResolver, RandomCombatResolver>();
    config.AddScoped<INotificationRenderer, ConsoleNotificationRenderer>();
    config.AddScoped<IEntityAdder, RandomMonsterPlacementEntityAdder>();
    config.AddScoped<INotificationAdder, NotificationAdder>();
    config.AddScoped<PlayerHandler>();
    config.AddScoped<MapRenderer>();
    config.AddScoped<MonstersHandler>();
    config.AddScoped<HudRenderer>();
    config.AddScoped<NotificationUpdater>();
});

var host = builder.Build();

var mapGenerator = host.Services.GetRequiredService<IMapGenerator>();
var entityAdder = host.Services.GetRequiredService<IEntityAdder>();
var mapRenderer = host.Services.GetRequiredService<MapRenderer>();
var playerHandler = host.Services.GetRequiredService<PlayerHandler>();
var monstersHandler = host.Services.GetRequiredService<MonstersHandler>();
var fogofWarUpdater = host.Services.GetRequiredService<IFogOfWarUpdater>();
var hudRenderer = host.Services.GetRequiredService<HudRenderer>();
var notificationUpdater = host.Services.GetRequiredService<NotificationUpdater>();

var (tiles, playerPosition) = mapGenerator.GenerateMap();

var dungeon = new Dungeon(tiles);
dungeon = entityAdder.InitPlayer(dungeon, playerPosition.X, playerPosition.Y);
dungeon = entityAdder.InitMicroMouse(dungeon);
dungeon = fogofWarUpdater.UpdateFogOfWar(dungeon);

var currentTicks = Environment.TickCount64;
var playerDelay = 0L;
var changed = true;
var hudChanged = true;

Console.CursorVisible = false;
Console.ForegroundColor = ConsoleColor.White;

while (true)
{
    if (changed)
    {
        mapRenderer.RenderDungeon(dungeon);
    }
    if (hudChanged)
    {
        hudRenderer.RenderHud(dungeon);     
    }
    var newTicks = Environment.TickCount64;
    var delta = newTicks - currentTicks;
    currentTicks = newTicks;

    (var hasRemovedNotifications, dungeon) = notificationUpdater.UpdateNotifications(dungeon, delta);
    (var playerChanged, var hasNewNotifications, dungeon, playerDelay) = playerHandler.HandleInput(dungeon, playerDelay, delta);
    var (entitiesChanged, entities) = monstersHandler.HandleMonsters(dungeon.Entities);
    dungeon = dungeon with { Entities = entities };
    
    changed = playerChanged || entitiesChanged;
    hudChanged = hasRemovedNotifications || hasNewNotifications;
    if (changed)
    {
        dungeon = fogofWarUpdater.UpdateFogOfWar(dungeon);
    }
}
