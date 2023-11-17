namespace Roguelike.Lib.Tools;

public class MonstersHandler
{
    private readonly IMonsterHandler monsterHandler;

    public MonstersHandler(IMonsterHandler monsterHandler)
    {
        this.monsterHandler = monsterHandler;
    }

    public ImmutableList<Entity> HandleMonsters(ImmutableList<Entity> entities)
    {
        for (var i = 0; i < entities.Count; i++)
        {
            if (entities[i].EntityType == EntityType.Monster)
            {
                var monster = monsterHandler.HandleMonster(entities[i]);
                entities = entities.SetItem(i, monster);
            }
        }
        return entities;
    }
}
