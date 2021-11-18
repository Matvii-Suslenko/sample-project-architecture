using Core.Entities.Components;
using Core.Enums;

namespace Core.Entities
{
    public static class EntityFactory
    {
        public static Entity CreateEntity(EntityId id) => CreateEntity(new EntityDataComponent(id));

        private static Entity CreateEntity(EntityDataComponent data)
        {
            Entity entity = new Entity();
            entity.AddComponent(data);
          
            switch (data.Id)
            {
                case EntityId.Box:
                    //entity.AddComponent(new PlayerDataComponent());
                    break;
            }

            return entity;
        }
    }
}