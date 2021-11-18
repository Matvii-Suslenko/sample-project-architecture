using Core.Entities;

namespace Core.Entities.Systems
{
    public interface ISystem
    {
        EntityType HandledEntities { get; }

        void WorkSomeMagic(Entity entity, EntityType type);

        void Dispose();
    }
}