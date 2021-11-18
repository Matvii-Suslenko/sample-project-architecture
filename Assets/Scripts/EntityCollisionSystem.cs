using Core.Entities;
using Core.Entities.Systems;
using Core.Game;
using Core.Helpers;

public class EntityCollisionSystem : ISystem
{
    public EntityType HandledEntities => EntityType.DynamicObject | EntityType.StaticObject;
    public ITicking _ticker;

    public EntityCollisionSystem(ITicking ticker)
    {
        _ticker = ticker;
    }

    public void Dispose()
    {
    }

    public void WorkSomeMagic(Entity entity, EntityType type)
    {
        LoggerCore.SendLog($"Entity collision magic. {_ticker.Time}");
    }
}
