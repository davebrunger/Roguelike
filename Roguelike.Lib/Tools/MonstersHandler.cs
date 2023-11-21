namespace Roguelike.Lib.Tools;

public class MonstersHandler
{
    private readonly IMonsterHandler monsterHandler;

    public MonstersHandler(IMonsterHandler monsterHandler)
    {
        this.monsterHandler = monsterHandler;
    }

    public (bool Changed, Roster<Entity> Entities) HandleMonsters(Roster<Entity> entities)
    {
        var keys = entities.Keys.ToList();
        var changed = false;
        foreach (var entityId in keys)
        {
            if (entities[entityId].EntityType == EntityType.Monster)
            {
                var monster = monsterHandler.HandleMonster(entities[entityId]);
                entities = monster != null 
                    ? entities.Update(monster)
                    : entities.Remove(entityId);
                changed = true;
            }
        }
        return (changed, entities);
    }
}
