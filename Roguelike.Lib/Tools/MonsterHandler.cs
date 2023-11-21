namespace Roguelike.Lib.Tools;

public class MonsterHandler : IMonsterHandler
{
    public Entity? HandleMonster(Entity entity)
    {
        return entity.Dead ? null : entity;
    }
}
