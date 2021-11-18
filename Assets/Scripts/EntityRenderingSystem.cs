using System;
using Core.Entities;
using Core.Entities.Components;
using Core.Entities.Systems;
using Events;

namespace Components
{
    public class EntityRenderingSystem : ISystem
    {
        public EntityType HandledEntities => EntityType.DynamicObject | EntityType.StaticObject;

        private UnityEvents _unityEvents;
        private EntityManager _entityManager;

        public EntityRenderingSystem(EntityManager entityManager, UnityEvents unityEvents)
        {
            _entityManager = entityManager;
            _unityEvents = unityEvents;
            _unityEvents.OnUpdate += OnUnityUpdate;
        }

        private void OnUnityUpdate()
        {
            foreach (Entity entity in _entityManager.GetEntities(HandledEntities))
            {
                if (!entity.TryGetComponent(out PositionComponent position))
                    continue;

                //if (_spawnManager.InTheZone(position.Position))
                //{
                //    if (!entity.TryGetComponent(out EntityGO go) && entity.TryGetComponent(out EntityDataComponent entityData))
                //        VisualizeEntity(entity, entityData);
                //}
                //else
                //{
                //    if (entity.TryGetComponent(out EntityGO go))
                //    {
                //        RemoveRender(entity, go);
                //    }
                //}
            }
        }

        public void Dispose()
        {
            _unityEvents.OnUpdate -= OnUnityUpdate;
        }

        public void WorkSomeMagic(Entity entity, EntityType type)
        {
            throw new System.NotImplementedException();
        }
    }

}
