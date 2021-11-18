using System;
using Core.Game;
using Core.Entities.Systems;
using System.Collections.Generic;
using Core.Helpers;
using Core.Entities.Components;

namespace Core.Entities
{
    [Flags]
    public enum EntityType : byte
    {
        None = 0,
        Player = 1 << 0,
        Mob = 1 << 1,
        DynamicObject = 1 << 2,
        StaticObject = 1 << 3,
        Vehicle = 1 << 4,
        Effect = 1 << 5,
        All = 0xFF,
    }

    public struct EntityEnumerable
    {
        private readonly Dictionary<EntityType, List<Entity>> _entities;
        private readonly EntityType _entityType;
        private readonly Predicate<Entity> _filter;

        public EntityEnumerable(Dictionary<EntityType, List<Entity>> entities, EntityType entityType = EntityType.All, Predicate<Entity> filter = null)
        {
            _entities = entities;
            _entityType = entityType;
            _filter = filter;
        }

        public EntityEnumerator GetEnumerator()
        {
            return new EntityEnumerator(_entities, _entityType, _filter);
        }
    }

    public struct EntityEnumerator
    {
        private readonly Dictionary<EntityType, List<Entity>> _entities;
        private readonly EntityType _entityType;
        private readonly Predicate<Entity> _filter;

        private EntityType _checkedEntityTypes;
        private int _lastIndex;

        public Entity Current { get; private set; }

        public EntityEnumerator(Dictionary<EntityType, List<Entity>> entities, EntityType entityType, Predicate<Entity> filter)
        {
            _entities = entities;
            _entityType = entityType;
            _filter = filter;

            _checkedEntityTypes = EntityType.None;
            _lastIndex = -1;

            Current = null;
        }

        public bool MoveNext()
        {
            foreach (KeyValuePair<EntityType, List<Entity>> pair in _entities)
            {
                if ((_entityType & pair.Key & ~_checkedEntityTypes) > 0)
                {
                    List<Entity> entities = pair.Value;
                    lock (entities)
                    {
                        for (int i = _lastIndex + 1; i < entities.Count; i++)
                        {
                            Entity entity = entities[i];
                            _lastIndex = i;
                            if (_filter == null || _filter(entity))
                            {
                                Current = entity;
                                return true;
                            }
                        }
                        _checkedEntityTypes |= pair.Key;
                        _lastIndex = -1;
                    }
                }
            }

            return false;
        }
    }

    public class EntityManager: IDisposable
    {
        private ITicking _ticker;
        private List<ISystem> _systems;
        private Dictionary<EntityType, List<Entity>> _entities;

        public event Action<EntityType, Entity> EntitySpawned = delegate { };
        public event Action<EntityType, Entity> EntityDespawned = delegate { };

        public EntityManager(ITicking ticker)
        {
            _ticker = ticker;
            _systems = new List<ISystem>();;

            _entities = new Dictionary<EntityType, List<Entity>>();
            _entities.Add(EntityType.StaticObject, new List<Entity>());
            _entities.Add(EntityType.DynamicObject, new List<Entity>());

            AddSystem(new EntityCollisionSystem(_ticker));
           
            _ticker.Tick += OnTick;
        }

        public EntityEnumerable GetEntities(EntityType entityType = EntityType.All, Predicate<Entity> filter = null)
        {
            return new EntityEnumerable(_entities, entityType, filter);
        }

        public void Dispose()
        {
            _ticker.Tick -= OnTick;

            foreach (KeyValuePair<EntityType, List<Entity>> pair in _entities)
            {
                List<Entity> entities = pair.Value;
                lock (entities)
                {
                    for (int i = 0; i < entities.Count; i++)
                    {
                        entities[i].RaiseUnregister();
                        i--;
                    }
                }
            }

            for (int i = 0; i< _systems.Count; i++)
            {
                _systems[i].Dispose();
            }
        }

        public void AddSystem(ISystem system)
        {
            if (!_systems.Contains(system))
                _systems.Add(system);
        }

        private void OnTick(long tick)
        {
            foreach (ISystem system in _systems)
            {
                foreach (KeyValuePair<EntityType, List<Entity>> pair in _entities)
                {
                    if (!system.HandledEntities.HasEntityType(pair.Key))
                        continue;

                    List<Entity> entities = pair.Value;
                    lock (entities)
                    {
                        for (int i = 0; i < entities.Count; i++)
                        {
                            system.WorkSomeMagic(entities[i], pair.Key);
                        }
                    }
                }
            }
        }

        public Entity RegisterEntity(EntityType type, Entity entity)
        {
            if (entity != null)
            {
                Action<Entity> action = null;
                action = (entity) =>
                {
                    UnregisterEntity(type, entity);
                    entity.Unregister -= action;
                };
                entity.Unregister += action;

                List<Entity> entities = _entities[type];
                lock (entities)
                {
                    entities.Add(entity);
                }
                EntitySpawned(type, entity);
            }

            return entity;
        }

        private void UnregisterEntity(EntityType type, Entity entity)
        {
            List<Entity> entities = _entities[type];
            lock (entities)
            {
                entities.Remove(entity);
            }

            foreach (var component in entity)
            {
                if (component.Value is IDisposed disposableComponent)
                    disposableComponent.Dispose();
            }

            EntityDespawned(type, entity);
        }
    }
}